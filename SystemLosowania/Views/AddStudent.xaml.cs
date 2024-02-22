using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class AddStudent : ContentPage
{
	public AddStudent()
	{
		InitializeComponent();
	}

    private void btnCreate_Clicked(object sender, EventArgs e)
    {

        Student student = new Student();
        student.Id = Guid.NewGuid().ToString();
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