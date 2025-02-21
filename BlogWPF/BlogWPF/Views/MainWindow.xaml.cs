using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlogWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

	private void OnLoginBtnClick(object sender, RoutedEventArgs e)
	{
		this.MainFrame.NavigationService.Navigate(new Uri("Views/LoginPage.xaml", UriKind.Relative));
	}

	private void OnUserBtnClick(object sender, RoutedEventArgs e)
	{
		this.MainFrame.NavigationService.Navigate(new Uri("Views/UserPage.xaml", UriKind.Relative));
	}
}