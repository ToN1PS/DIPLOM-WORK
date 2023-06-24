using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DIPLOM.Model.ScheduleModel
{
    public class ClassInfo
    {
        public string time { get; set; }
        public string subject { get; set; }
        public string teacher { get; set; }
        public string room { get; set; }
        public string type { get; set; }
        public string number { get; set; } 
        public string depCode { get; set; }
    }

    public class DayInfo
    {
        public string day { get; set; }
        public List<ClassInfo> classes { get; set; }
    }

    public class WeekInfo
    {
        public string week { get; set; }
        public string group { get; set; }
        public List<DayInfo> days { get; set; }
    }

    public class RootObjectSchedule
    {
        public List<WeekInfo> schedule { get; set; }

        public static RootObjectSchedule LoadJsonSchedule(string fileName)
        {
            
            RootObjectSchedule rootObject = null;

            var assembly = typeof(App).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Data.{fileName}");
            using (StreamReader sr = new StreamReader(stream))
            {
                string content = sr.ReadToEnd();
                rootObject = JsonConvert.DeserializeObject<RootObjectSchedule>(content);
            }

            return rootObject;
        }
    }
}
