using DIPLOM.ViewModel;
using Microsoft.Maui.Controls.Platform;

namespace DIPLOM.View
{
    public partial class Auth : ContentPage
    {
        public Auth()
        {
            InitializeComponent();
            
            BindingContext = new AuthViewModel(Navigation);
        }


    }
}
