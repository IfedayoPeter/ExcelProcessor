namespace ValueJetImport.Model
{
    public class ProcessedStudent
    {
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int StudentStatus { get; set; } = 1;
        public string Nationality { get; set; } = "Nigerian";
        public string StateOrigin { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Skype { get; set; } = "";
        public string Facebook { get; set; } = "";
        public string AccountEmail { get; set; } = "";
        public string StudentCode { get; set; }
        public string ProgramCode { get; set; }
        public string StreamCode { get; set; }
        public string StreamName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string FacultyCode { get; set; }
        public string AdmissionTypeCode { get; set; } = "";
        public string AdmissionTypeName { get; set; } = "";
        public string AcademicLevelCode { get; set; } = "";
        public string Photo { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string Currency { get; set; } = "";
        public string StudentType { get; set; } = "Local";
        public string SessionCode { get; set; } = "";
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string Period { get; set; } = "";
        public int AcademicStanding { get; set; } = 1;
        public bool Active { get; set; } = true;
        public bool IsLMS { get; set; } = true;
        public bool IsCheck { get; set; } = true;
    }
}
