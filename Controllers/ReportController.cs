using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TopCovidCases.Models;
using TopCovidCases.Services;

namespace TopCovidCases.Controllers
{
    public class ReportController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string provincia_iso = "";
            if (Request["ddl_provincia"] != null)
            {
                provincia_iso = Request["ddl_provincia"].ToString();
            }
            ViewData["cambiar"] = provincia_iso == "" || provincia_iso == "[all]" ? false : true;
            var report = await GetReport(provincia_iso);
            var region = await GetRegion(provincia_iso);
            var tuple = new Tuple<List<RegionsSelectModel>, List<ReportsModel>>(region, report);
            return View(tuple);
        }

        //Metodo para obtener el reporte de la Web API
        public async Task<List<ReportsModel>> GetReport(string region_name)
        {
            //Creando una lista de tipo reportsmodel
            List<ReportsModel> reports = new List<ReportsModel>();

            //Iniciando la conexion a la WEB API (URL + KEY)
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports"),
                Headers =
                {
                    { "X-RapidAPI-Key", "4dc22a5880mshabdda25cbad99abp1a0850jsn084744c95df0"},
                    { "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
                },

            };
            //Obteniendo la data de la WEB API al Modelo
            using (var response = await client.SendAsync(request))
            {

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var a = JsonConvert.DeserializeObject<RpData>(body);
                    if (region_name == "" || region_name == "[all]") //Condicionando si el nombre de la region es vacia o es [all] se ejecute la siguiente consulta
                    {
                        reports = a.data.OrderByDescending(x => x.Confirmed)
                                  .Distinct()
                                  .Take(10)
                                  .ToList();
                    } else //Sino, que se ejecute la consulta tomando de referencia el nombre de la region
                    {
                        reports = a.data.OrderByDescending(x => x.Confirmed).Where(w => w.Region.Name == region_name)
                                  .Distinct()
                                  .Take(10)
                                  .ToList();

                    }
                }
                return reports;
            }
        }

        //Metodo para obtener las regiones de la web api para el select
        public async Task<List<RegionsSelectModel>> GetRegion(string region_name)
        {
            //Creando una lista de tipo RegionsSelectModel
            List<RegionsSelectModel> regions = new List<RegionsSelectModel>();

            //Obteniendo la data de la WEB API
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
                Headers =
                {
                    { "X-RapidAPI-Key", "4dc22a5880mshabdda25cbad99abp1a0850jsn084744c95df0"},
                    { "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
                },

            };

            //Obteniendo la data de la WEB API al Modelo
            using (var response = await client.SendAsync(request))
            {

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var a = JsonConvert.DeserializeObject<RegionData>(body);

                    regions = a.data.OrderBy(x => x.name).Distinct().ToList();
                    regions.Insert(0, new RegionsSelectModel { iso = "0", name = "[all]", selected = true }); //Construyendo la consulta insert para mostrar el primer valor como all
                }
               
            }
            regions = regions.Select(x =>
            {
                x.selected = x.name == region_name ? true : false; //Consulta en la que determina si el campo de la clase y el parametro coinciden, arroje verdadero, caso contrario, arroje falso
                return x;
            }).ToList();
            return regions;
        }
    }
}