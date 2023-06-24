using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPLOM.Model.UserInfoModel;

namespace DIPLOM.ViewModel
{
    public class EducationalViewModel : INotifyPropertyChanged
    {

        private string _formStudies;
        public string FormStudies
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

        private string _universities;
        public string Universities
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

        private string _group;
        public string Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged(nameof(Group));
                }
            }
        }

        private string _selectedUniversity;
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

        private string _selectedGroup;
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

        private string _fio;
        public string Fio
        {
            get { return _fio; }
            set
            {
                if (_fio != value)
                {
                    _fio = value;
                    OnPropertyChanged(nameof(Fio));
                }
            }
        }

        // Другие свойства и методы ViewModel

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EducationalViewModel()
        {
            var data = RootObjectUserInfo.LoadJsonUserInfo();
            Group = data.User.Group;
            SelectedUniversity = data.User.Universities;
            Fio = data.User.FIO;
            FormStudies = data.User.FormStudies;

        }


    }
}
