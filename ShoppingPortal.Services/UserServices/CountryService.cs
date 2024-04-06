using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
namespace ShoppingPortal.Services.UserServices
{
    

    public class CountryService
    {
        private readonly string _countryJsonPath;

        public CountryService(IConfiguration config, IHostingEnvironment env)
        {
            var relativePath = config["Paths:CountryJson"];
            _countryJsonPath = Path.Combine(env.ContentRootPath, relativePath);
        }

        public Dictionary<string, List<string>> GetCountryStates()
        {
            if (!File.Exists(_countryJsonPath))
                return new Dictionary<string, List<string>>();

            var jsonData = File.ReadAllText(_countryJsonPath);
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData);
        }
    }


}
