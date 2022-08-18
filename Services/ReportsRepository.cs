using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace TopCovidCases.Services
{
    public interface IReportsRepository
    {
        Task<HttpRequestMessage> GetConn();
    }
    public class ReportsRepository 
    {

    }
}