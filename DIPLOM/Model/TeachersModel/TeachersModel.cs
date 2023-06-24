using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DIPLOM.Model.TeachersModel
{
    public class SubKeys
    {
        public string DepartamentNamekeys { get; set; }
        public string AddressKeys { get; set; }
        public string OfficeKeys { get; set; }
        public string EmailKeys { get; set; }
        public string NameKeys { get; set; }
        public string Schedule { get; set; }
    }

    public class Keys
    {
        public string MainKeys { get; set; }
        public SubKeys SubKeys { get; set; }
    }

    public class Teacher
    {
        public string Address { get; set; }
        public string CabinetNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Department
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Groups { get; set; }
        public Dictionary<string, Teacher> Teachers { get; set; }
    }

    public class RootObjectTeachers
    {
        public Keys Keys { get; set; }
        public Dictionary<string, Department> Departaments { get; set; }

        public static RootObjectTeachers LoadJsonTeachers(string fileName)
        {
            RootObjectTeachers rootObject = null;

            var assembly = typeof(App).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Data.{fileName}");
            using (StreamReader sr = new StreamReader(stream))
            {
                string content = sr.ReadToEnd();
                rootObject = JsonConvert.DeserializeObject<RootObjectTeachers>(content);
            }

            return rootObject;
        }
    }
}
