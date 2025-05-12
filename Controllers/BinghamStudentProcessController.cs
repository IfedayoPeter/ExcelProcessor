using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueJetImport.Helpers;
using ValueJetImport.Model;

namespace Processor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinghamStudentProcessController : ControllerBase
    {


        [HttpPost("bingham_student")]
        public IActionResult ProcessBinghamStudent([FromBody] List<Student> students)
        {
            var processedStudents = new List<ProcessedStudent>();
            var skippedStudents = new List<Student>();

            foreach (var student in students)
            {
                // Only process students with level 100
                //if (student.Department != "ARC" && student.Department != "ACC")
                //    continue;

                // Split student name into first, middle, and last names
                var names = student.StudentName.Split(' ');
                string firstName = names.Length > 0 ? names[0] : "";
                string middleName = names.Length > 2 ? names[1] : "";
                string lastName = names.Length > 1 ? names[^1] : "";

                // Initialize department and faculty codes
                string deptCode;
                string facultyCode;
                string programCode;

                var departmentEntry = BinghamDepartmentMapping.DepartmentCodes
                .FirstOrDefault(entry => entry.Value.DeptCode == student.Department);

                if (BinghamDepartmentMapping.DepartmentCodes.ContainsKey(student.Program)) // Fallback to Program field
                {
                    deptCode = BinghamDepartmentMapping.DepartmentCodes[student.Program].DeptCode;
                    facultyCode = BinghamDepartmentMapping.DepartmentCodes[student.Program].FacultyCode;
                    programCode = BinghamDepartmentMapping.DepartmentCodes[student.Program].ProgramCode;

                    // Optionally log or indicate the override
                    // e.g., Console.WriteLine($"Department '{student.Department}' not found. Overriding with program '{student.Program}' data.");
                }
                // Check if a matching department was found
                else if (!string.IsNullOrEmpty(departmentEntry.Key))
                {
                    deptCode = departmentEntry.Value.DeptCode;
                    facultyCode = departmentEntry.Value.FacultyCode;
                    programCode = departmentEntry.Value.ProgramCode;
                }

                else
                {
                    // If neither Department nor Program is found, you can handle it accordingly
                    // Here, we will skip the student or throw an error
                    // return BadRequest($"Neither the department '{student.Department}' nor the program '{student.Program}' is valid.");
                    skippedStudents.Add(student);
                    continue;
                }

                // Create processed student object
                var processedStudent = new ProcessedStudent
                {
                    AccountName = student.RegistrationNumber.ToString(),
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    StudentCode = student.MatricNo,
                    ProgramCode = programCode,
                    StreamCode = programCode,
                    StreamName = student.Program,
                    DepartmentCode = deptCode,
                    DepartmentName = student.Program,
                    FacultyCode = facultyCode,
                    AcademicLevelCode = student.Level.ToString(),
                    ClassCode = student.Level.ToString(),
                    CourseCode = programCode,
                    StudentType = "Local",  // Default StudentType as 1
                    Nationality = "Nigerian"  // Default nationality as Nigerian
                };

                processedStudents.Add(processedStudent);
            }

            // Return both processed and skipped students
            return Ok(new
            {
                ProcessedStudents = processedStudents,
                SkippedStudents = skippedStudents
            });
        }

        [HttpPost("student")]
        public IActionResult ProcessStudentData([FromBody] List<Student> students)
        {
            var processedStudents = new List<ProcessedStudent>();
            var skippedStudents = new List<Student>();

            foreach (var student in students)
            {
                // Split student name into first, middle, and last names
                var names = student.StudentName.Split(' ');
                string firstName = names.Length > 0 ? names[0] : "";
                string middleName = names.Length > 2 ? names[1] : "";
                string lastName = names.Length > 1 ? names[^1] : "";

                // Initialize department and faculty codes
                string deptCode;
                string facultyCode;
                string programCode;

                var departmentEntry = DepartmentMapping.DepartmentCodes
                    .FirstOrDefault(entry => entry.Value.DeptCode == student.Department);

                if (DepartmentMapping.DepartmentCodes.ContainsKey(student.Program)) // Fallback to Program field
                {
                    deptCode = DepartmentMapping.DepartmentCodes[student.Program].DeptCode;
                    facultyCode = DepartmentMapping.DepartmentCodes[student.Program].FacultyCode;
                    programCode = DepartmentMapping.DepartmentCodes[student.Program].ProgramCode;
                }
                else if (!string.IsNullOrEmpty(departmentEntry.Key)) // Check if a matching department was found
                {
                    deptCode = departmentEntry.Value.DeptCode;
                    facultyCode = departmentEntry.Value.FacultyCode;
                    programCode = departmentEntry.Value.ProgramCode;
                }
                else
                {
                    // Add to skipped students and continue with the next iteration
                    skippedStudents.Add(student);
                    continue;
                }

                // Create processed student object
                var processedStudent = new ProcessedStudent
                {
                    AccountName = student.RegistrationNumber.ToString(),
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    StudentCode = student.MatricNo.ToString(),
                    ProgramCode = programCode,
                    StreamCode = programCode,
                    StreamName = student.Program,
                    DepartmentCode = deptCode,
                    DepartmentName = student.Program,
                    FacultyCode = facultyCode,
                    AcademicLevelCode = student.Level.ToString(),
                    ClassCode = student.Level.ToString(),
                    CourseCode = programCode,
                    StudentType = "Local",  // Default StudentType as 1
                    Nationality = "Nigerian"  // Default nationality as Nigerian
                };

                processedStudents.Add(processedStudent);
            }

            // Return both processed and skipped students
            return Ok(new
            {
                ProcessedStudents = processedStudents,
                SkippedStudents = skippedStudents
            });
        }

    }
}
