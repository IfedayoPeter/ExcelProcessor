namespace ValueJetImport.Helpers
{
    public static class FeeMappings
    {
        public static readonly Dictionary<string, (string FacultyCode, string DepartmentCode, string ProgramCode)> Mappings =
    new()
    {
        { "ACCO", ("01", "ACC", "ACCO") }, // Accounting
        { "ARCH", ("04", "ARC", "ARCH") }, // Architecture
        { "BIOC", ("04", "BCM", "BIOC") }, // Biochemistry
        { "BANK", ("01", "BFN", "BANK") }, // Banking and Finance
        { "FINA", ("01", "BFN", "FINA") }, // Finance
        { "BIOL", ("04", "BIO", "BIOL") }, // Biology
        { "MICR", ("04", "BIO", "MICR") }, // Microbiology
        { "BTCH", ("04", "BTE", "BTCH") }, // Building Technology
        { "BUIL", ("04", "BTE", "BUIL") }, // Building
        { "BUSI", ("01", "BUS", "BUSI") }, // Business Administration
        { "INHR", ("01", "BUS", "INHR") }, // Industrial Relations and HR Management
        { "MARK", ("01", "BUS", "MARK") }, // Marketing
        { "CHEM", ("02", "CHE", "CHEM") }, // Chemical Engineering
        { "ICHM", ("04", "CHM", "ICHM") }, // Industrial Chemistry
        { "CMPS", ("04", "CSI", "CMPS") }, // Computer Science
        { "MANA", ("04", "CSI", "MANA") }, // Management Information System
        { "CIVI", ("02", "CVE", "CIVI") }, // Civil Engineering
        { "ECON", ("01", "ECO", "ECON") }, // Economics
        { "CMPE", ("02", "ELE", "CMPE") }, // Computer Engineering
        { "ELEC", ("02", "ELE", "ELEC") }, // Electrical and Electronics Engineering
        { "INFO", ("02", "ELE", "INFO") }, // Information and Communication Engineering
        { "ESTA", ("04", "ESM", "ESTA") }, // Estate Management
        { "ENGL", ("03", "LNG", "ENGL") }, // English
        { "MASS", ("01", "MAS", "MASS") }, // Mass Communication
        { "IMAT", ("04", "MTS", "IMAT") }, // Industrial Mathematics
        { "MECH", ("02", "MEE", "MECH") }, // Mechanical Engineering
        { "PETR", ("02", "PET", "PETR") }, // Petroleum Engineering
        { "IPHY", ("04", "PHY", "IPHY") }, // Industrial Physics
        { "INTE", ("03", "PSI", "INTE") }, // International Relations
        { "PASS", ("03", "PSI", "PASS") }, // Policy and Strategic Studies
        { "POLI", ("03", "PSI", "POLI") }, // Political Science
        { "PSYC", ("03", "PSY", "PSYC") }, // Psychology
        { "SOCI", ("01", "SOC", "SOCI") }  // Sociology
    };

        public static (string FacultyCode, string DepartmentCode, string ProgramCode)? GetMapping(string programCode) =>
            Mappings.TryGetValue(programCode, out var mapping) ? mapping : null;

    }
}