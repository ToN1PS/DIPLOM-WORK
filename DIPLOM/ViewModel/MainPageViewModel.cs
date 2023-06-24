using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPLOM.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Platform;
using static DIPLOM.Model.MainPage;
using DIPLOM.Model.ScheduleModel;
using System.Globalization;
using DIPLOM.Model.UserInfoModel;

namespace DIPLOM.ViewModel
{
    public class MyViewModel : ObservableObject
    {
        private string _content;
        private string _typeOfWeek;
        
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public string TypeOfWeek
        {
            get { return _typeOfWeek; }
            set { SetProperty(ref _typeOfWeek, value); }
        }

        public ICommand ChangeContentCommand { get; }

        public MyViewModel()
        {
            

            DetermineTypeOfWeek();
            ChangeContentCommand = new RelayCommand<string>(ExecuteChangeContentCommand);
            
        }

        


        private void ExecuteChangeContentCommand(string dayOfWeek)
        {
            // Set the content based on the day of the week
        }

        private void DetermineTypeOfWeek()
        {
            // Получение номера текущей недели
            int currentWeekNumber = GetCurrentWeekNumber();

            // Проверка четности/нечетности номера недели
            if (currentWeekNumber % 2 == 0)
            {
                TypeOfWeek = "Четная";
                App.CurrentWeekType = "Четная";
            }
            else
            {
                TypeOfWeek = "Нечетная";
                App.CurrentWeekType = "Нечетная";
            }
        }

        private int GetCurrentWeekNumber()
        {
            // Получение текущей даты
            DateTime currentDate = DateTime.Today;

            // Получение номера недели для текущей даты
            int currentWeekNumber = (currentDate.DayOfYear - 1) / 7 + 1;

            return currentWeekNumber;
        }
    }

    public class ScheduleViewModel : ObservableObject
    {
        private ObservableCollection<Lesson> _schedule;

        public ObservableCollection<Lesson> Schedule
        {
            get { return _schedule; }
            set { SetProperty(ref _schedule, value); }
        }

        public ScheduleViewModel()
        {
            Schedule = new ObservableCollection<Lesson>();

            string path = "Schedule.json";
            var dataSchedule = RootObjectSchedule.LoadJsonSchedule(path);

            string group = App.Group;

            string dayOfWeek = GetRussianDayOfWeek();
            App.ButtonCommandParameter = dayOfWeek;
            string targetWeek = App.CurrentWeekType;

            // Поиск соответствующей недели в расписании
            var selectedWeek = dataSchedule.schedule.FirstOrDefault(week => week.group == group && week.week == targetWeek);

            if (selectedWeek != null)
            {
                // Поиск соответствующего дня в выбранной неделе
                var selectedDay = selectedWeek.days.FirstOrDefault(day => day.day.ToLower() == dayOfWeek);

                if (selectedDay != null)
                {
                    
                    // Проход по всем классам в выбранном дне
                    foreach (var classInfo in selectedDay.classes)
                    {
                        // Создание нового объекта Lesson на основе данных класса
                        var lesson = new Lesson
                        {
                            Subject = classInfo.subject,
                            Teacher = classInfo.teacher,
                            Room = classInfo.room,
                            Week = selectedWeek.week,
                            Time = classInfo.time,
                            Type = classInfo.type,
                            Number = classInfo.number
                        };

                        // Добавление созданного урока в расписание
                        Schedule.Add(lesson);
                    }
                }
                else
                {
                    var lesson = new Lesson
                    {
                        Subject = "В этот день у вас выходной.",

                    };
                    Schedule.Add(lesson);
                }
            }

            
        }

        

        private string GetRussianDayOfWeek()
        {
            DateTime date = DateTime.Today;
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            string russianDayOfWeek = russianCulture.DateTimeFormat.GetDayName(date.DayOfWeek);

            return russianDayOfWeek;
        }

        public void ChangeSchedule(string dayOfWeek, string targetWeek)
        {
            // Очистка текущего расписания
            Schedule.Clear();

            string path = "Schedule.json";
            var dataSchedule = RootObjectSchedule.LoadJsonSchedule(path);

            string group = App.Group.ToLower();

            // Поиск соответствующей недели в расписании
            var selectedWeek = dataSchedule.schedule.FirstOrDefault(week => week.group == group && week.week == targetWeek);

            if (selectedWeek != null)
            {
                // Поиск соответствующего дня в выбранной неделе
                var selectedDay = selectedWeek.days.FirstOrDefault(day => day.day == dayOfWeek);

                if (selectedDay != null)
                {
                    // Проход по всем классам в выбранном дне
                    foreach (var classInfo in selectedDay.classes)
                    {
                        // Создание нового объекта Lesson на основе данных класса
                        var lesson = new Lesson
                        {
                            Subject = classInfo.subject,
                            Teacher = classInfo.teacher,
                            Room = classInfo.room,
                            Week = selectedWeek.week,
                            Time = classInfo.time,
                            Type = classInfo.type,
                            Number = classInfo.number
                        };

                        // Добавление созданного урока в расписание
                        Schedule.Add(lesson);
                    }
                }
                else
                {
                    var lesson = new Lesson
                    {
                        Subject = "В этот день у вас выходной.",

                    };
                    Schedule.Add(lesson);
                }
            }
        }

        public void RefreshSchedule()
        {
            // Вызовите OnPropertyChanged для свойства Schedule, чтобы обновить отображение расписания
            OnPropertyChanged(nameof(Schedule));
        }
    }

    public class Lesson : ObservableObject
    {
        private string _subject;
        private string _teacher;
        private string _room;
        private string _week;
        private string _time;
        private string _type;
        private string _number;

        public string Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value); }
        }

        public string Teacher
        {
            get { return _teacher; }
            set { SetProperty(ref _teacher, value); }
        }

        public string Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }

        public string Week
        {
            get { return _week; }
            set { SetProperty(ref _week, value); }
        }

        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
    }

    internal class DatePanelViewModel : ObservableObject
    {
        private Model.MainPage.DatePanelModel _mainPageModel;

        public DatePanelViewModel()
        {
            _mainPageModel = new Model.MainPage.DatePanelModel();
        }

        public ObservableCollection<string> Dates => _mainPageModel.Dates;

        public ObservableCollection<string> NameDays => _mainPageModel.NameDays;

        public ObservableCollection<Color> Colors => _mainPageModel.Colors;
    }
}
