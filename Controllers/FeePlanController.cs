using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ValueJetImport.Model;
using ValueJetImport.Helpers;

namespace FeePlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeePlanController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessFeePlan([FromBody] List<FeePlan> feePlans)
        {
            if (feePlans == null || !feePlans.Any())
                return BadRequest("No data provided.");

            // Process all feePlans without filtering
            var processedPlans = feePlans
                .SelectMany(plan => MapFeeItems(plan, FeeMappings.GetMapping(plan.Programme)))
                .ToList();

            var excelBytes = GenerateExcelFile(processedPlans);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FeePlan.xlsx");
        }



        private List<ProcessedFeePlan> MapFeeItems(FeePlan plan, (string FacultyCode, string DepartmentCode, string ProgramCode)? mappings)
        {
            if (mappings == null) return new List<ProcessedFeePlan>();

            var processedFees = new List<ProcessedFeePlan>();

            var feeItems = new List<(string FeeItemName, decimal Amount)>
                {
                    ("TUITION/EXAM/REGISTRATION", plan.TuitionExamRegistration),
                    ("ACCOMM", plan.Accomm),
                    ("UTILITY", plan.Utility),
                    ("ICT DEV FUND", plan.ICTDevelopmentFund),
                    ("LABS. W/SHOP &STUDIO", plan.LabsWorkshopStudio),
                    ("VIRTUAL LIBRARY FEE", plan.VirtualLibraryFee),
                    ("ICT TRAINING", plan.ICTTraining),
                    ("WIRELESS INTERNET", plan.WirelessInternet),
                    ("EDS", plan.EDS),
                    ("COLLEGE DUES", plan.CollegeDues),
                    ("FIELD TRIPS", plan.FieldTrips),
                    ("MATRIC", plan.Matric),
                    ("I. D CARD", plan.IDCard),
                    ("RESULT VER.", plan.ResultVerification),
                    ("TMC & VISION 10-2022 KIT", plan.TMCVisionKit),
                    ("CAUTION FEES", plan.CautionFees),
                    ("BOOK DEPOSIT", plan.BookDeposit),
                    ("MEDICAL DEPOSIT", plan.MedicalDeposit),
                    ("MEDICAL TEST", plan.MedicalTest),
                    ("LAB COAT/TOOL", plan.LabCoatTool),
                    ("CONVOCATION", plan.Convocation)
                };

            feeItems.ForEach(fee =>
            {
                processedFees.Add(new ProcessedFeePlan
                {
                    FeeItemCode = GetFeeItemCode(fee.FeeItemName), // Assuming you have a method to map FeeItemName to FeeItemCode
                    AcademicLevelCode = plan.Level.ToString(), // Set AcademicLevelCode as required
                    AcademicFacultyCode = mappings.Value.FacultyCode,
                    AcademicDepartmentCode = mappings.Value.DepartmentCode,
                    AcademicProgramCode = mappings.Value.ProgramCode,
                    Amount = fee.Amount
                });
            });

            return processedFees;
        }

        private string GetFeeItemCode(string feeItemName)
        {
            // This method maps fee item names to the appropriate FeeItemCode
            return feeItemName switch
            {
                "TUITION/EXAM/REGISTRATION" => "01",
                "ACCOMM" => "02",
                "UTILITY" => "20",
                "ICT DEV FUND" => "03",
                "LABS. W/SHOP &STUDIO" => "04",
                "VIRTUAL LIBRARY FEE" => "05",
                "ICT TRAINING" => "22",
                "WIRELESS INTERNET" => "06",
                "EDS" => "07",
                "COLLEGE DUES" => "08",
                "FIELD TRIPS" => "10",
                "MATRIC" => "25",
                "I. D CARD" => "09",
                "RESULT VER." => "15",
                "TMC & VISION 10-2022 KIT" => "26",
                "CAUTION FEES" => "23",
                "BOOK DEPOSIT" => "17",
                "MEDICAL DEPOSIT" => "24",
                "MEDICAL TEST" => "16",
                "LAB COAT/TOOL" => "21",
                "CONVOCATION" => "12",
                _ => "00" // Default code if no match is found
            };
        }

        private byte[] GenerateExcelFile(List<ProcessedFeePlan> processedPlans)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();

            // Create a single worksheet for all data
            var worksheet = package.Workbook.Worksheets.Add("FeePlanData");

            // Define headers
            var headers = new[]
            {
        "FeeItemCode", "AcademicLevelCode", "AcademicFacultyCode", "AcademicDepartmentCode",
        "AcademicProgramCode", "StudentType", "Amount", "SessionCode", "Period",
        "Currency", "InventoryAccount", "COGSAccount"
    };

            // Add headers to the first row
            for (var i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Populate data rows
            for (var i = 0; i < processedPlans.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = processedPlans[i].FeeItemCode;
                worksheet.Cells[i + 2, 2].Value = processedPlans[i].AcademicLevelCode;
                worksheet.Cells[i + 2, 3].Value = processedPlans[i].AcademicFacultyCode;
                worksheet.Cells[i + 2, 4].Value = processedPlans[i].AcademicDepartmentCode;
                worksheet.Cells[i + 2, 5].Value = processedPlans[i].AcademicProgramCode;
                worksheet.Cells[i + 2, 6].Value = "1"; // StudentType
                worksheet.Cells[i + 2, 7].Value = processedPlans[i].Amount;
                worksheet.Cells[i + 2, 8].Value = processedPlans[i].SessionCode;
                worksheet.Cells[i + 2, 9].Value = processedPlans[i].Period;
                worksheet.Cells[i + 2, 10].Value = "NGN"; // Currency
                worksheet.Cells[i + 2, 11].Value = "27000"; // InventoryAccount
                worksheet.Cells[i + 2, 12].Value = "10360"; // COGSAccount
            }

            return package.GetAsByteArray();
        }


    }
}
