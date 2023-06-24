using Newtonsoft.Json;


namespace DIPLOM.Model.CoursPaperModel
{
    public class SubKeys
    {
        public string CodeKeys { get; set; }
        public string TeacherKeys { get; set; }
    }

    public class Keys
    {
        public string MainKeys { get; set; }
        public SubKeys SubKeys { get; set; }
    }

    public class FileData
    {
        public string Code { get; set; }
        public string Discipline { get; set; }
        public string Teacher { get; set; }
        public string Groups { get; set; }
        public string Semestr { get; set; }
        public string PdfpathMY { get; set; }
        public string PdfpathRP { get; set; }
    }


    public class RootObjectCoursePaper
    {
        public Keys Keys { get; set; }
        public string MoreInfo { get; set; }
        public Dictionary<string, FileData> Files { get; set; }


        public static RootObjectCoursePaper LoadJsonPDFfilesCoursePaper(string fileName)
        {
            RootObjectCoursePaper rootObject = null;

            var assembly = typeof(App).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Data.{fileName}");
            using (StreamReader sr = new StreamReader(stream))
            {
                string content =  sr.ReadToEnd();
                rootObject = JsonConvert.DeserializeObject<RootObjectCoursePaper>(content);
            }

            return rootObject;
        }

    }
}
