namespace ValueJetImport.Model
{
    public class TicketDetail
    {
        public string Tktnbr { get; set; }
        public string Pax { get; set; }
        public string Pnr { get; set; }
        public string Issuer { get; set; }
        public string Agent { get; set; }
        //public DateTime IssueDate { get; set; }
        //public DateTime ScheduledFlightDate { get; set; }
        public string OperatedFlightDate { get; set; }
        public string Origin { get; set; }
        public decimal Amount { get; set; }
        public decimal YQ { get; set; }
    }
}
