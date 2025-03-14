namespace ValueJetImport.Model
{
    public class BinghamProcessedFeePlan
    {
        public string FeeItemCode { get; set; }
        public string AcademicLevelCode { get; set; }
        public string AcademicFacultyCode { get; set; }
        public string AcademicDepartmentCode { get; set; }
        public string AcademicProgramCode { get; set; }
        public int StudentType { get; set; }
        public decimal Amount { get; set; }
        public string SessionCode { get; set; }
        public string Period { get; set; }
        public string Currency { get; set; }
    }
}
