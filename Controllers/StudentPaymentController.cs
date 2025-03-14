using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ValueJetImport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPaymentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentPaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("export/{tenant}")]
        public async Task<IActionResult> ExportStudentPaymentsAsync(string tenant, [FromQuery] string? paymentCode, [FromQuery] string? studentNumber, [FromQuery] string? sort, [FromQuery] int limit = 100)
        {
            // Validate the tenant type
            var validTenants = new[] { "undergraduate", "postgraduate" };
            if (!validTenants.Contains(tenant.ToLower()))
            {
                return BadRequest("Invalid tenant type. Valid tenants are: undergraduate, postgraduate.");
            }

            // Prepare query parameters
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(paymentCode))
                queryParams.Add($"filter[payment_code]={paymentCode}");
            if (!string.IsNullOrEmpty(studentNumber))
                queryParams.Add($"filter[student_number]={studentNumber}");
            if (!string.IsNullOrEmpty(sort))
                queryParams.Add($"sort={sort}");
            queryParams.Add($"limit={limit}");

            string queryString = queryParams.Any() ? $"?{string.Join("&", queryParams)}" : "";

            // Prepare the HTTP client
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://portal.binghamuni.edu.ng/integrations/api/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "HnKcqzjS10ZqAkfPl7IkFMf6zMFOh3iiwgjlQAgT7a3caa2c");
            client.DefaultRequestHeaders.Add("X-API-TENANT", tenant);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Call the student payments API
            var response = await client.GetAsync($"student-payments{queryString}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch student payments. Reason: {response.ReasonPhrase}");
            }

            // Deserialize the response JSON
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Raw API Response: " + responseContent); // Debugging

            // Deserialize API response
            var jsonObject = JsonSerializer.Deserialize<ApiResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var studentPayments = jsonObject?.Data?.Data?.Data;
            if (studentPayments == null || !studentPayments.Any())
            {
                return BadRequest("No student payments found.");
            }

            // Generate Excel file
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Student Payments");

            // Define Headers
            var headers = new List<string> { "Payment Code", "Student Code", "Total Amount", "Payment Date", "Description", "Bank Code", "Academic Session", "Verified" };
            for (int i = 0; i < headers.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Populate Data
            int row = 2;
            foreach (var payment in studentPayments)
            {
                worksheet.Cell(row, 1).Value = payment.Payment_Code;
                worksheet.Cell(row, 2).Value = payment.Student_Code;
                worksheet.Cell(row, 3).Value = payment.Total_Amount;
                worksheet.Cell(row, 4).Value = payment.Payment_Date;
                worksheet.Cell(row, 5).Value = payment.Payment_Description;
                worksheet.Cell(row, 6).Value = payment.Bank_Code;
                worksheet.Cell(row, 7).Value = payment.Academic_Session;
                worksheet.Cell(row, 8).Value = payment.Is_Payment_Verified ? "Yes" : "No";
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Save Excel to memory stream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Return file as a response
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{tenant}_student_payments.xlsx");
        }
    }

    // C# Model for API Response
    public class ApiResponse
    {
        public DataWrapper Data { get; set; }
    }

    public class DataWrapper
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public PaginationWrapper Data { get; set; }
    }

    public class PaginationWrapper
    {
        public int Current_Page { get; set; }
        public List<StudentPayment> Data { get; set; }
        public int Total { get; set; }
    }

    public class StudentPayment
    {
        public string Payment_Code { get; set; }
        public string Student_Code { get; set; }
        public int Total_Amount { get; set; }
        public string Payment_Date { get; set; }
        public string Payment_Description { get; set; }
        public int Bank_Code { get; set; }
        public string Academic_Session { get; set; }
        public bool Is_Payment_Verified { get; set; }


    }
}
