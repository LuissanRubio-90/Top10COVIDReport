using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopCovidCases.Models
{
    public class TotalReportsViewModel
    {
        public DateTime Date { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Confirmed { get; set; }
        public int Confirmed_Diff { get; set; }
        public int Deaths { get; set; }
        public int Deaths_Diff { get; set; }
        public int Recovered { get; set; }
        public int Recovered_Diff { get; set; }
        public int Active { get; set; }
        public int Active_Diff { get; set; }
        public decimal Fatality_Rate { get; set; }
    }
}