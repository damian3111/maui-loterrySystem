using SystemLosowania.Models;

namespace SystemLosowania.Views;


[QueryProperty(nameof(StundentId), "Id")]
public partial class UpdateStudent : ContentPage
{
	public UpdateStudent()
	{
		InitializeComponent();
	}

    string studentId;

    public String StundentId
    {
        set
        {
            studentId = value;
        }
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        StudentRepository.ReadStudentsFromXml();
        
    }

    private void btnCreate_Clicked(object sender, EventArgs e)
    {
        StudentRepository.RemoveProductFromXml(studentId);
        Student student = new Student();
        student.Id = studentId;
        student.Firstname = firstname.Text;
        student.Lastname = lastname.Text;
        student.Class = (string)classPicker.SelectedItem;

        StudentRepository.AddStudent(student);
        StudentRepository.ReadStudentsFromXml();

       

        Shell.Current.GoToAsync("..");
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {

    }
}