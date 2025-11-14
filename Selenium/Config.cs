using Microsoft.Extensions.Configuration;
using System;

namespace Core
{
    public static class Config
    {
        public static IConfigurationRoot Configuration { get; }

        static Config()
        {
             Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
