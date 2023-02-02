

using Microsoft.Extensions.Configuration.Json;
using System.Net;
using System.Web;

namespace practice.Utils
{
    public class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigHelper()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

    }
}
