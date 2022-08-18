using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopCovidCases.Models
{
    public class RegionsModel
    {
        public string Iso { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Lat { get; set; }

        [JsonProperty("long")]
        //public string @long { get; set; }
        public string Long { get; set; }
        public int? TotalConfirmed { get; set; }
        public CitiesModel[] Cities { get; set; }
        //public object[] cities { get; set; }
    }
}