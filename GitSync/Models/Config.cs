using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GitSync.Models
{
    public class Config
    {
        public List<string> Paths;
        public List<string> Remotes;
        public int SyncEveryHours;
        public string AppInsights;

        public static Config LoadConfig(string configFile)
        {
            if (!File.Exists(configFile))
                SaveConfig(new Config(), configFile);

            var text = File.ReadAllText(configFile);
            return JsonConvert.DeserializeObject<Config>(text);
        }

        public static void SaveConfig(Config config, string configFile)
        {
            File.WriteAllText(configFile,
                Newtonsoft.Json.JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}
