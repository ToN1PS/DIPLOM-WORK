using DIPLOM.Model.PracticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPLOM.GetInfo
{
    internal class PracticeHelper
    {
        public static string GetInfoPractice(List<string> listInput, string myGroup, string filepath)
        {
            var dataPractice = RootObjectPractice.LoadJsonPDFfilesPractice(filepath);
            List<string> dataFiles = dataPractice.Files.Values
                .Where(file => file.Groups.Split(',').Contains(myGroup))
                .Select(file => file.Code)
                .ToList();

            int valueInput = 0;
            foreach (string input in listInput)
            {
                if (int.TryParse(input, out int value))
                {
                    valueInput = value;
                    break;
                }
            }

            string info = "";

            if (App.ContextChat == null && valueInput == 0)
            {
                info = "Чтобы выбрать нужный файл введите код практики\n\n";
                foreach (string code in dataFiles)
                {
                    var file = dataPractice.Files.Values.FirstOrDefault(f => f.Code == code);
                    if (file != null)
                    {
                        info += $"Код практики: {code}\n";
                        info += $"Название практики: {file.Name}\n";
                        info += $"Имя преподавателя: {file.Teacher}\n";
                        info += $"Курс: {file.Semestr}\n\n";

                        App.ContextChat = "практика";
                    }
                }
            }
            else if (App.ContextChat == "практика" || valueInput > 0)
            {
                App.ContextChat = null;

                foreach (var file in dataPractice.Files.Values)
                {
                    if (int.Parse(file.Code) == valueInput)
                    {

                        info = file.Pdfpath + "\n";
                        
                        break;
                        
                    }
                    else
                    {
                        info = $"Код практики не существует {valueInput}";
                    }
                }
            }

            return info;
        }
    }
}
