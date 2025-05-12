using Microsoft.AspNetCore.Mvc;
using ValueJetImport.Helpers;
using ValueJetImport.Model;

namespace Processor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesProcessController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessFees([FromBody] List<ExcelFeeInput> feeInputs)
        {
            var processedFees = new List<ProcessedFee>();

            foreach (var fee in feeInputs)
            {
                // Check if the programme exists in the DepartmentCodes dictionary
                if (!DepartmentMapping.DepartmentCodes.TryGetValue(fee.Programme, out var departmentFaculty))
                {
                    return BadRequest($"Programme '{fee.Programme}' not found in department mapping.");
                }

                // Destructure the department and faculty code
                var (programcode, deptCode, facultyCode) = departmentFaculty;

                // Create processed fees for each fee item
                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTCTER",
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.TuitionExamRegistration
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTCACC",
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.Accommodation
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTCUTY",
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.Utility
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTCIDF",
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.IctDevFund
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTCLWS",  // Labs W/Shop & Studio
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.LabsWorkshopStudio
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSVLF",  // Virtual Library Fee
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.VirtualLibraryFee
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSCIT",  // CIT Training
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.CitTraining
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSWIT",  // Wireless Internet
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.WirelessInternet
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSEDS",  // EDS
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.Eds
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSCDS",  // College Dues
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.CollegeDues
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUASSFTS",  // Field Trips
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.FieldTrips
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTMAT",  // Matric
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.Matric
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTIDC",  // I.D Card
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.IdCard
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTRVT",  // Result Verification
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.ResultVerification
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTTVK",  // TMC & Vision 10-2022 Kit
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.TmcVisionKit
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTCFS",  // Caution Fees
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.CautionFees
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTBDT",  // Book Deposit
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.BookDeposit
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTMDT",  // Medical Deposit
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.MedicalDeposit
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTMET",  // Medical Test
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.MedicalTest
                });

                processedFees.Add(new ProcessedFee
                {
                    FeeItemCode = "CUTOTLCT",  // Lab Coat/Tool
                    AcademicDepartmentCode = deptCode,
                    AcademicFacultyCode = facultyCode,
                    StreamCode = deptCode,
                    Amount = fee.LabCoatTool
                });

            }

            // Return the processed data as JSON
            return Ok(processedFees);
        }
    }
}
