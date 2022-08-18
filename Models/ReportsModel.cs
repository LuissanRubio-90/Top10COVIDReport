using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopCovidCases.Models
{

    public class RpData
    {
        public ReportsModel[] data { get; set; }
    }
    public class ReportsModel
    {
        public string Date { get; set; }
        public int? Confirmed { get; set; }
        public int? Deaths { get; set; }
        public int? Recovered { get; set; }
        public int? Confirmed_diff { get; set; }
        public int? Deaths_diff { get; set; }
        public int? Recovered_diff { get; set; }
        public string Last_update { get; set; }
        public int? Active { get; set; }
        public int? Active_diff { get; set; }
        public float? Fatality_rate { get; set; }
        
        public RegionsModel Region { get; set; }

    }
}