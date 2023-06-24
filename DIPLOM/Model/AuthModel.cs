
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DIPLOM.Model
{
    internal class Auth
    {
        public class UniversitiesModel
        {
            public ObservableCollection<string> GetUniversities()
            {
                // Здесь происходит инициализация коллекции университетов
                ObservableCollection<string> universities = new ObservableCollection<string>()
            {
                "ИСОУ",
                "ИГИН",
                "И тд",
                // Добавьте сюда другие университеты, которые должны быть доступны в списке
            };
                return universities;
            }
        }

        public class GroupsModel
        {
            public ObservableCollection<string> GetGroups()
            {
                // Здесь происходит инициализация коллекции университетов
                ObservableCollection<string> groups = new ObservableCollection<string>()
            {
                "Мкнб 19 1",
                "Пктб 19 1",
                "И тд",
                // Добавьте сюда другие университеты, которые должны быть доступны в списке
            };
                return groups;
            }
        }

        public class FormStudiesModel
        {
            public ObservableCollection<string> GetFormStudies()
            {
                // Здесь происходит инициализация коллекции университетов
                ObservableCollection<string> FormStudies = new ObservableCollection<string>()
            {
                "Очная форма",
                "Заочная форма",

            };
                return FormStudies;
            }
        }

        


    //    public string Filename { get; set; }
    //    public string SaveData;

    //    public void Save()
    //    {
    //        //ObservableCollection<string> SaveData = new ObservableCollection<string>()
    //        //{
    //        //    "Очная форма",
    //        //    "Заочная форма",
    //        //};

    //        SaveData = ("da");

    //        Filename = $"{Path.GetRandomFileName()}.authData.txt";
    //        File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), SaveData);
    //    }
               
    }
}


