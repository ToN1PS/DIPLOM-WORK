
using System.Collections.ObjectModel;
using System.Globalization;

namespace DIPLOM.Model
{
    internal class MainPage
    {

       
        public class DatePanelModel
        {
            public ObservableCollection<string> Dates { get; set; }
            public ObservableCollection<string> NameDays { get; set; }
            public ObservableCollection<Color> Colors { get; set; }

            

            public DatePanelModel()
            {
                Dates = GetDates();
                NameDays = GetNameOfDay();
                Colors = GetColors();
                

            }


            public ObservableCollection<Color> GetColors()
            {
                ObservableCollection<Color> colors = new ObservableCollection<Color>();

                DateTime today = DateTime.Today;

                for (int i = 0; i < 7; i++)
                {
                    Color myColor = Color.FromRgb(255, 0, 0);
                    colors.Add(myColor);
                }
                return colors;
            }

            public ObservableCollection<string> GetDates()
            {
                // Здесь происходит инициализация коллекции 
                ObservableCollection<string> dates = new ObservableCollection<string>();


                DateTime today = DateTime.Today;

                // Устанавливаем культуру форматирования в русскую культуру
                CultureInfo culture = new CultureInfo("ru-RU");

                // Получаем день недели с понедельника по воскресенье (1-7)
                int dayOfWeek = (int)today.DayOfWeek;

                // Получаем дату понедельника текущей недели
                DateTime mondayDate = today.AddDays(-dayOfWeek + 1);

                
                // Заполняем коллекцию датами текущей недели
                for (int i = 0; i < 7; i++)
                {
                    DateTime date = mondayDate.AddDays(i);
                    string dayOfWeekName = culture.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek);
                    dates.Add(dayOfWeekName);
                }
                return dates;


            }

            public ObservableCollection<string> GetNameOfDay()
            {
                ///////////////////
                // Здесь происходит инициализация коллекции 
                ObservableCollection<string> DatesWithDayOfWeek = new ObservableCollection<string>();

                DateTime today = DateTime.Today;

                // Устанавливаем культуру форматирования в русскую культуру
                CultureInfo culture = new CultureInfo("ru-RU");

                // Получаем день недели с понедельника по воскресенье (1-7)
                int dayOfWeek = (int)today.DayOfWeek;

                // Получаем дату понедельника текущей недели
                DateTime mondayDate = today.AddDays(-dayOfWeek + 1);


                // Заполняем коллекцию датами текущей недели
                for (int i = 0; i < 7; i++)
                {
                    DateTime date = mondayDate.AddDays(i);
                    string dayOfWeekName = date.ToString("dd", culture);
                    DatesWithDayOfWeek.Add(dayOfWeekName.ToString());
                    
                }

                return DatesWithDayOfWeek;
            }

            
        }

        

        

        
       

        
    }
}


