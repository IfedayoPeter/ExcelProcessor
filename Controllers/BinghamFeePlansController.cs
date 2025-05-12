using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static Processor.Controllers.BinghamStudentController;

namespace Processor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeePlanController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly string[] ValidTenants = { "undergraduate", "postgraduate" };

        public FeePlanController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Reusable HttpClient Configuration Method
        private HttpClient ConfigureHttpClient(string tenant)
        {
            if (!ValidTenants.Contains(tenant.ToLower()))
            {
                throw new ArgumentException("Invalid tenant type. Valid tenants are: undergraduate, postgraduate.");
            }

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://portal.binghamuni.edu.ng/integrations/api/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "HnKcqzjS10ZqAkfPl7IkFMf6zMFOh3iiwgjlQAgT7a3caa2c");
            client.DefaultRequestHeaders.Add("X-API-TENANT", tenant);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        [HttpGet("fee-plans")]
        public async Task<IActionResult> GetFeePlansAsync(
            string tenant,
            string filter = "",
            string sort = "",
            int limit = 25)
        {
            if (!new[] { 25, 50, 100 }.Contains(limit))
            {
                return BadRequest("Invalid limit. Valid limits are: 25, 50, 100.");
            }

            try
            {
                var feePlans = await FetchAllFeePlansAsync(tenant, filter, sort, limit);

                if (feePlans == null || !feePlans.Any())
                {
                    return NoContent(); // Return No Content if no fee plans found
                }

                var fileContent = GenerateExcelFile(feePlans, tenant);

                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{tenant}_fee_plans.xlsx");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<List<FeePlan>> FetchFeePlansAsync(string tenant, string filter, string sort, int limit)
        {
            var client = ConfigureHttpClient(tenant);
            var queryParameters = BuildQueryParameters(filter, sort, limit);
            var queryString = string.Join("&", queryParameters);

            var response = await client.GetAsync($"fee-plans?{queryString}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch fee plans. Reason: {response.ReasonPhrase}");
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<FeePlan>>();
            return apiResponse?.Data?.Data.Data ?? new List<FeePlan>();
        }

        private async Task<List<FeePlan>> FetchAllFeePlansAsync(string tenant, string filter, string sort, int limit)
        {
            List<FeePlan> feePlans = [];
            var client = ConfigureHttpClient(tenant);
            var queryParameters = BuildQueryParameters(filter, sort, limit);
            var queryString = string.Join("&", queryParameters);

            int totalPages = 0;
            int totalPlans = 0;

            var response = await client.GetAsync($"fee-plans?{queryString}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch fee plans. Reason: {response.ReasonPhrase}");
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<FeePlan>>();

            var data = apiResponse?.Data?.Data;

            if (data is not null)
            {
                totalPages = data.last_page;
                totalPlans = data.total;
            };

            if (totalPages > 1)
            {
                try
                {
                    for (int i = 1; i <= totalPages; i++)
                    {
                        response = await client.GetAsync($"fee-plans?{queryString}&page={i}");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Failed to fetch students. Reason: {response.ReasonPhrase}");
                        }

                        apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<FeePlan>>();

                        feePlans.AddRange(apiResponse?.Data?.Data.Data);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return feePlans;
        }


        private List<string> BuildQueryParameters(string filter, string sort, int limit)
        {
            var queryParameters = new List<string> { $"limit={limit}" };

            if (!string.IsNullOrEmpty(filter))
            {
                queryParameters.Add($"filter[{filter}]");
            }

            if (!string.IsNullOrEmpty(sort))
            {
                queryParameters.Add($"sort={sort}");
            }

            return queryParameters;
        }

        private MemoryStream GenerateExcelFile(List<FeePlan> feePlans, string tenant)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Fee Plans");

            WriteHeaders(worksheet);
            WriteFeePlanData(worksheet, feePlans);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        private void WriteHeaders(IXLWorksheet worksheet)
        {
            var headers = new List<string>
            {
                "FeePlan Code", "FeeItem Code", "FeeItem Name", "Class Code", "Student Type", "Amount",
                "Programme Code", "Programme Name", "Department Code", "Department Name", "Fculty Code",
                "Faculty Name", "Academic session", "Currency ISO", "Currency Symbol", "Currenct htmlEntity","Revenue Account"
            };

            for (var col = 1; col <= headers.Count; col++)
            {
                worksheet.Cell(1, col).Value = headers[col - 1];
            }
        }

        private void WriteFeePlanData(IXLWorksheet worksheet, List<FeePlan> feePlans)
        {
            var row = 2;
            foreach (var feePlan in feePlans)
            {
                var col = 1;
                worksheet.Cell(row, col++).Value = feePlan.fee_plan_code;
                worksheet.Cell(row, col++).Value = feePlan.fee_item?.code;
                worksheet.Cell(row, col++).Value = feePlan.fee_item?.name;
                worksheet.Cell(row, col++).Value = feePlan.class_code;
                worksheet.Cell(row, col++).Value = feePlan.student_type;
                worksheet.Cell(row, col++).Value = feePlan.amount;
                worksheet.Cell(row, col++).Value = feePlan.programme?.Code;
                worksheet.Cell(row, col++).Value = feePlan.programme?.Name;
                worksheet.Cell(row, col++).Value = feePlan.department?.Code;
                worksheet.Cell(row, col++).Value = feePlan.department?.Name;
                worksheet.Cell(row, col++).Value = feePlan.faculty?.Code;
                worksheet.Cell(row, col++).Value = feePlan.faculty?.Name;
                worksheet.Cell(row, col++).Value = feePlan.academic_session;
                worksheet.Cell(row, col++).Value = feePlan.currency?.iso;
                worksheet.Cell(row, col++).Value = feePlan.currency?.symbol;
                worksheet.Cell(row, col++).Value = feePlan.currency?.htmlentity;
                worksheet.Cell(row, col++).Value = feePlan.revenue_account;
                row++;
            }
        }

        public class Currency
        {
            public string iso { get; set; }
            public string symbol { get; set; }
            public string htmlentity { get; set; }
        }

        public class FeePlan
        {
            public string fee_plan_code { get; set; }
            public FeeItem fee_item { get; set; }
            public string class_code { get; set; }
            public int student_type { get; set; }
            public decimal amount { get; set; }
            public Programme programme { get; set; }
            public Department department { get; set; }
            public Faculty faculty { get; set; }
            public string academic_session { get; set; }
            public Currency currency { get; set; }
            public string revenue_account { get; set; }
        }


        public class FeeItem
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class FeePlanResponse
        {
            public List<FeePlan> Data { get; set; }
            public int current_page { get; set; }
            public int last_page { get; set; }
            public int total { get; set; }
        }

        public class ApiResponse<T>
        {
            public bool success { get; set; }
            public int code { get; set; }
            public Data1 Data { get; set; }
        }
        public class Data1
        {
            public FeePlanResponse Data { get; set; }
        }
    }
}
