using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopCovidCases.Models
{
    public class CitiesModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int? Fips { get; set; }
        public string Lat { get; set; }
        [JsonProperty("long")]
        //public string @long { get; set; }
        public string Long { get; set; }
        public int? Confirmed { get; set; }
        public int? Deaths { get; set; }
        public int? Confirmed_diff { get; set; }
        public int? Deaths_diff { get; set; }
        public string Last_update { get; set; }
    }
}