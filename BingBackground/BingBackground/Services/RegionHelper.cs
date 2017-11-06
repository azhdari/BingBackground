using BingBackground.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BingBackground.Services
{
    public class RegionHelper
    {
        #region Fields

        private readonly string filePath;

        #endregion

        #region Constructors

        public RegionHelper(string filePath)
        {
            this.filePath = filePath;
            Load();
        }

        #endregion

        #region Properties
        
        public List<Region> Regions { get; set; } = new List<Region>();

        public Region DefaultRegion { get; } = new Region
        {
            Id = "en-US",
            Name = "United States - English"
        };

        #endregion

        #region Methods

        public Region GetRandom()
        {
            if (!Regions.Any())
            {
                return DefaultRegion;
            }

            var enableds = Regions.Where(x => !x.Disabled).ToList();

            if (enableds.Count == 1)
            {
                return enableds.First();
            }
            
            var random = new Random();
            var next = random.Next(0, enableds.Count);
            return enableds.ElementAt(next);
        }

        #endregion

        #region Utilities

        private void Load()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("The filePath must be set.");
            }

            if (!File.Exists(filePath))
            {
                CreateEmptyRegionsFile();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                Regions = JsonConvert.DeserializeObject<List<Region>>(json);
            }
            catch (Exception)
            {
                Regions = new List<Region>();
            }
        }

        private void CreateEmptyRegionsFile()
        {
            var json = DefaultJson();
            var dir = Path.GetDirectoryName(filePath);
            
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(filePath, json);
        }

        private string DefaultJson()
        {
            return @"[
  {
    ""Id"": ""fa - IR"",
    ""Name"": ""Iran""
  },
  {
    ""Id"": ""en-IN"",
    ""Name"": ""India""
  },
  {
    ""Id"": ""es-AR"",
    ""Name"": ""Argentina""
  },
  {
    ""Id"": ""en-AU"",
    ""Name"": ""Australia""
  },
  {
    ""Id"": ""de-AT"",
    ""Name"": ""Austria""
  },
  {
    ""Id"": ""nl-BE"",
    ""Name"": ""Belgium - Dutch""
  },
  {
    ""Id"": ""fr-BE"",
    ""Name"": ""Belgium - French""
  },
  {
    ""Id"": ""pt-BR"",
    ""Name"": ""Brazil""
  },
  {
    ""Id"": ""en-CA"",
    ""Name"": ""Canada - English""
  },
  {
    ""Id"": ""fr-CA"",
    ""Name"": ""Canada - French""
  },
  {
    ""Id"": ""fr-FR"",
    ""Name"": ""France""
  },
  {
    ""Id"": ""de-DE"",
    ""Name"": ""Germany""
  },
  {
    ""Id"": ""zh-HK"",
    ""Name"": ""Hong Kong S.A.R.""
  },
  {
    ""Id"": ""en-ID"",
    ""Name"": ""Indonesia""
  },
  {
    ""Id"": ""it-IT"",
    ""Name"": ""Italy""
  },
  {
    ""Id"": ""ja-JP"",
    ""Name"": ""Japan""
  },
  {
    ""Id"": ""ko-KR"",
    ""Name"": ""Korea""
  },
  {
    ""Id"": ""en-MY"",
    ""Name"": ""Malaysia""
  },
  {
    ""Id"": ""es-MX"",
    ""Name"": ""Mexico""
  },
  {
    ""Id"": ""nl-NL"",
    ""Name"": ""Netherlands""
  },
  {
    ""Id"": ""nb-NO"",
    ""Name"": ""Norway""
  },
  {
    ""Id"": ""zh-CN"",
    ""Name"": ""People's Republic of China""
  },
  {
    ""Id"": ""pl-PL"",
    ""Name"": ""Poland""
  },
  {
    ""Id"": ""ru-RU"",
    ""Name"": ""Russia""
  },
  {
    ""Id"": ""ar-SA"",
    ""Name"": ""Saudi Arabia""
  },
  {
    ""Id"": ""en-ZA"",
    ""Name"": ""South Africa""
  },
  {
    ""Id"": ""es-ES"",
    ""Name"": ""Spain""
  },
  {
    ""Id"": ""sv-SE"",
    ""Name"": ""Sweden""
  },
  {
    ""Id"": ""fr-CH"",
    ""Name"": ""Switzerland - French""
  },
  {
    ""Id"": ""de-CH"",
    ""Name"": ""Switzerland - German""
  },
  {
    ""Id"": ""zh-TW"",
    ""Name"": ""Taiwan""
  },
  {
    ""Id"": ""tr-TR"",
    ""Name"": ""Turkey""
  },
  {
    ""Id"": ""en-GB"",
    ""Name"": ""United Kingdom""
  },
  {
    ""Id"": ""en-US"",
    ""Name"": ""United States - English""
  },
  {
    ""Id"": ""es-US"",
    ""Name"": ""United States - Spanish""
  }
]
";
        }

        #endregion
    }
}
