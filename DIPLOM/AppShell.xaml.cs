using DIPLOM.Model.UserInfoModel;

namespace DIPLOM
{



    public partial class AppShell : Shell
    {
	    public AppShell()
	    {
		    InitializeComponent();
            LoadGroup();
            Routing.RegisterRoute(nameof(View.MainPage), typeof(View.MainPage));
        

        }

        private void LoadGroup()
        {
        
            var data = RootObjectUserInfo.LoadJsonUserInfo();
            App.Group = data.User.Group.ToLower().ToString();
        }

    }
}