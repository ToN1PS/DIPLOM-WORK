using DIPLOM.ViewModel;

namespace DIPLOM.View;

public partial class EducationalInstitutionView : ContentPage
{
	public EducationalInstitutionView()
	{
		InitializeComponent();
        BindingContext = new EducationalViewModel();
    }
    private void FormStudyClicked(object sender, EventArgs e)
    {
        FormStudy.IsVisible = false;

        if (FormStudy.IsVisible)
        {
            FormStudy.Focus();
            
        }
        else
        {
            FormStudy.IsVisible = true;
            FormStudy.Focus();
        }
    }

    private void UniversityClicked(object sender, EventArgs e)
    {
        University.IsVisible = false;

        if (University.IsVisible)
        {
            University.Focus();

        }
        else
        {
            University.IsVisible = true;
            University.Focus();
        }
    }

    private void GroupNameClicked(object sender, EventArgs e)
    {
        GroupName.IsVisible = false;

        if (GroupName.IsVisible)
        {
            GroupName.Focus();

        }
        else
        {
            GroupName.IsVisible = true;
            GroupName.Focus();
        }
    }
    private void FIOClicked(object sender, EventArgs e)
    {
        FIO.IsEnabled = true;
        framew.IsVisible= true;
        FIO.Focus();
    }

    private void FIOWriht(object sender, EventArgs e)
    {
        
        framew.IsVisible = false;
        FIO.IsEnabled = false;
    }
}