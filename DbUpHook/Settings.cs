using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DbUpHook
{
    public static class Settings
    {
        public static IList<int> Ignore = 
            string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Ignore"])
                ? new List<int>()
                : ConfigurationManager.AppSettings["Ignore"].Split(',').Select(int.Parse).ToList();

        public static string Path = ConfigurationManager.AppSettings["Path"];
    }
}