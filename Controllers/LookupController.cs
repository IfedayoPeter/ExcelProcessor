using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ValueJetImport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LookupController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("export/{type}/{tenant}")]
        public async Task<IActionResult> ExportResourceAsync(string type, string tenant)
        {
            // Validate the resource type
            var validTypes = new[] { "programmes", "departments", "faculties", "fee_items" };
            if (!validTypes.Contains(type.ToLower()))
            {
                return BadRequest("Invalid resource type. Valid types are: programmes, departments, faculties, fee_items.");
            }

            // Validate the tenant type
            var validTenants = new[] { "undergraduate", "postgraduate" };
            if (!validTenants.Contains(tenant.ToLower()))
            {
                return BadRequest("Invalid tenant type. Valid tenants are: undergraduate, postgraduate.");
            }

            // Prepare the HTTP client
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://portal.binghamuni.edu.ng/integrations/api/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "HnKcqzjS10ZqAkfPl7IkFMf6zMFOh3iiwgjlQAgT7a3caa2c");
            client.DefaultRequestHeaders.Add("X-API-TENANT", tenant);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Call the endpoint
            var response = await client.GetAsync($"lookup/{type}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch {type}. Reason: {response.ReasonPhrase}");
            }

            // Deserialize the response JSON
            var data = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();

            // Export to Excel
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(type.ToUpper());
            var row = 1;

            if (data != null && data.Any())
            {
                // Write headers
                var headers = data.First().Keys.ToList();
                for (var col = 1; col <= headers.Count; col++)
                {
                    worksheet.Cell(row, col).Value = headers[col - 1];
                }

                // Write data
                foreach (var item in data)
                {
                    row++;
                    var col = 1;
                    foreach (var value in item.Values)
                    {
                        worksheet.Cell(row, col).Value = value?.ToString() ?? string.Empty;
                        col++;
                    }
                }
            }
            else
            {
                worksheet.Cell(1, 1).Value = "No data available.";
            }

            // Prepare the file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"{tenant}_{type}_data.xlsx";
            return File(stream, contentType, fileName);
        }
    }
}
