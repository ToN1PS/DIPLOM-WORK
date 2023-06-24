using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPLOM.Model.UserInfoModel
{
    public class User
    {
        public string FormStudies { get; set; }
        public string Universities { get; set; }
        public string Group { get; set; }
        public string FIO { get; set; }
    }


    public class RootObjectUserInfo
    {
        public User User { get; set; }

        public static RootObjectUserInfo LoadJsonUserInfo()
        {
            string fileName = "UserInfo.json";
            RootObjectUserInfo rootObject = null;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                rootObject = JsonConvert.DeserializeObject<RootObjectUserInfo>(json);
            }

            return rootObject;
        }

        public static void SaveJsonUserInfo(string SelectedFormStudy, string SelectedUniversity, string SelectedGroup, string FIO)
        {
            RootObjectUserInfo data = new RootObjectUserInfo()
            {
                User = new User()
                {
                    FormStudies = SelectedFormStudy,
                    Universities = SelectedUniversity,
                    Group = SelectedGroup,
                    FIO = FIO
                }
            };

            string fileName = "UserInfo.json";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
        }

    }
}
