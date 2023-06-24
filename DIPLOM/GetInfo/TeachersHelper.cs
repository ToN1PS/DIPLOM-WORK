using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DIPLOM.Model.CoursPaperModel;
using DIPLOM.Model.PracticeModel;
using DIPLOM.Model.ScheduleModel;
using DIPLOM.Model.TeachersModel;


namespace DIPLOM.GetInfo
{
    internal class TeachersHelper
    {
        public static string GetInfoTeachers(string input, string myGroup, string filepath)
        {
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);
            string fullName = ExtractFullName(input);
            int testINT; 
            int.TryParse(input, out testINT);


            if (fullName == null && App.ContextChat.Split(',').Length == 2)
            {
                if (App.ContextChat == null)
                {
                    App.ContextChat = $"преподаватель,{input}";
                }
                return "Введите верный формат ФИО (Фамилия И.О.)";
            }
            else if (testINT != 0)
            {
                
                fullName = App.ContextChat.Split(',').ToList()[2];
                return GetInfoTeacherCode(filepath, fullName, input);
            }

            List<string> req;
            if (App.ContextChat != null)
            {
                req = App.ContextChat.Split(',').ToList()[1].Split(' ').ToList();
            }
            else
            {
                req = input.Split(' ').ToList();
            }
            App.ContextChat = null;

            fullName = fullName.ToLower();

            string answer = GetAnswer(filepath, fullName, req);

            if (answer == "Преподаватель не найден")
            {
                App.ContextChat = $"преподаватель,{input}";
                return "Преподаватель не найден, возможно вы допустили ошибку";
            }
            else if (answer == "Введите код кафедры в которой находится преподаватель")
            {
                App.ContextChat = $"преподаватель,{input},{fullName}";
                string code = GetInfoCode(filepath, fullName);
                return $"Введите код кафедры в которой находится преподаватель\n {code}";
            }
            else
            {
                App.ContextChat = null;
            }

            return answer;
        }

        static string GetInfoTeacherCode(string filepath, string fullName, string newInput)
        {
            string oldInput = App.ContextChat.Split(',').ToList()[1].ToLower();
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);
            Department dep = dataTeacher.Departaments.Values.FirstOrDefault(i => i.Code == newInput);

