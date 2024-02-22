using System.Collections.ObjectModel;
using SystemLosowania.Models;
using SystemLosowania.Views;

namespace SystemLosowania
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {

            InitializeComponent();
        }


        private void btnAddCategory_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var parameter = button.Text;

            
            Shell.Current.GoToAsync($"{nameof(Students)}?class={parameter}");
        }
    }
}