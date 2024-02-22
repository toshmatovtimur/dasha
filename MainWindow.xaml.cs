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

namespace HumanResourcesDepartmentWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        //Войти
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrWhiteSpace(loginX.Text) && string.IsNullOrWhiteSpace(PasswX.Text) 
            || string.IsNullOrWhiteSpace(loginX.Text) || string.IsNullOrWhiteSpace(PasswX.Text))
            {
                MessageBox.Show("Вы пропустили поле\nПовторите попытку", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
               
            else
            {
                if(loginX.Text == "user" && PasswX.Text == "user")
                {
                    BaseWindow @base = new();
                    @base.Show();
                    Close();
                }

                else if (loginX.Text == "admin" && PasswX.Text == "admin")
                {
                    AdminWindow @admin = new();
                    @admin.Show();
                    Close();
                }
                else
                    MessageBox.Show("Неправильный логин или пароль\nПовторите попытку", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}