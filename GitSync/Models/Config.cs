using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GitSync.Models
{
    public class Config
    {
        public List<string> Paths = new List<string>();
        public List<string> Remotes = new List<string>();
        public int SyncEveryHours;
        public string AppInsights;
        public string ConfigPath;

        public static Config Load(string configFile)
        {
            if (!File.Exists(configFile))
                Save(new Config(), configFile);

            var text = File.ReadAllText(configFile);
            var config = JsonConvert.DeserializeObject<Config>(text);
            config.ConfigPath = configFile;
            return config;
        }

        public static void Save(Config config, string configFile)
        {
            File.WriteAllText(configFile,
                Newtonsoft.Json.JsonConvert.SerializeObject(config, Formatting.Indented));
            config.ConfigPath = configFile;
        }

        public string AddPathToConfig()
        {
            Console.WriteLine("No paths specified in config.");
            Console.WriteLine("Please enter the new path:");
            var path = Console.ReadLine();
            Paths.Add(path);
            Save(this, this.ConfigPath);
            Console.WriteLine("Config saved.");
            Console.WriteLine();
            return path;
        }

    }
}
