using System.Collections.ObjectModel;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

[QueryProperty(nameof(StundentClass), "class")]
public partial class Students : ContentPage
{

    public Students()
	{
		InitializeComponent();

    }

    List<Student> students;
    List<string> studentIds;
    Queue<Student> studentQueue = new Queue<Student>();
    private string studentClass;

    public String StundentClass
    {
        set
        {
            studentClass = value;
        }
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        StudentRepository.ReadStudentsFromXml();
        students = StudentRepository.getStudents(studentClass);
        studentIds = students.Select(student => student.Id).ToList();
        studentQueue.Clear();
        listContacts.ItemsSource = students;
    }


    private void btnAddCategory_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddStudent));
    }



    public void randomStudent(object sender, EventArgs e)
    {

        Random rand = new Random();
        int randomIndex = rand.Next(0, studentIds.Count);

        try {
            if (studentQueue.Count() == 4) {

                studentIds.Add(studentQueue.Dequeue().Id);
            }

            string v = studentIds[randomIndex];
            Student student = students.FirstOrDefault(student => student.Id == v);
            List<Student> ts = new List<Student>();
            ts.Add(student);
            listContacts.ItemsSource = ts;
            studentIds.Remove(student.Id);
            studentQueue.Enqueue(student);

        }
        catch(Exception ex) {
            DisplayAlert("Error", $"Cannot get random student!", "OK");
        }


    }

    private void listCategories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            Student selectedStudent = (Student)e.SelectedItem;
            string id = selectedStudent.Id;

            Student studentToRemove = students.Find(s => s.Id == id);

            if (studentToRemove != null)
            {
                studentIds.Remove(studentToRemove.Id);
                DisplayAlert($"Selected {studentToRemove.Firstname}", $" {studentToRemove.Firstname}, {studentToRemove.Lastname}, {studentToRemove.Class}", "OK");

            }
        }
    }

    private void updateStudent(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        string id = (String)button.CommandParameter;


        Shell.Current.GoToAsync($"{nameof(UpdateStudent)}?Id={id}");
    }
}