using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace DIPLOM.Model.PracticeModel
{
    public class KeysData
    {
        public string MainKeys { get; set; }
        public SubKeysData SubKeys { get; set; }
    }

    public class SubKeysData
    {
        public string CodeKeys { get; set; }
        public string TeacherKeys { get; set; }
    }

    public class FileData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Keys { get; set; }
        public string Groups { get; set; }
        public string Semestr { get; set; }
        public string Pdfpath { get; set; }
    }
    public class RootObjectPractice
    {
        public KeysData Keys { get; set; }
        public string MoreInfo { get; set; }
        public Dictionary<string, FileData> Files { get; set; }


        public static RootObjectPractice LoadJsonPDFfilesPractice(string fileName)
        {
            RootObjectPractice rootObject = null;

            var assembly = typeof(App).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Data.{fileName}");
            using (StreamReader sr = new StreamReader(stream))
            {
                string content = sr.ReadToEnd();
                rootObject = JsonConvert.DeserializeObject<RootObjectPractice>(content);
            }

            return rootObject;
        }

    }
}
