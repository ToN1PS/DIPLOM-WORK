using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPLOM.GetInfo;
using DIPLOM.Model.CoursPaperModel;
using DIPLOM.Model.MethodistModel;
using DIPLOM.Model.PracticeModel;
using DIPLOM.Model.ScheduleModel;
using DIPLOM.Model.TeachersModel;
using DIPLOM;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;


namespace DIPLOM.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private string _messageText;
        public Entry EntryField { get; set; }
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        private string _userMessageText;
        public string UserMessageText
        {
            get { return _userMessageText; }
            set
            {
                _userMessageText = value;
                OnPropertyChanged(nameof(UserMessageText));
            }
        }

        private string _botResponseText;
        public string BotResponseText
        {
            get { return _botResponseText; }
            set
            {
                _botResponseText = value;
                OnPropertyChanged(nameof(BotResponseText));
            }
        }

        public ICommand SendMessageCommand { get; private set; }

        public ObservableCollection<ChatMessage> ChatMessages { get; private set; }
        public ScrollView ChatScrollView { get; set; }
        public ListView ChatListView { get; set; }

        public ChatViewModel()
        {
            SendMessageCommand = new Command(ExecuteSendMessageCommand);
            ChatMessages = new ObservableCollection<ChatMessage>();
            
        }
       
        private async void ExecuteSendMessageCommand()
        {
            string myGroup = App.Group;

            string botResponse = GetBotResponse(MessageText.ToLower(), myGroup);




            var userMessage = new ChatMessage { UserMessageText = MessageText, TimeStamp = DateTime.Now.ToString("HH:mm"), 
                BotResponseText = botResponse, BotResponseTimeStamp = DateTime.Now.ToString("HH:mm") };
            ChatMessages.Add(userMessage);




            MessageText = string.Empty;

            await ChatScrollView.ScrollToAsync(0, ChatScrollView.ContentSize.Height, true);

            


        }










        private string GetBotResponse(string input, string myGroup)
        {
            string coursPaperJsonName = "PDFfilesCoursePaper.json"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "PDFfilesCoursePaper.json");
            //string coursPaperJsonPath = "Resources/Data/PDFfilesCoursePaper.json";
            var dataCoursePaper = RootObjectCoursePaper.LoadJsonPDFfilesCoursePaper(coursPaperJsonName);
            List<string> keysCoursePaper = dataCoursePaper.Keys.MainKeys.Split(',').ToList();


            string methodistJsonName = "Methodists.json";
            var dataMethodists = RootObjectMethodist.LoadJsonMethodists(methodistJsonName);
            List<string> keysMethodist = dataMethodists.Keys.MainKeys.Split(',').ToList();



            string practiceJsonName = "PDFfilesPractice.json";
            var dataPractice = RootObjectPractice.LoadJsonPDFfilesPractice(practiceJsonName);
            List<string> keysPractice = dataPractice.Keys.MainKeys.Split(',').ToList();



            string TeachersJsonName = "Teachers.json";
            var dataTeachers = RootObjectTeachers.LoadJsonTeachers(TeachersJsonName);
            List<string> keysTeachers = dataTeachers.Keys.MainKeys.Split(',').ToList();


            List<string> listInput = input.Split(' ').ToList();


            foreach (var item in listInput)
            {
                if (keysMethodist.Contains(item.ToLower()))
                {
                    return MethodistHelper.GetInfoMethodist(listInput, myGroup, methodistJsonName);
                }
                else if (keysCoursePaper.Contains(item.ToLower()) || App.ContextChat == "методичка")
                {
                    return CoursePaperHelper.GetInfoCoursePaper(listInput, myGroup, coursPaperJsonName);
                }
                else if (keysPractice.Contains(item.ToLower()) || App.ContextChat == "практика")
                {
                    return PracticeHelper.GetInfoPractice(listInput, myGroup, practiceJsonName);
                }
                else if (keysTeachers.Contains(item.ToLower()) || (App.ContextChat != null && App.ContextChat.Split(',').Any(c => "преподаватель".Contains(c))))
                {
                    return TeachersHelper.GetInfoTeachers(input, myGroup, TeachersJsonName);
                }
            }
            // Замените этот возврат реальной логикой вашего бота
            return "Запрос задан некорректно";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    

    public class ChatMessage
    {
        public string UserMessageText { get; set; }
        public string BotResponseText { get; set; }

        public string TimeStamp { get; set; } // Время запроса пользователя
        public string BotResponseTimeStamp { get; set; } // Время ответа бота


    }

}
