using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Configurations
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44399/";
        public static string NationalParkAPI = APIBaseUrl + "api/v1/nationalparks/";
        public static string TrailAPI = APIBaseUrl + "api/v1/trails/";
    }
}
