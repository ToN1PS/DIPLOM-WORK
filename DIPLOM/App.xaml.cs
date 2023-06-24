using DIPLOM.View;
using DIPLOM.Model.UserInfoModel;

namespace DIPLOM;

public partial class App : Application
{
    public static string CurrentWeekType { get; set; }
    public static string ButtonCommandParameter { get; set; }
    public static string ContextChat { get; set; }

    public static string Group { get; set; }

    public App()
	{
        InitializeComponent();
        bool isFirstRun = IsFirstRun();

        MainPage = isFirstRun ? new Auth() : new AppShell();
    }

    private bool IsFirstRun()
    {
        // Проверяем состояние первого запуска, например, считывая значение из настроек приложения или базы данных
        // Если значение не установлено, считаем, что это первый запуск
        // После проверки, устанавливаем флаг первого запуска в false
        // Например:
        string fileName = "UserInfo.json";
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);


        return string.IsNullOrEmpty(path);


    }
}
