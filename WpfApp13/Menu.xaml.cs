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
using System.Windows.Shapes;
using WpfApp13.ApplicationData;

namespace WpfApp13
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public List<Statement> statements { get; set; }
        public Menu()
        {
            InitializeComponent();
            LoadS();
            LoadD();
            LoadR();
            Reasonss.DisplayMemberPath = "Name";
            Departmentss.DisplayMemberPath = "Name";
        }

        public void LoadS()
        {
            var list = AppConnect.ppEntities.Statement.ToList();
            foreach (var di in list)
            {
                Statement_List.ItemsSource = AppConnect.ppEntities.Statement.Where(x=> x.Status=="Принято").ToList();
            }
        }

        public void LoadD()
        {
            var Spec = AppConnect.ppEntities.Department.ToList();
            foreach (var sp in Spec)
            {
                Departmentss.Items.Add(sp);
            }
        }
        public void LoadR()
        {
            var Spec = AppConnect.ppEntities.Reason.ToList();
            foreach (var sp in Spec)
            {
                Reasonss.Items.Add(sp);
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Number_tb.Text.Length==0)
            {
                MessageBox.Show("Введите номер заявления", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Dat.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату заявления", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Surname_tb.Text.Length == 0)
            {
                MessageBox.Show("Введите фамилию заявителя", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Name_tb.Text.Length == 0)
            {
                MessageBox.Show("Введите имя заявителя", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Patronymic_tb.Text.Length == 0)
            {
                MessageBox.Show("Введите отчество заявителя", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Reasonss.SelectedItem == null)
            {
                MessageBox.Show("Выберите вопрос", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Departmentss.SelectedItem == null)
            {
                MessageBox.Show("Выберите отдел", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            else
            {
                var id = AppConnect.ppEntities.Statement.ToList().Count + 1;

                Statement statement = new Statement();
                statement.Number = id++;
                statement.Date = Dat.SelectedDate.Value;
                statement.Surname = Surname_tb.Text;
                statement.Name = Name_tb.Text;
                statement.Patronymic = Patronymic_tb.Text;
                statement.Status = "Принято";
                statement.Reason = Reasonss.Text;
                statement.Department = Departmentss.Text;

            

                AppConnect.ppEntities.Statement.Add(statement);


                int result = AppConnect.ppEntities.SaveChanges();

                if (result > 0)
                {
                    Number_tb.Text = "";
                    Dat.Text = "";
                    Surname_tb.Text = "";
                    Name_tb.Text = "";
                    Patronymic_tb.Text = "";
                    Departmentss.Text = "";
                    Reasonss.Text = "";

                    MessageBox.Show("Заявление принято");
                }

                else
                {
                    MessageBox.Show("ERROR");
                }

                LoadS();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
