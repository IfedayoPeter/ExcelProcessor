namespace ValueJetImport.Helpers
{
    public static class BinghamDepartmentMapping
    {
        public static Dictionary<string, (string ProgramCode, string DeptCode, string FacultyCode)> DepartmentCodes = new Dictionary<string, (string, string, string)>
    {
        { "ACCOUNTING", ("ACC", "ACC", "ADM") },
        { "ANATOMY", ("ANA", "ANA", "BMS") },
        { "ARCHITECTURE", ("ARC", "ARC", "ENV") },
        { "BIOCHEMISTRY", ("BCH", "BCH", "SCI") },
        { "BIBLE STUDIES", ("BST", "BST", "ART") },
        { "BIOLOGICAL SCIENCES", ("MIC", "MIC", "SCI") },
        { "BUSINESS ADMINISTRATION", ("BUS", "BUS", "ADM") },
        { "CHEMICAL SCIENCE", ("CHM", "CHM", "SCI") },
        { "CHRISTIAN THEOLOGY", ("REL", "REL", "ART") },
        { "COMPUTER SCIENCE", ("CMP", "CMP", "SCI") },
        { "CYBER SECURITY", ("CSE", "CSE", "SCI") },
        { "ECONOMICS", ("ECO", "ECO", "SMS") },
        { "ENGLISH", ("ENG", "ENG", "ART") },
        { "ENTREPRENEURSHIP", ("ENT", "ENT", "ADM") },
        { "ENVIRONMENTAL MANAGEMENT", ("ENV", "ENV", "ENV") },
        { "ESTATE MANAGEMENT", ("EST", "EST", "ENV") },
        { "GUIDANCE AND COUNSELING", ("GAC", "GAC", "EDU") },
        { "GENERAL STUDIES", ("GST", "GST", "EDU") },
        { "INDUSTRIAL MATHEMATICS", ("IMT", "IMT", "SCI") },
        { "INFORMATION TECHNOLOGY", ("ITE", "ITE", "SCI") },
        { "LANDSCAPE ARCHITECTURE", ("LAN", "LAN", "ARC") },
        { "LAW", ("LAW", "LAW", "LAW") },
        { "LIBRARY AND INFORMATION SCIENCE", ("LIS", "LIS", "SMS") },
        { "SUPPLY CHAIN MANAGEMENT", ("SCM", "SCM", "ADM") },
        { "MASS COMMUNICATION", ("MAC", "MAC", "CMS") },
        { "MATHEMATICS", ("MTH", "MTH", "SCI") },
        { "MEDICAL LABORATORY SCIENCE", ("MLS", "MLS", "FHS") },
        { "MEDICINE AND SURGERY", ("MBBS", "MBBS", "CLS") },
        { "NURSING", ("NUR", "NUR", "FHS") },
        { "OPTOMETRY", ("OPT", "OPT", "FHS") },
        { "PHARMACY", ("PHA", "PHA", "PHA") },
        { "PHILOSOPHY", ("PHI", "PHI", "ART") },
        { "PHYSICS", ("PHY", "PHY", "SCI") },
        { "PHYSIOLOGY", ("PHS", "PHS", "BMS") },
        { "POLITICAL SCIENCE", ("POL", "POL", "SMS") },
        { "PROCUREMENT MANAGEMENT", ("PMT", "PMT", "ADM") },
        { "PSYCHOLOGY", ("PSC", "PSC", "EDU") },
        { "PUBLIC HEALTH", ("PUB", "PUB", "CLS") },
        { "QUANTITY SURVEYING", ("QUA", "QUA", "ENV") },
        { "RADIOGRAPHY", ("RAD", "RAD", "FHS") },
        { "SOCIOLOGY", ("SOC", "SOC", "SMS") }
    };
    }


}
