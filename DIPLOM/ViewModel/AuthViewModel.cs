using Newtonsoft.Json;
using DIPLOM.Model.UserInfoModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using static DIPLOM.Model.Auth;
using DIPLOM.View;

namespace DIPLOM.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private FormStudiesModel _formStudiesModel;
        private UniversitiesModel _universitiesModel;
        private GroupsModel _groupsModel;

        private ObservableCollection<string> _formStudies;
        private ObservableCollection<string> _universities;
        private ObservableCollection<string> _groups;

        private string _selectedFormStudy;
        private string _selectedUniversity;
        private string _selectedGroup;
        private string _fio;

        public ObservableCollection<string> FormStudies
        {
            get { return _formStudies; }
            set
            {
                if (_formStudies != value)
                {
                    _formStudies = value;
                    OnPropertyChanged(nameof(FormStudies));
                }
            }
        }

        public ObservableCollection<string> Universities
        {
            get { return _universities; }
            set
            {
                if (_universities != value)
                {
                    _universities = value;
                    OnPropertyChanged(nameof(Universities));
                }
            }
        }

        public ObservableCollection<string> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups != value)
                {
                    _groups = value;
                    OnPropertyChanged(nameof(Groups));
                }
            }
        }

        public string SelectedFormStudy
        {
            get { return _selectedFormStudy; }
            set
            {
                if (_selectedFormStudy != value)
                {
                    _selectedFormStudy = value;
                    OnPropertyChanged(nameof(SelectedFormStudy));
                }
            }
        }

        public string SelectedUniversity
        {
            get { return _selectedUniversity; }
            set
            {
                if (_selectedUniversity != value)
                {
                    _selectedUniversity = value;
                    OnPropertyChanged(nameof(SelectedUniversity));
                }
            }
        }

        public string SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
                    OnPropertyChanged(nameof(SelectedGroup));
                }
            }
        }

        public string FIO
        {
            get { return _fio; }
            set
            {
                if (_fio != value)
                {
                    _fio = value;
                    OnPropertyChanged(nameof(FIO));
                }
            }
        }

        public ICommand SaveCommand { get; private set; }
        public INavigation Navigation { get; private set; }

        public AuthViewModel(INavigation navigation)
        {
            _formStudiesModel = new FormStudiesModel();
            _universitiesModel = new UniversitiesModel();
            _groupsModel = new GroupsModel();

            FormStudies = _formStudiesModel.GetFormStudies();
            Universities = _universitiesModel.GetUniversities();
            Groups = _groupsModel.GetGroups();
            Navigation = navigation;


            SaveCommand = new Command(Save);

             
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save()
        {
            RootObjectUserInfo.SaveJsonUserInfo(SelectedFormStudy, SelectedUniversity, SelectedGroup, FIO);
            App.Current.MainPage = new AppShell();
        }
        
    }
}
