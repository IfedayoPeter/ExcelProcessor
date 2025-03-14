namespace ValueJetImport.Helpers
{
    public static class BinghamFeeMappings
    {
        public static readonly Dictionary<string, (string FacultyCode, string DepartmentCode, string ProgramCode)> Mappings =
        new()
        {
        { "ACC", ("ADM", "ACC", "ACC") }, // Accounting
        { "ANA", ("BMS", "ANA", "ANA") }, // Anatomy
        { "ARC", ("ENV", "ARC", "ARC") }, // Architecture
        { "BCH", ("SCI", "BCH", "BCH") }, // Biochemistry
        { "BST", ("ART", "BST", "BST") }, // Bible Studies
        { "MIC", ("SCI", "MIC", "MIC") }, // Biological Sciences
        { "BUS", ("ADM", "BUS", "BUS") }, // Business Administration
        { "CHM", ("SCI", "CHM", "CHM") }, // Chemical Science
        { "REL", ("ART", "REL", "REL") }, // Christian Theology
        { "CMP", ("SCI", "CMP", "CMP") }, // Computer Science
        { "CSE", ("SCI", "CSE", "CSE") }, // Cyber Security
        { "ECO", ("SMS", "ECO", "ECO") }, // Economics
        { "ENG", ("ART", "ENG", "ENG") }, // English
        { "ENT", ("ADM", "ENT", "ENT") }, // Entrepreneurship
        { "ENV", ("ENV", "ENV", "ENV") }, // Environmental Management
        { "EST", ("ENV", "EST", "EST") }, // Estate Management
        { "GAC", ("EDU", "GAC", "GAC") }, // Guidance and Counseling
        { "GST", ("EDU", "GST", "GST") }, // General Studies
        { "IMT", ("SCI", "IMT", "IMT") }, // Industrial Mathematics
        { "ITE", ("SCI", "ITE", "ITE") }, // Information Technology
        { "LAN", ("ARC", "LAN", "LAN") }, // Landscape Architecture
        { "LAW", ("LAW", "LAW", "LAW") }, // Law
        { "LIS", ("SMS", "LIS", "LIS") }, // Library and Information Science
        { "SCM", ("ADM", "SCM", "SCM") }, // Supply Chain Management
        { "MAC", ("CMS", "MAC", "MAC") }, // Mass Communication
        { "MTH", ("SCI", "MTH", "MTH") }, // Mathematics
        { "MLS", ("FHS", "MLS", "MLS") }, // Medical Laboratory Science
        { "MBBS", ("CLS", "MBBS", "MBBS") }, // Medicine and Surgery
        { "NUR", ("FHS", "NUR", "NUR") }, // Nursing
        { "OPT", ("FHS", "OPT", "OPT") }, // Optometry
        { "PHA", ("PHA", "PHA", "PHA") }, // Pharmacy
        { "PHI", ("ART", "PHI", "PHI") }, // Philosophy
        { "PHY", ("SCI", "PHY", "PHY") }, // Physics
        { "PHS", ("BMS", "PHS", "PHS") }, // Physiology
        { "POL", ("SMS", "POL", "POL") }, // Political Science
        { "PMT", ("ADM", "PMT", "PMT") }, // Procurement Management
        { "PSC", ("EDU", "PSC", "PSC") }, // Psychology
        { "PUB", ("CLS", "PUB", "PUB") }, // Public Health
        { "QUA", ("ENV", "QUA", "QUA") }, // Quantity Surveying
        { "RAD", ("FHS", "RAD", "RAD") }, // Radiography
        { "SOC", ("SMS", "SOC", "SOC") }  // Sociology
        };

        public static (string FacultyCode, string DepartmentCode, string ProgramCode)? GetMapping(string programCode) =>
            Mappings.TryGetValue(programCode, out var mapping) ? mapping : null;
    }

}
