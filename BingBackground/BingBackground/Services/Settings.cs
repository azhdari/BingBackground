using Newtonsoft.Json;
using System;
using System.IO;

namespace BingBackground.Services
{
    public class Settings
    {
        #region Fields

        private readonly string filePath;

        #endregion

        #region Constructors

        public Settings(string filePath)
        {
            this.filePath = filePath;
            Load();
        }

        #endregion

        #region Properties

        public SettingsValues Values { get; set; }

        #endregion

        #region Methods



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
                CreateEmptySettingsFile();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                Values = JsonConvert.DeserializeObject<SettingsValues>(json);
            }
            catch (Exception)
            {
                Values = new SettingsValues();
            }
        }

        private void CreateEmptySettingsFile()
        {
            var obj = new SettingsValues();
            var json = JsonConvert.SerializeObject(obj);
            var dir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(filePath, json);
        }

        #endregion
    }
}
