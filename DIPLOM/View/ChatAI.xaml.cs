using DIPLOM.ViewModel;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;

namespace DIPLOM.View
{
    public partial class ChatAI : ContentPage, INotifyPropertyChanged
    {
        private ChatViewModel _viewModel;
        
        public ChatAI()
        {
            IgnoresContainerArea = true;

            InitializeComponent();
            _viewModel = new ChatViewModel();
            BindingContext = _viewModel;

            _viewModel.ChatScrollView = ChatScrollView;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ChatViewModel.UserMessageText))
                    OnPropertyChanged(nameof(UserMessageText));
                else if (args.PropertyName == nameof(ChatViewModel.BotResponseText))
                    OnPropertyChanged(nameof(BotResponseText));
            };
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("SettingsPageView");
        }

        private void SendClick(object sender, EventArgs e)
        {
            // No action is required here, the command is bound to the ViewModel
            

        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UserMessageText
        {
            get { return _viewModel.UserMessageText; }
            set
            {
                _viewModel.UserMessageText = value;
                OnPropertyChanged(nameof(UserMessageText));
            }
        }

        public string BotResponseText
        {
            get { return _viewModel.BotResponseText; }
            set
            {
                _viewModel.BotResponseText = value;
                OnPropertyChanged(nameof(BotResponseText));
            }
        }
    }
}
