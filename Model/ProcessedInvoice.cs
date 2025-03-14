namespace ValueJetImport.Model
{
    public class ProcessedInvoice
    {
        public int CNTBTCH { get; set; } = 5;
        public int CNTITEM { get; set; }
        public string IDCUST { get; set; } = "PAS2023";
        public string IDINVC { get; set; }
        public string IDSHPT { get; set; }
        public string SPECINST { get; set; }
        public int TEXTTRX { get; set; } = 1;
        public int IDTRX { get; set; } = 15;
        public string ORDRNBR { get; set; }
        public string CUSTPO { get; set; }
        public string INVCDESC { get; set; }
        public int SWPRTINVC { get; set; }
        public string INVCAPPLTO { get; set; }
        public string IDACCTSET { get; set; } = "CUS03";
        public string DATEINVC { get; set; }
        public string DATEASOF { get; set; }
        public int FISCYR { get; set; } = 2023;
        public int FISCPER { get; set; } = 01;
        public string CODECURN { get; set; } = "NGN";
        public string RATETYPE { get; set; } = "SP";
        public int SWMANRTE { get; set; }
        public decimal EXCHRATEHC { get; set; } = 1;
        public decimal ORIGRATEHC { get; set; }
        public string TERMCODE { get; set; } = "ADP";
        public int SWTERMOVRD { get; set; }
        public string DATEDUE { get; set; }
        public string DATEDISC { get; set; }
        public decimal PCTDISC { get; set; }
        public decimal AMTDISCAVL { get; set; }
        public int LASTLINE { get; set; } = 4;
        public string CODESLSP1 { get; set; }
        public string CODESLSP2 { get; set; }
        public string CODESLSP3 { get; set; }
        public string CODESLSP4 { get; set; }
        public string CODESLSP5 { get; set; }
        public decimal PCTSASPLT1 { get; set; }
        public decimal PCTSASPLT2 { get; set; }
        public decimal PCTSASPLT3 { get; set; }
        public decimal PCTSASPLT4 { get; set; }
        public decimal PCTSASPLT5 { get; set; }
        public int SWTAXBL { get; set; }
        public int SWMANTX { get; set; }
        public string CODETAXGRP { get; set; } = "'FIRS";
        public string CODETAX1 { get; set; } = "VAT";
        public string CODETAX2 { get; set; } = "WHT10";
        public string CODETAX3 { get; set; } = "WHT5";
        public string CODETAX4 { get; set; }
        public string CODETAX5 { get; set; }
        public int TAXSTTS1 { get; set; } = 2;
        public int TAXSTTS2 { get; set; } = 2;
        public int TAXSTTS3 { get; set; } = 2;
        public int TAXSTTS4 { get; set; }
        public int TAXSTTS5 { get; set; }
        public decimal BASETAX1 { get; set; }
        public decimal BASETAX2 { get; set; }
        public decimal BASETAX3 { get; set; }
        public decimal BASETAX4 { get; set; }
        public decimal BASETAX5 { get; set; }
        public decimal AMTTAX1 { get; set; }
        public decimal AMTTAX2 { get; set; }
        public decimal AMTTAX3 { get; set; }
        public decimal AMTTAX4 { get; set; }
        public decimal AMTTAX5 { get; set; }
        public decimal AMTTXBL { get; set; }
        public decimal AMTNOTTXBL { get; set; }
        public decimal AMTTAXTOT { get; set; }
        public decimal AMTINVCTOT { get; set; }
        public decimal AMTPPD { get; set; }
        public decimal AMTPAYMTOT { get; set; } = 1;
        public decimal AMTPYMSCHD { get; set; }
        public decimal AMTNETTOT { get; set; }
        public string IDSTDINVC { get; set; }
        public string DATEPRCS { get; set; }
        public string IDPPD { get; set; }
        public string IDBILL { get; set; }
        public string SHPTOLOC { get; set; }
        public string SHPTOSTE1 { get; set; }
        public string SHPTOSTE2 { get; set; }
        public string SHPTOSTE3 { get; set; }
        public string SHPTOSTE4 { get; set; }
        public string SHPTOCITY { get; set; }
        public string SHPTOSTTE { get; set; }
        public string SHPTOPOST { get; set; }
        public string SHPTOCTRY { get; set; }
        public string SHPTOCTAC { get; set; }
        public string SHPTOPHON { get; set; }
        public string SHPTOFAX { get; set; }
        public string DATERATE { get; set; }
        public int SWPROCPPD { get; set; }
        public decimal AMTDUE { get; set; }
        public int CUROPER { get; set; } = 1;
        public string DRILLAPP { get; set; }
        public string DRILLTYPE { get; set; }
        public string DRILLDWNLK { get; set; }
        public string GETIBSINFO { get; set; }
        public int PROCESSCMD { get; set; }
        public string SHPVIACODE { get; set; }
        public string SHPVIADESC { get; set; }
        public int PRPTYCODE { get; set; } = 1;
        public int PRPTYVALUE { get; set; } = 1;
        public int SWJOB { get; set; }
        public string ERRBATCH { get; set; }
        public string ERRENTRY { get; set; }
        public string EMAIL { get; set; }
        public string CTACPHONE { get; set; }
        public string CTACFAX { get; set; }
        public string CTACEMAIL { get; set; }
        public decimal AMTDSBWTAX { get; set; }
        public decimal AMTDSBNTAX { get; set; }
        public decimal AMTDSCBASE { get; set; }
        public int INVCTYPE { get; set; } = 1;
        public int SWRTGINVC { get; set; }
        public string RTGAPPLYTO { get; set; }
        public int SWRTG { get; set; }
        public decimal RTGAMT { get; set; }
        public decimal RTGPERCENT { get; set; }
        public int RTGDAYS { get; set; }
        public string RTGDATEDUE { get; set; }
        public string RTGTERMS { get; set; }
        public int SWRTGDDTOV { get; set; }
        public int SWRTGAMTOV { get; set; }
        public int SWRTGRATE { get; set; }
        public string VALUES { get; set; }
        public string SRCEAPPL { get; set; } = "AR";
        public string ARVERSION { get; set; } = "71A";
        public int TAXVERSION { get; set; } = 1;
        public int SWTXRTGRPT { get; set; }
        public string CODECURNRC { get; set; } = "NGN";
        public int SWTXCTLRC { get; set; } = 1;
        public decimal RATERC { get; set; } = 1;
        public string RATETYPERC { get; set; }
        public string RATEDATERC { get; set; }
        public string RATEOPRC { get; set; } = "1";
        public int SWRATERC { get; set; }
        public decimal TXAMT1RC { get; set; }
        public decimal TXAMT2RC { get; set; }
        public decimal TXAMT3RC { get; set; }
        public decimal TXAMT4RC { get; set; }
        public decimal TXAMT5RC { get; set; }
        public decimal TXTOTRC { get; set; }
        public decimal TXBSERT1TC { get; set; }
        public decimal TXBSERT2TC { get; set; }
        public decimal TXBSERT3TC { get; set; }
        public decimal TXBSERT4TC { get; set; }
        public decimal TXBSERT5TC { get; set; }
        public decimal TXAMTRT1TC { get; set; }
        public decimal TXAMTRT2TC { get; set; }
        public decimal TXAMTRT3TC { get; set; }
        public decimal TXAMTRT4TC { get; set; }
        public decimal TXAMTRT5TC { get; set; }
        public decimal TXBSE1HC { get; set; }
        public decimal TXBSE2HC { get; set; }
        public decimal TXBSE3HC { get; set; }
        public decimal TXBSE4HC { get; set; }
        public decimal TXBSE5HC { get; set; }
        public decimal TXAMT1HC { get; set; }
        public decimal TXAMT2HC { get; set; }
        public decimal TXAMT3HC { get; set; }
        public decimal TXAMT4HC { get; set; }
        public decimal TXAMT5HC { get; set; }
        public decimal AMTGROSHC { get; set; }
        public decimal RTGAMTHC { get; set; }
        public decimal AMTDISCHC { get; set; }
        public decimal DISTNETHC { get; set; }
        public decimal AMTPPDHC { get; set; }
        public decimal AMTDUEHC { get; set; }
        public int SWPRTLBL { get; set; }
        public decimal TXTOTRTTC { get; set; }
        public decimal TXTOTAMT1 { get; set; }
        public decimal TXTOTAMT2 { get; set; }
        public decimal TXTOTAMT3 { get; set; }
        public decimal TXTOTAMT4 { get; set; }
        public decimal TXTOTAMT5 { get; set; }
        public decimal RTGAMTDTL { get; set; }
        public string IDSHIPNBR { get; set; }
        public int SWOECOST { get; set; }
        public string ENTEREDBY { get; set; } = "ADMIN";
        public string DATEBUS { get; set; }
        public string EDN { get; set; }
        public decimal AMTWHT1TC { get; set; }
        public decimal AMTWHT2TC { get; set; }
        public decimal AMTWHT3TC { get; set; }
        public decimal AMTWHT4TC { get; set; }
        public decimal AMTWHT5TC { get; set; }
        public string SFPAURL { get; set; }
        public string SFPAID { get; set; }
        public string TXTOTWHT { get; set; }
        public decimal AMTDUEWHT { get; set; }
        public decimal AMTDUEWHDS { get; set; }
    }

}

