using BlogWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlogWPF
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            //bool isAdmin = false;
            //this.BackBtn.Visibility = isAdmin ? Visibility.Visible : Visibility.Hidden; // Come tramutiamo in XAML?
		}

		private void OnBackBtn(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Uri("Views/LoginPage.xaml", UriKind.Relative));
		}
	}
}
