using DIPLOM.Model.MethodistModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DIPLOM.GetInfo
{
    public static class MethodistHelper
    {
        public static string GetInfoMethodist(List<string> listInput, string myGroup, string filepath)
        {

            string info = "";

            var data = RootObjectMethodist.LoadJsonMethodists(filepath);

            Methodist methodistData = null;
            List<string> groups;

            // Ищем подходящего методиста
            foreach (var methodist in data.Methodists.Values)
            {
                groups = null;
                groups = methodist.Groups.Split(',').ToList();

                if (groups.Contains(myGroup))
                {
                    methodistData = methodist;
                    break; // Добавлено прерывание цикла, если методист найден
                }
            }



            // Ищем совпадения по тегам
            foreach (string inputElement in listInput)
            {
                if (data.Keys.SubKeys.NumberKeys.Contains(inputElement) == true)
                {
                    info += methodistData.Number + "\n";
                    // Дополнительные действия с информацией, если нужно
                }
                else if (data.Keys.SubKeys.OfficeKeys.Contains(inputElement) == true)
                {
                    info += methodistData.Office + "\n";
                    // Дополнительные действия с информацией, если нужно
                }
                else if (data.Keys.SubKeys.NameKeys.Contains(inputElement) == true)
                {
                    info += methodistData.Name+ "\n";
                    // Дополнительные действия с информацией, если нужно
                }
                else if (data.Keys.SubKeys.EmailKeys.Contains(inputElement) == true)
                {
                    info += methodistData.Email + "\n";
                    // Дополнительные действия с информацией, если нужно
                }
            }

            
            if (info == "")
            {
                info += methodistData.Name + "\n";
                info += methodistData.Number + "\n";
                info += methodistData.Office + "\n";
                info += methodistData.Email + "\n";
            }

            return info;
        }
    }
}
