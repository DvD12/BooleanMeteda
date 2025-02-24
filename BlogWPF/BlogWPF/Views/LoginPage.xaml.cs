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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
		}

		private async void OnLoginBtnClick(object sender, RoutedEventArgs e)
		{
			ApiService.Email = this.EmailTxt.Text;
			ApiService.Password = this.PasswordTxt.Text;
			var tokenApiResult = await ApiService.GetJwtToken();
			if (tokenApiResult.Data == null)
			{
				MessageBox.Show($"Errore login! {tokenApiResult.ErrorMessage}");
				return;
			}
			this.NavigationService.Navigate(new Uri("Views/UserPage.xaml", UriKind.Relative));
		}

		private async void OnRegisterBtnClick(object sender, RoutedEventArgs e)
		{
			ApiService.Email = this.EmailTxt.Text;
			ApiService.Password = this.PasswordTxt.Text;
			var registerApiResult = await ApiService.Register();
			if (registerApiResult.Data == false)
			{
				MessageBox.Show($"Errore registrazione! {registerApiResult.ErrorMessage}");
				return;
			}
			else
			{
				MessageBox.Show($"Registrazione avvenuta con successo!");
			}
		}
	}
}