            foreach (var item in oldInput.Split(' ').ToList())
            {
                if (dataTeacher.Keys.SubKeys.EmailKeys.Split(',').ToList().Contains(item))
                {
                    
                    var teacher = dep?.Teachers.Values.FirstOrDefault(t => t.Name.ToLower().Replace(".", "") == fullName);
                    if (teacher != null)
                    {
                        return $"Почта {teacher.Email}\nПреподаватель {teacher.Name}";
                    }
                }
                else if (dataTeacher.Keys.SubKeys.AddressKeys.Split(',').Contains(item))
                {
                    var teacher = dep?.Teachers.Values.FirstOrDefault(t => t.Name.ToLower().Replace(".", "") == fullName);
                    if (teacher != null)
                    {
                        return $"Адрес {teacher.Address}\nНомер кабинета кафедры {teacher.CabinetNumber}\n{teacher.Name}";
                    }
                }
                else if (dataTeacher.Keys.SubKeys.Schedule.Split(',').Contains(item))
                {
                    var teacher = dep?.Teachers.Values.FirstOrDefault(t => t.Name.ToLower().Replace(".", "") == fullName);
                    if (teacher != null) { 
                        return GetScheduleTeacher(filepath, fullName, newInput);
                    }
                }
            }
            return null;
        }

        static string GetInfoCode(string filepath, string fullName)
        {
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);
            StringBuilder res = new StringBuilder();

            foreach (var dep in dataTeacher.Departaments.Values)
            {
                foreach (var item in dep.Teachers.Values)
                {
                    if (item.Name.ToLower().Replace(".", "") == fullName)
                    {
                        res.AppendLine($"\nНазвание кафедры {dep.Title}");
                        res.AppendLine($"Код кафедры {dep.Code}");
                        
                    }
                }
            }

            return res.ToString();
        }
        static string ExtractFullName(string input)
        {
            string pattern = @"\b(?<lastName>[А-ЯЁа-яё]+)\s+(?<firstName>[А-ЯЁа-яё])\.?\s*(?<middleName>[А-ЯЁа-яё])?\b";
            Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string lastName = match.Groups["lastName"].Value;
                string firstName = match.Groups["firstName"].Value;
                string middleName = match.Groups["middleName"].Value;

                string fullName = $"{lastName} {firstName}";

                if (!string.IsNullOrEmpty(middleName))
                {
                    fullName += $" {middleName.Substring(0, 1)}.";
                }

                return fullName.Replace(".", ""); ;
            }
            else
            {
                return null;
            }
        }

        static TeacherResult GetTeacher(string filepath, string fullName)
        {
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);

            TeacherResult result = new TeacherResult();

            foreach (var dep in dataTeacher.Departaments.Values)
            {
                foreach (var teacher in dep.Teachers.Values)
                {
                    if (teacher.Name.ToLower().Replace(".", "") == fullName)
                    {
                        result.Teacher = teacher;
                        result.DepCode = dep.Code;
                        result.Number++;
                    }
                }
            }

            return result;
        }

        public class TeacherResult
        {
            public Teacher Teacher { get; set; }
            public int Number { get; set; }
            public string DepCode { get; set; }
        }

        static string GetAnswer(string filepath, string fullName, List<string> req)
        {
            var result = GetTeacher(filepath, fullName);
            
            Teacher teacher = result.Teacher;
            int number = result.Number;

            if (teacher == null)
            {
                return "Преподаватель не найден";
            }
            else if (number > 1)
            {
                return "Введите код кафедры в которой находится преподаватель";
            }
            //string depcode = ;
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);
            string answer = "Ошибка";

            foreach (string item in req)
            {
                if (dataTeacher.Keys.SubKeys.EmailKeys.Contains(item))
                {
                    answer = $"Почта {teacher.Email}\nПреподаватель {teacher.Name}";
                    break;
                }
                else if (dataTeacher.Keys.SubKeys.OfficeKeys.Contains(item) || dataTeacher.Keys.SubKeys.AddressKeys.Contains(item))
                {

                    answer = $"Адрес {teacher.Address}\nНомер кабинета кафедры {teacher.CabinetNumber}\n{teacher.Name}";
                    break;
                }
                else if (dataTeacher.Keys.SubKeys.Schedule.Contains(item))
                {
                    
                    
                    answer = GetScheduleTeacher(filepath, fullName, result.DepCode);
                    break;
                }
            }

            return answer;
        }
        static string GetCode(string filepath, string fullName)
        {
            var dataTeacher = RootObjectTeachers.LoadJsonTeachers(filepath);
            StringBuilder res = new StringBuilder();

            foreach (var dep in dataTeacher.Departaments.Values)
            {
                foreach (var item in dep.Teachers.Values)
                {
                    if (item.Name.ToLower().Replace(".", "") == fullName)
                    {
                        res.AppendLine($"\nНазвание кафедры {dep.Title}");
                        res.AppendLine($"Код кафедры {dep.Code}");

                    }
                }
            }

            return res.ToString();
        }

        static string GetScheduleTeacher(string filepath, string fullName, string depcode)
        {
            var dataSchedule = RootObjectSchedule.LoadJsonSchedule("Schedule.json");

            string schedule = "";
            

            foreach (var item in dataSchedule.schedule)
            {
                foreach (var day in item.days)
                {
                    foreach (var classItem in day.classes)
                    {
                        string test = classItem.teacher.Replace(".", "").ToLower();
                        string test2 = classItem.depCode;

                        if ((classItem.teacher.Replace(".", "").ToLower() == fullName))
                        {
                            if (depcode == classItem.depCode)
                            {
                                
                                schedule += $"\nДень - {day.day}\n";
                                schedule += $"Время - {classItem.time}\n";
                                schedule += $"Номер пары - {classItem.number} \n";
                                schedule += $"Кабинет - {classItem.room} \n";
                                schedule += $"Тип - {classItem.type}";
                            }
                            
                        }
                    }
                }
            }

            return schedule;
        }

    }

}
