using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ValueJetImport.Model;
using ValueJetImport.Helpers;

namespace FeePlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinghamFeePlanController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessFeePlan([FromBody] List<BinghamFeePlan> feePlans)
        {
            if (feePlans == null || !feePlans.Any())
                return BadRequest("No data provided.");

            // Process all feePlans without filtering
            var processedPlans = feePlans
                .SelectMany(plan => MapFeeItems(plan, BinghamFeeMappings.GetMapping(plan.Programme)))
                .ToList();

            var excelBytes = GenerateExcelFile(processedPlans);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FeePlan.xlsx");
        }



        private List<BinghamProcessedFeePlan> MapFeeItems(BinghamFeePlan plan, (string FacultyCode, string DepartmentCode, string ProgramCode)? mappings)
        {
            if (mappings == null) return new List<BinghamProcessedFeePlan>();

            var processedFees = new List<BinghamProcessedFeePlan>();

            var feeItems = new List<(string FeeItemName, decimal Amount)>
                {
                    ("Tuition", plan.Tuition),
                    ("MaintenanceFees", plan.MaintenanceFees),
                    ("DevelopmentLevy", plan.DevelopmentLevy),
                    ("ExaminationFees", plan.ExaminationFees),
                    ("LibraryServices", plan.LibraryServices),
                    ("RegistrationCharges", plan.RegistrationCharges),
                    ("Games", plan.Games),
                    ("ICTServices", plan.ICTServices),
                    ("EntrepreneurFees", plan.EntrepreneurFees),
                    ("StudioCharges", plan.StudioCharges),
                    ("TIHIP", plan.TIHIP),
                    ("NewHostel", plan.NewHostel),
                };

            feeItems.ForEach(fee =>
            {
                processedFees.Add(new BinghamProcessedFeePlan
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
                "Tuition" => "01",
                "MaintenanceFees" => "02",
                "DevelopmentLevy" => "03",
                "ExaminationFees" => "04",
                "LibraryServices" => "05",
                "RegistrationCharges" => "06",
                "Games" => "07",
                "ICTServices" => "08",
                "EntrepreneurFees" => "09",
                "StudioCharges" => "10",
                "TIHIP" => "11",
                "NewHostel" => "12",
                _ => "00" // Default code if no match is found
            };
        }

        private byte[] GenerateExcelFile(List<BinghamProcessedFeePlan> processedPlans)
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
        "Currency",
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
            }

            return package.GetAsByteArray();
        }


    }
}
