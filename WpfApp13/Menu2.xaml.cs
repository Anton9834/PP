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
    /// Логика взаимодействия для Menu2.xaml
    /// </summary>
    public partial class Menu2 : Window
    {
        public List<Statement> statements { get; set; }
        public List<Department> departments { get; set; }
        public Menu2()
        {
            InitializeComponent();
            LoadS();
            //Departmentss.DisplayMemberPath = "Name";
            DataContext = this;
        }

        public void LoadS()
        {
            var list = AppConnect.ppEntities.Department.ToList();
            foreach (var di in list)
            {
                Departmentss.Items.Add(di);
            }
        }
       

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var order = Statement_List.SelectedItem as Statement;

            if (order != null)
            {

                var orderChanged = AppConnect.ppEntities.Statement.Where(o => o.Number == order.Number).FirstOrDefault();
                if (orderChanged != null)
                {
                    orderChanged.Status = "Завершено";
                    

                    
                    MessageBox.Show("Заявление рассмотрено!!!");
                }
            }
            AppConnect.ppEntities.SaveChanges();
        }

        private void Departmentss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var special = Departmentss.SelectedItem as Department;
            if (special != null)
            {
                Statement_List.ItemsSource = AppConnect.ppEntities.Statement.Where(c => c.Status == "Принято" && c.Department == special.Name).ToList();
            }
            Statement_List.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
