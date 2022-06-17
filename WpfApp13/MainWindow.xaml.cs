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
using WpfApp13.ApplicationData;

namespace WpfApp13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.ppEntities = new PPEntities();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var patientObj = AppConnect.ppEntities.Employee.FirstOrDefault(x => x.Login == Log.Text && x.Password == Pass.Password);
                if (patientObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (patientObj.Role=="Секретарь")
                    {
                        Menu menu = new Menu();
                        menu.Show();
                        Close();
                        MessageBox.Show("Здравствуйте " + patientObj.Surname + " " + patientObj.Name + " " + patientObj.Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }               
                    else
                    {
                        Menu2 menu2 = new Menu2();
                        menu2.Show();
                        Close();
                        MessageBox.Show("Здравствуйте " + patientObj.Surname + " " + patientObj.Name + " " + patientObj.Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка" + Ex.Message.ToString() + "Критическая работа приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
    }
}
