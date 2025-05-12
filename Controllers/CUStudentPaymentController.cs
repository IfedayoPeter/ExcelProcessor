using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Processor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CUStudentPaymentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string BaseUrl = "https://studentaccount.covenantuniversity.edu.ng/api/paymentroute.php";
        private const string ApiKey = "e3f8a1c9b75a4d10e3d2f9b1c8e07a69f25c7d8a3e5b4c8f6d1e2f3c4b5a6d7e";

        public CUStudentPaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Fetch Payments
        [HttpGet("fetch-payments")]
        public async Task<IActionResult> FetchPaymentsAsync(
            [FromQuery] string? StudentCode,
            [FromQuery] string? date_from,
            [FromQuery] string? date_to,
            [FromQuery] string? bank,
            [FromQuery] int? limit)
        {
            var queryParams = new List<string> { "type=payments" };

            if (!string.IsNullOrEmpty(StudentCode)) queryParams.Add($"StudentCode={StudentCode}");
            if (!string.IsNullOrEmpty(date_from)) queryParams.Add($"date_from={date_from}");
            if (!string.IsNullOrEmpty(date_to)) queryParams.Add($"date_to={date_to}");
            if (!string.IsNullOrEmpty(bank)) queryParams.Add($"bank={bank}");
            if (limit.HasValue) queryParams.Add($"limit={limit}");

            string queryString = string.Join("&", queryParams);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

            var response = await client.GetAsync($"{BaseUrl}?{queryString}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch payments. Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            // Debugging - Log response
            //Console.WriteLine("Raw API Response: " + responseContent);
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return BadRequest("API returned an empty response.");
            }
            

            try
            {
                // Deserialize JSON
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var payments = apiResponse?.Data ?? new List<StudentPayment>();
                if (!payments.Any())
                {
                    return BadRequest("No payment records found.");
                }

                // Generate Excel File
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Student Payments");

                // Headers
                var headers = new[] { "Student Code", "Total Amount", "Payment Reference", "Payment Code", "Payment Date", "Bank ID" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                }

                // Populate Data
                int row = 2;
                foreach (var payment in payments)
                {
                    worksheet.Cell(row, 1).Value = payment.StudentCode;
                    worksheet.Cell(row, 2).Value = payment.TotalAmount;
                    worksheet.Cell(row, 3).Value = payment.PaymentReference;
                    worksheet.Cell(row, 4).Value = payment.PaymentCode;
                    worksheet.Cell(row, 5).Value = payment.PaymentDate;
                    worksheet.Cell(row, 6).Value = payment.BankId;
                    row++;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                // Save Excel to memory stream
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                // Return Excel File
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentPayments.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing data: {ex.Message}");
            }
        }

        // Fetch Billing Details
        [HttpGet("fetch-billing")]
        public async Task<IActionResult> FetchBillingAsync(
            [FromQuery] string? StudentCode,
            [FromQuery] string? session,
            [FromQuery] string? date_from,
            [FromQuery] string? date_to,
            [FromQuery] int? limit)
        {
            var queryParams = new List<string>
            {
                "type=billing"
            };

            if (!string.IsNullOrEmpty(StudentCode)) queryParams.Add($"StudentCode={StudentCode}");
            if (!string.IsNullOrEmpty(session)) queryParams.Add($"session={session}");
            if (!string.IsNullOrEmpty(date_from)) queryParams.Add($"date_from={date_from}");
            if (!string.IsNullOrEmpty(date_to)) queryParams.Add($"date_to={date_to}");
            if (limit.HasValue) queryParams.Add($"limit={limit}");

            string queryString = string.Join("&", queryParams);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

            var response = await client.GetAsync($"{BaseUrl}?{queryString}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch billing details. Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return Content(responseContent, "application/json");
        }


        // Response Models
        public class ApiResponse
        {
            public List<StudentPayment> Data { get; set; }
        }

        public class StudentPayment
        {
            public string StudentCode { get; set; }
            public decimal TotalAmount { get; set; }
            public string? PaymentReference { get; set; }
            public int PaymentCode { get; set; }
            public string PaymentDate { get; set; }
            public string? BankId { get; set; }
        }

    }
}
