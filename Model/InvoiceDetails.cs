namespace ValueJetImport.Model
{
    public class InvoiceDetails
    {
        public int CNTBTCH { get; set; } = 5;
        public int CNTITEM { get; set; }
        public int CNTLINE { get; set; }
        public string IDITEM { get; set; }
        public string IDDIST { get; set; } = string.Empty;
        public string TEXTDESC { get; set; }
        public string UNITMEAS { get; set; } = "EACH";
        public int QTYINVC { get; set; } = 1;
        public decimal AMTCOST { get; set; } = 0;
        public decimal AMTPRIC { get; set; }
        public decimal AMTEXTN { get; set; }
        public decimal AMTCOGS { get; set; }
        public decimal AMTTXBL { get; set; }
        public decimal TOTTAX { get; set; }
        public decimal BASETAX1 { get; set; }
        public decimal BASETAX2 { get; set; }
        public decimal BASETAX3 { get; set; }
        public decimal BASETAX4 { get; set; }
        public decimal BASETAX5 { get; set; }
        public int TAXSTTS1 { get; set; }
        public int TAXSTTS2 { get; set; }
        public int TAXSTTS3 { get; set; }
        public int TAXSTTS4 { get; set; }
        public int TAXSTTS5 { get; set; }
        public int SWTAXINCL1 { get; set; }
        public int SWTAXINCL2 { get; set; }
        public int SWTAXINCL3 { get; set; }
        public int SWTAXINCL4 { get; set; }
        public int SWTAXINCL5 { get; set; }
        public decimal RATETAX1 { get; set; }
        public decimal RATETAX2 { get; set; }
        public decimal RATETAX3 { get; set; }
        public decimal RATETAX4 { get; set; }
        public decimal RATETAX5 { get; set; }
        public decimal AMTTAX1 { get; set; }
        public decimal AMTTAX2 { get; set; }
        public decimal AMTTAX3 { get; set; }
        public decimal AMTTAX4 { get; set; }
        public decimal AMTTAX5 { get; set; }
        public int IDACCTREV { get; set; } 
        public int IDACCTINV { get; set; } 
        public int IDACCTCOGS { get; set; } 
        public int COMMENT { get; set; } = 0;
        public int SWPRTSTMT { get; set; } = 0;
        public decimal ITEMCOST { get; set; } = 0;
        public int CONTRACT { get; set; } = 0;
        public int PROJECT { get; set; } = 0;
        public int CATEGORY { get; set; } = 0;
        public int RESOURCE { get; set; } = 0;
        public int TRANSNBR { get; set; } = 0;
        public int COSTCLASS { get; set; } = 0;
        public int BILLDATE { get; set; } = 0;
        public int SWIBT { get; set; } = 1;
        public int SWDISCABL { get; set; } = 0;
        public int OCNTLINE { get; set; } = 0;
        public decimal RTGAMT { get; set; } = 0;
        public decimal RTGPERCENT { get; set; } = 0;
        public int RTGDAYS { get; set; } = 0;
        public int RTGDATEDUE { get; set; } = 0;
        public int SWRTGDDTOV { get; set; } = 0;
        public int SWRTGAMTOV { get; set; } = 0;
        public int VALUES { get; set; } = 0;
        public int PROCESSCMD { get; set; } = 0;
        public decimal RTGDISTTC { get; set; } = 0;
        public decimal RTGCOGSTC { get; set; } = 0;
        public decimal RTGALTBTC { get; set; } = 0;
        public decimal RTGINVDIST { get; set; } = 0;
        public decimal RTGINVCOGS { get; set; } = 0;
        public decimal RTGINVALTB { get; set; } = 0;
        public decimal TXAMT1RC { get; set; } = 0;
        public decimal TXAMT2RC { get; set; } = 0;
        public decimal TXAMT3RC { get; set; } = 0;
        public decimal TXAMT4RC { get; set; } = 0;
        public decimal TXAMT5RC { get; set; } = 0;
        public decimal TXTOTRC { get; set; } = 0;
        public decimal TXBSERT1TC { get; set; } = 0;
        public decimal TXBSERT2TC { get; set; } = 0;
        public decimal TXBSERT3TC { get; set; } = 0;
        public decimal TXBSERT4TC { get; set; } = 0;
        public decimal TXBSERT5TC { get; set; } = 0;
        public decimal TXAMTRT1TC { get; set; } = 0;
        public decimal TXAMTRT2TC { get; set; } = 0;
        public decimal TXAMTRT3TC { get; set; } = 0;
        public decimal TXAMTRT4TC { get; set; } = 0;
        public decimal TXAMTRT5TC { get; set; } = 0;
        public decimal TXBSE1HC { get; set; } = 0;
        public decimal TXBSE2HC { get; set; } = 0;
        public decimal TXBSE3HC { get; set; } = 0;
        public decimal TXBSE4HC { get; set; } = 0;
        public decimal TXBSE5HC { get; set; } = 0;
        public decimal TXAMT1HC { get; set; } = 0;
        public decimal TXAMT2HC { get; set; } = 0;
        public decimal TXAMT3HC { get; set; } = 0;
        public decimal TXAMT4HC { get; set; } = 0;
        public decimal TXAMT5HC { get; set; } = 0;
        public decimal DISTNETHC { get; set; }
        public decimal RTGAMTHC { get; set; } = 0;
        public decimal AMTCOGSHC { get; set; } = 0;
        public decimal AMTCOSTHC { get; set; } = 0;
        public decimal AMTPRICHC { get; set; } 
        public decimal AMTEXTNHC { get; set; } 
        public decimal TXTOTRTTC { get; set; } = 0;
        public decimal TXTOTAMT1 { get; set; } = 0;
        public decimal TXTOTAMT2 { get; set; } = 0;
        public decimal TXTOTAMT3 { get; set; } = 0;
        public decimal TXTOTAMT4 { get; set; } = 0;
        public decimal TXTOTAMT5 { get; set; } = 0;
        public decimal RTGAMTOTC { get; set; } = 0;
        public decimal RTGDISTOTC { get; set; } = 0;
        public decimal RTGCOGSOTC { get; set; } = 0;
        public decimal RTGALTBOTC { get; set; } = 0;
        public decimal AMTWHT1TC { get; set; } = 0;
        public decimal AMTWHT2TC { get; set; } = 0;
        public decimal AMTWHT3TC { get; set; } = 0;
        public decimal AMTWHT4TC { get; set; } = 0;
        public decimal AMTWHT5TC { get; set; } = 0;
    }
}
