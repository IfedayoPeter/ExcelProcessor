using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using ValueJetImport.Model;

[ApiController]
[Route("api/process")]
public class ValueJetInvoiceController : ControllerBase
{
    [HttpPost("transactions")]
    public IActionResult ProcessInvoice([FromBody] List<KiuTransaction> inputData)
    {
        var invoiceList = new List<ProcessedInvoice>();
        var invoiceDetailsList = new List<InvoiceDetails>();
        int cntItem = 1; // Initialize CNTITEM for InvoiceDetails

        // Process Invoice and InvoiceDetails data
        foreach (var transaction in inputData)
        {
            // Process the invoice
            var processedInvoice = new ProcessedInvoice
            {
                IDINVC = $"{transaction.Origin}-{transaction.FlightDate:MM/dd/yyyy}",
                INVCDESC = $"{transaction.Origin}-{transaction.FlightDate:MM/dd/yyyy}",
                DATEINVC = $"{transaction.FlightDate:MM/dd/yyyy}",
                DATEASOF = $"{transaction.FlightDate:MM/dd/yyyy}",
                DATEDUE = $"{transaction.FlightDate:MM/dd/yyyy}",
                DATERATE = $"{transaction.FlightDate:MM/dd/yyyy}",
                DATEBUS = $"{transaction.FlightDate:MM/dd/yyyy}",
            };

            invoiceList.Add(processedInvoice);

            // Process the invoice details (4 rows per transaction)
            invoiceDetailsList.Add(new InvoiceDetails
            {
                CNTITEM = cntItem,
                CNTLINE = 20,
                IDITEM = "F_FA",
                TEXTDESC = "PAX FARE",
                AMTPRIC = transaction.SumOfAmount,
                AMTEXTN = transaction.SumOfAmount,
                AMTTXBL = transaction.SumOfAmount,
                DISTNETHC = transaction.SumOfAmount,
                AMTPRICHC = transaction.SumOfAmount,
                AMTEXTNHC = transaction.SumOfAmount,
                IDACCTREV = 70000,
                IDACCTINV = 70000,
                IDACCTCOGS = 70000,
            });

            invoiceDetailsList.Add(new InvoiceDetails
            {
                CNTITEM = cntItem,
                CNTLINE = 40,
                IDITEM = "T_NG",
                TEXTDESC = "NG TAX",
                AMTPRIC = transaction.Ng,
                AMTEXTN = transaction.Ng,
                AMTTXBL = transaction.Ng,
                DISTNETHC = transaction.Ng,
                AMTPRICHC = transaction.Ng,
                AMTEXTNHC = transaction.Ng,
                IDACCTREV = 70002,
                IDACCTINV = 70002,
                IDACCTCOGS = 70002,
            });

            invoiceDetailsList.Add(new InvoiceDetails
            {
                CNTITEM = cntItem,
                CNTLINE = 60,
                IDITEM = "T_QT",
                TEXTDESC = "QT PASSENGER SERVICE CHARGE",
                AMTPRIC = transaction.Qt,
                AMTEXTN = transaction.Qt,
                AMTTXBL = transaction.Qt,
                DISTNETHC = transaction.Qt,
                AMTPRICHC = transaction.Qt,
                AMTEXTNHC = transaction.Qt,
                IDACCTREV = 70104,
                IDACCTINV = 70104,
                IDACCTCOGS = 70104,
            });

            invoiceDetailsList.Add(new InvoiceDetails
            {
                CNTITEM = cntItem,
                CNTLINE = 80,
                IDITEM = "T_YQ",
                TEXTDESC = "YQ FUEL SURCHARGE",
                AMTPRIC = transaction.SumOfYQ,
                AMTEXTN = transaction.SumOfYQ,
                AMTTXBL = transaction.SumOfYQ,
                DISTNETHC = transaction.SumOfYQ,
                AMTPRICHC = transaction.SumOfYQ,
                AMTEXTNHC = transaction.SumOfYQ,
                IDACCTREV = 70001,
                IDACCTINV = 70001,
                IDACCTCOGS = 70001,
            });

            cntItem++; // Increment CNTITEM
        }

        // Create Excel file with two sheets
        using (var workbook = new XLWorkbook())
        {
            // Add "Invoice" sheet
            var invoiceSheet = workbook.Worksheets.Add("Invoice");
            invoiceSheet.Cell(1, 1).InsertTable(invoiceList);

            // Add "Invoice Details" sheet
            var invoiceDetailsSheet = workbook.Worksheets.Add("InvoiceDetails");
            invoiceDetailsSheet.Cell(1, 1).InsertTable(invoiceDetailsList);

            // Save to a MemoryStream
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Return the file as a downloadable Excel file
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProcessedInvoice.xlsx");
            }
        }
    }
}
