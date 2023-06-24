using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DIPLOM.ViewModel;
using System.Windows.Input;
using DIPLOM.Tamplates;
using System;
using System.Collections.ObjectModel;
using DIPLOM.Model.UserInfoModel;
using Newtonsoft.Json;

namespace DIPLOM.View
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            
            BindingContext = new MyViewModel();
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(View.SettingsPageView), typeof(View.SettingsPageView));

            ChangeColor();

        }



        private void ChangeColor()
        {
            var buttons = new List<Button>();
            buttons.Add(Button1);
            buttons.Add(Button2);
            buttons.Add(Button3);
            buttons.Add(Button4);
            buttons.Add(Button5);
            buttons.Add(Button6);
            buttons.Add(Button7);
            foreach(Button button in buttons)
            {
                if (App.ButtonCommandParameter == button.CommandParameter.ToString().ToLower())
                {
                    _previousButton = button;
                    _previousBackgroundColor = button.BackgroundColor;
                    _previousColorText = button.TextColor;

                    button.BackgroundColor = Color.FromRgb(250, 240, 230);
                    button.TextColor = Color.FromRgb(0, 0, 0);
                    break;
                }
            }
            
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("SettingsPageView");
        }


        private void ChangeContentCommand(object sender, EventArgs e, string dayOfWeek)
        {
            // Получите экземпляр MyViewModel из BindingContext
            var viewModel = (MyViewModel)BindingContext;


            // Вызовите команду ChangeContentCommand в модели представления
            viewModel.ChangeContentCommand.Execute(dayOfWeek);
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            
            

            var button = (Button)sender;
            
            
            if (_previousButton != null)
            {
                _previousButton.BackgroundColor = _previousBackgroundColor;
                _previousButton.TextColor = _previousColorText;
            }
            // Сохранить текущую кнопку и ее цвет
            _previousButton = button;
            _previousBackgroundColor = button.BackgroundColor;
            _previousColorText = button.TextColor;
            
            button.BackgroundColor = Color.FromRgb(250, 240, 230);
            button.TextColor = Color.FromRgb(0, 0, 0);

            string dayOfWeek = (string)button.CommandParameter;
            string week = (string)TypeOfWeekLabel.Text;

            // Получите экземпляр ScheduleViewModel из BindingContext
            var scheduleViewModel = (ScheduleViewModel)ScheduleTemplate.BindingContext;

            // Измените расписание в соответствии с выбранным днем недели
            scheduleViewModel.ChangeSchedule(dayOfWeek, week);

            // Обновите отображение расписания
            scheduleViewModel.RefreshSchedule();
        }

        private Button _previousButton;
        private Color _previousBackgroundColor;
        private Color _previousColorText;
    }

}