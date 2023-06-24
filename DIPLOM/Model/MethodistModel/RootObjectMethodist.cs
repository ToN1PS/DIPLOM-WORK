using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DIPLOM.Model.MethodistModel
{
    public class RootObjectMethodist
    {
        public Dictionary<string, Methodist> Methodists { get; set; }
        public Keys Keys { get; set; }


        public static RootObjectMethodist LoadJsonMethodists(string fileName)
        {
            RootObjectMethodist rootObject = null;

            var assembly = typeof(App).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Data.{fileName}");
            using (StreamReader sr = new StreamReader(stream))
            {
                string content = sr.ReadToEnd();
                rootObject = JsonConvert.DeserializeObject<RootObjectMethodist>(content);
            }

            return rootObject;
        }
    }
}
