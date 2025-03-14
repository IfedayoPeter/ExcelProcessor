namespace ValueJetImport.Model
{
    public class ProcessedFee
    {
        public string FeeItemCode { get; set; }
        public string AcademicLevelCode { get; set; } = "LVL100";  // Assume default level is 100
        public string AcademicFacultyCode { get; set; }
        public string AcademicDepartmentCode { get; set; }
        public string StreamCode { get; set; }
        public string StudentType { get; set; } = "1"; // Default to 1
        public decimal Amount { get; set; }
        public string SessionCode { get; set; } = "";  // Can be set later if necessary
        public string Period { get; set; } = "";  // Can be set later if necessary
        public string Currency { get; set; } = "NGN";  // Default currency
        public string RevenueAccount { get; set; } = "";  // Optional for now
    }
}
