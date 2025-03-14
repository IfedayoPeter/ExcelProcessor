using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;

namespace ValueJetImport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly string[] ValidTenants = { "undergraduate", "postgraduate" };

        public StudentController(IHttpClientFactory httpClientFactory)
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

        [HttpGet("students")]
        public async Task<IActionResult> GetStudentsAsync(
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
                var students = await FetchAllStudentsAsync(tenant, filter, sort, limit);

                if (students == null || !students.Any())
                {
                    return NoContent(); // Return No Content if no students found
                }

                var fileContent = GenerateExcelFile(students, tenant);

                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{tenant}_students_data.xlsx");
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

        private async Task<List<Student>> FetchStudentsAsync(string tenant, string filter, string sort, int limit)
        {
            var client = ConfigureHttpClient(tenant);
            var queryParameters = BuildQueryParameters(filter, sort, limit);
            var queryString = string.Join("&", queryParameters);

            var response = await client.GetAsync($"students?{queryString}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch students. Reason: {response.ReasonPhrase}");
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return apiResponse?.Data?.Data.Data ?? new List<Student>();

        }

        private async Task<List<Student>> FetchAllStudentsAsync(string tenant, string filter, string sort, int limit)
        {
            List<Student> students = [];

            var client = ConfigureHttpClient(tenant);
            var queryParameters = BuildQueryParameters(filter, sort, limit);
            var queryString = string.Join("&", queryParameters);

            int totalPages = 0;
            int totalStudents = 0;

            var response = await client.GetAsync($"students?{queryString}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch students. Reason: {response.ReasonPhrase}");
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

            var data = apiResponse?.Data?.Data;

            if (data is not null)
            {
                totalPages = data.last_page;
                totalStudents = data.total;
            };

            if (totalPages > 1)
            {
                try
                {
                    for (int i = 1; i <= totalPages; i++)
                    {
                        response = await client.GetAsync($"students?{queryString}&page={i}");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Failed to fetch students. Reason: {response.ReasonPhrase}");
                        }

                        apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                        students.AddRange(apiResponse?.Data?.Data.Data);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            //if (!totalStudents.Equals(students.Count))
            //{
            //    throw new Exception("Students Fetched not complete");
            //}

            return students;

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

        private MemoryStream GenerateExcelFile(List<Student> students, string tenant)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");

            WriteHeaders(worksheet);
            WriteStudentData(worksheet, students);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        private void WriteHeaders(IXLWorksheet worksheet)
        {
            var headers = new List<string>
            {
                "Account Number", "Student Number", "First Name", "Last Name", "Other Name", "Gender", "Student Type",
                "Student Status", "Student Level", "Programme Code", "Programme Name", "Department Code", "Department Name",
                "Faculty Code", "Faculty Name", "Academic Session", "Academic Standing"
            };

            for (var col = 1; col <= headers.Count; col++)
            {
                worksheet.Cell(1, col).Value = headers[col - 1];
            }
        }

        private void WriteStudentData(IXLWorksheet worksheet, List<Student> students)
        {
            var row = 2;
            foreach (var student in students)
            {
                row++;
                var col = 1;
                worksheet.Cell(row, col++).Value = student.account_number;
                worksheet.Cell(row, col++).Value = student.student_number;
                worksheet.Cell(row, col++).Value = student.first_name;
                worksheet.Cell(row, col++).Value = student.last_name;
                worksheet.Cell(row, col++).Value = student.other_name;
                worksheet.Cell(row, col++).Value = student.gender;
                worksheet.Cell(row, col++).Value = student.student_type;
                worksheet.Cell(row, col++).Value = student.student_status;
                worksheet.Cell(row, col++).Value = student.student_level;
                worksheet.Cell(row, col++).Value = student.Programme?.Code;
                worksheet.Cell(row, col++).Value = student.Programme?.Name;
                worksheet.Cell(row, col++).Value = student.Department?.Code;
                worksheet.Cell(row, col++).Value = student.Department?.Name;
                worksheet.Cell(row, col++).Value = student.Faculty?.Code;
                worksheet.Cell(row, col++).Value = student.Faculty?.Name;
                worksheet.Cell(row, col++).Value = student.academic_session;
                //worksheet.Cell(row, col++).Value = student.academic_standing;
            }
        }

        // Models for Deserialization
        public class Programme
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class Department
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class Faculty
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class Student
        {
            public string account_number { get; set; }
            public string student_number { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string other_name { get; set; }
            public string gender { get; set; }
            public int student_type { get; set; }
            public int student_status { get; set; }
            public string student_level { get; set; }
            public Programme Programme { get; set; }
            public Department Department { get; set; }
            public Faculty Faculty { get; set; }
            public string academic_session { get; set; }
            //public byte academic_standing { get; set; }
        }

        public class ApiResponse
        {
            public bool Success { get; set; }
            public int Code { get; set; }
            public Data1 Data { get; set; }
        }

        public class Data1
        {
            public ApiData Data { get; set; }
        }

        public class ApiData
        {
            public int CurrentPage { get; set; }
            public int last_page { get; set; }
            public int total { get; set; }
            public List<Student> Data { get; set; }
        }
    }
}
