using ParkyWeb.Models;
using ParkyWeb.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repositories
{
    public class NationalParkRepository:GenericRepository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NationalParkRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
    }
}
