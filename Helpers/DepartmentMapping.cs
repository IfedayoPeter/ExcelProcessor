namespace ValueJetImport.Helpers
{
    public static class DepartmentMapping
    {
        public static Dictionary<string, (string ProgramCode, string DeptCode, string FacultyCode)> DepartmentCodes = new Dictionary<string, (string, string, string)>
        {
            { "Accounting", ("ACCO", "ACC", "01") },
            { "Architecture", ("ARCH", "ARC", "04") },
            { "Biochemistry", ("BIOC", "BCM", "04") },
            { "Banking and Finance", ("BANK", "BFN", "01") },
            { "Finance", ("FINA", "BFN", "01") },
            { "Biology", ("BIOL", "BIO", "04") },
            { "Microbiology", ("MICR", "BIO", "04") },
            { "Building Technology", ("BTCH", "BTE", "04") },
            { "Building", ("BTCH", "BTE", "04") },
            { "Business Administration", ("BUSI", "BUS", "01") },
            { "Industrial Relations and Human Resource Management", ("INHR", "BUS", "01") },
            { "Marketing", ("MARK", "BUS", "01") },
            { "Chemical Engineering", ("CHEM", "CHE", "02") },
            { "Industrial Chemistry", ("ICHM", "CHM", "04") },
            { "Computer Science", ("CMPS", "CSI", "04") },
            { "Management Information System", ("MANA", "CSI", "04") },
            { "Civil Engineering", ("CIVI", "CVE", "02") },
            { "Economics", ("ECON", "ECO", "01") },
            { "Computer Engineering", ("CMPE", "ELE", "02") },
            { "Electrical and Electronics Engineering", ("ELEC", "ELE", "02") },
            { "Information and Communication Engineering", ("INFO", "ELE", "02") },
            { "Estate Management", ("ESTA", "ESM", "04") },
            { "English", ("ENGL", "LNG", "03") },
            { "Mass Communication", ("MASS", "MAS", "01") },
            { "Industrial Mathematics", ("IMAT", "MTS", "04") },
            { "Mechanical Engineering", ("MECH", "MEE", "02") },
            { "Petroleum Engineering", ("PETR", "PET", "02") },
            { "Industrial Physics", ("IPHY", "PHY", "04") },
            { "International Relations", ("INTE", "PSI", "03") },
            { "Policy and Strategic Studies", ("PASS", "PSI", "03") },
            { "Political Science", ("POLI", "PSI", "03") },
            { "Psychology", ("PSYC", "PSY", "03") },
            { "Sociology", ("SOCI", "SOC", "01") }
        };
    }


}
