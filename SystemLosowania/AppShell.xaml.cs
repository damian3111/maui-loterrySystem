using SystemLosowania.Models;
using SystemLosowania.Views;

namespace SystemLosowania
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddStudent), typeof(AddStudent));
            Routing.RegisterRoute(nameof(Student), typeof(Student));
            Routing.RegisterRoute(nameof(StudentRepository), typeof(StudentRepository));
            Routing.RegisterRoute(nameof(Students), typeof(Students));
            Routing.RegisterRoute(nameof(UpdateStudent), typeof(UpdateStudent));

        }
    }
}