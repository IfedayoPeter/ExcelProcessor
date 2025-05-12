using Microsoft.AspNetCore.Mvc;
using ValueJetImport.Helpers;
using ValueJetImport.Model;

namespace Processor.Controllers
{
    [Route("api/excel")]
    [ApiController]
    public class ValueJetDataProcessingController : ControllerBase
    {
        // POST api/excelprocessing/process
        [HttpPost]
        public IActionResult ProcessTickets([FromBody] List<TicketDetail> ticketDetails)
        {
            if (ticketDetails == null || !ticketDetails.Any())
            {
                return BadRequest("No data provided");
            }


            // Group by Origin and Operated Flight Date
            var groupedTickets = ticketDetails
                .GroupBy(t => new { t.Origin, t.OperatedFlightDate })
                .Select(g => new ProcessedResult
                {
                    Origin = g.Key.Origin,
                    FlightDate = $"{g.Key.OperatedFlightDate}",
                    SumOfAmount = g.Sum(t => t.Amount),
                    SumOfYQ = g.Sum(t => t.YQ),
                    DistinctCountOfTktnbr = g.Select(t => t.Tktnbr).Distinct().Count(),
                    NG = (g.Sum(t => t.Amount) + g.Sum(t => t.YQ)) * 0.05M,
                    QTRate = GetQTRate(g.Key.Origin),  // Calculate QT RATE based on origin
                    QT = g.Select(t => t.Tktnbr).Distinct().Count() * GetQTRate(g.Key.Origin)  // QT Calculation
                })
                .OrderBy(g => g.Origin)
                .ToList();

            return Ok(groupedTickets);
        }

        [HttpPost("invoice_details")]
        public IActionResult ProcessInvoiceDetails([FromBody] List<KiuTransaction> inputData)
        {
            var processedDataList = new List<InvoiceDetails>();
            int cntItem = 1; // Initialize CNTITEM

            foreach (var data in inputData)
            {
                // Create 4 rows based on the incoming data
                processedDataList.Add(new InvoiceDetails
                {
                    CNTITEM = cntItem,
                    CNTLINE = 20,
                    IDITEM = "F_FA",
                    TEXTDESC = "PAX FARE",
                    AMTPRIC = data.SumOfAmount,
                    AMTEXTN = data.SumOfAmount,
                    AMTTXBL = data.SumOfAmount,
                    DISTNETHC = data.SumOfAmount,
                    AMTPRICHC = data.SumOfAmount,
                    AMTEXTNHC = data.SumOfAmount,
                    IDACCTREV = 70000,
                    IDACCTINV = 70000,
                    IDACCTCOGS = 70000,

                });

                processedDataList.Add(new InvoiceDetails
                {
                    CNTITEM = cntItem,
                    CNTLINE = 40,
                    IDITEM = "T_NG",
                    TEXTDESC = "NG TAX",
                    AMTPRIC = data.Ng,
                    AMTEXTN = data.Ng,
                    AMTTXBL = data.Ng,
                    DISTNETHC = data.Ng,
                    AMTPRICHC = data.Ng,
                    AMTEXTNHC = data.Ng,
                    IDACCTREV = 70002,
                    IDACCTINV = 70002,
                    IDACCTCOGS = 70002,

                });

                processedDataList.Add(new InvoiceDetails
                {
                    CNTITEM = cntItem,
                    CNTLINE = 60,
                    IDITEM = "T_QT",
                    TEXTDESC = "QT PASSENGER SERVICE CHARGE",
                    AMTPRIC = data.Qt,
                    AMTEXTN = data.Qt,
                    AMTTXBL = data.Qt,
                    DISTNETHC = data.Qt,
                    AMTPRICHC = data.Qt,
                    AMTEXTNHC = data.Qt,
                    IDACCTREV = 70104,
                    IDACCTINV = 70104,
                    IDACCTCOGS = 70104,

                });

                processedDataList.Add(new InvoiceDetails
                {
                    CNTITEM = cntItem,
                    CNTLINE = 80,
                    IDITEM = "T_YQ",
                    TEXTDESC = "YQ FUEL SURCHARGE",
                    AMTPRIC = data.SumOfYQ,
                    AMTEXTN = data.SumOfYQ,
                    AMTTXBL = data.SumOfYQ,
                    DISTNETHC = data.SumOfYQ,
                    AMTPRICHC = data.SumOfYQ,
                    AMTEXTNHC = data.SumOfYQ,
                    IDACCTREV = 70001,
                    IDACCTINV = 70001,
                    IDACCTCOGS = 70001,

                });

                cntItem++; // Increment CNTITEM after every 4 rows
            }

            return Ok(processedDataList);
        }
        [HttpPost("invoice")]
        public IActionResult ProcessInvoice([FromBody] List<KiuTransaction> inputData)
        {
            var processedDataList = new List<ProcessedInvoice>();

            foreach (var transaction in inputData)
            {
                var processedInvoice = new ProcessedInvoice
                {
                    IDINVC = $"{transaction.Origin}-{transaction.FlightDate:MM/dd/yyyy}", // Format: origin-flightDate
                    INVCDESC = $"{transaction.Origin}-{transaction.FlightDate:MM/dd/yyyy}", // Same as IDINVC
                    DATEINVC = $"{transaction.FlightDate:MM / dd / yyyy}",
                    DATEASOF = $"{transaction.FlightDate:MM / dd / yyyy}",
                    DATEDUE = $"{transaction.FlightDate:MM / dd / yyyy}",
                    DATERATE = $"{transaction.FlightDate:MM / dd / yyyy}",
                    DATEBUS = $"{transaction.FlightDate:MM / dd / yyyy}",
                };

                // Add processed invoice to list
                processedDataList.Add(processedInvoice);
            }

            // Return processed data as JSON
            return Ok(processedDataList);
        }





        private decimal GetQTRate(string origin)
        {
            // Define QT Rate based on origin
            switch (origin)
            {
                case "ABB":
                    return 4000M;
                case "ABV":
                case "PHC":
                case "BNI":
                    return 2000M;
                case "LOS":
                    return 4000M;
                default:
                    return 0M;
            }
        }
    }
}
