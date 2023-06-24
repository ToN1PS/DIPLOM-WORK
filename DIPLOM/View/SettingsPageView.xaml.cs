namespace DIPLOM.View;

public partial class SettingsPageView : ContentPage
{
	public SettingsPageView()
	{
		InitializeComponent();
        
    }

    private async void OnClickinstitution(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EducationalInstitutionView());
    }
    private void UpdateButton(object sender, EventArgs e)
    {
        UpdateFrame.IsVisible = true;
    }


    private void UpdateClose(object sender, EventArgs e)
    {
        UpdateFrame.IsVisible = false;
    }

}