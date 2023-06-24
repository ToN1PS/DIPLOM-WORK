using DIPLOM.Model.CoursPaperModel;
using System.Collections.Generic;
using System.Linq;

namespace DIPLOM.GetInfo
{
    internal class CoursePaperHelper
    {
        public static string GetInfoCoursePaper(List<string> listInput, string myGroup, string filepath)
        {
            var dataCoursPaper = RootObjectCoursePaper.LoadJsonPDFfilesCoursePaper(filepath);
            List<string> dataFiles = dataCoursPaper.Files.Values
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
                info = "Чтобы выбрать нужную методчку введите код предмета\n\n";
                foreach (string code in dataFiles)
                {
                    var file = dataCoursPaper.Files.Values.FirstOrDefault(f => f.Code == code);
                    if (file != null)
                    {
                        info += $"Код предмета: {code}\n";
                        info += $"Название предмета: {file.Discipline}\n";
                        info += $"Имя преподавателя: {file.Teacher}\n";
                        info += $"Курс: {file.Semestr}\n\n";

                        App.ContextChat = "методичка";
                    }
                }
            }
            else if (App.ContextChat == "методичка" || valueInput > 0)
            {
                App.ContextChat = null;

                foreach (var file in dataCoursPaper.Files.Values)
                {
                    if (int.Parse(file.Code) == valueInput)
                    {
                        
                        info = file.PdfpathMY + "\n";
                        info += file.PdfpathRP + "\n";
                        break;
                    }
                    else
                    {
                        info = $"Код предмета не существует {valueInput}";
                    }
                }
            }

            return info;
        }
    }
}
