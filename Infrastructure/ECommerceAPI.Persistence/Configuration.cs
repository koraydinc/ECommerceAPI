using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                IConfigurationManager configurationManager = new ConfigurationManager(); //Microsoft.Extensions.Configuration
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API")); //Microsoft.Extensions.Configuration.Json
                configurationManager.AddJsonFile("appsettings.json"); //Microsoft.Extensions.Configuration.Json

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
