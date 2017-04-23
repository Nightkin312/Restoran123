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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using WorkWithRegistry;
using WorkWithDatabase;

namespace IS_Restoran
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;
        //static private SqlConnection con = new SqlConnection(@"server = KINPC\MSSQLSERVER1; database = Restoran; Integrated Security = true;");
        string User;

        public MainMenu(string User)
        {
            InitializeComponent();

            this.User = User;

            RegistryKey AuthInfo = RegistryInfo.Key();
            if (AuthInfo.ValueCount.Equals(0))
            {
                AuthInfo.SetValue("DataSource", @"KINPC\MSSQLSERVER1");
                AuthInfo.SetValue("UserID", "sa");
                AuthInfo.SetValue("Password", "1234");
                AuthInfo.SetValue("InitialCatalog", "Restoran");
            }

            DataSource = AuthInfo.GetValue("DataSource").ToString();
            UserID = AuthInfo.GetValue("UserID").ToString();
            Password = AuthInfo.GetValue("Password").ToString();
            InitialCatalog = AuthInfo.GetValue("InitialCatalog").ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow Avtor = new MainWindow();
            Avtor.Show();
            this.Close();
            
        }

        private Boolean CheckAccess(string Catalog)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand com = new SqlCommand("SELECT + " + Catalog + " FROM Prava_dostupa WHERE Logi=@Logi ", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                com.Parameters.AddWithValue("@Logi", User); //в txt_bxUser вписываем поле user

                if ((bool)com.ExecuteScalar())
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
                return false;
            }
            finally
            {
                //con.Close();
            }
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
             if (CheckAccess("AdminForm"))
            {
                AdminForm AF = new AdminForm();
                AF.Show();
            } else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }

        }

        private void SklRab_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("SklRabForm"))
            {
                SklRabForm SklF = new SklRabForm();
                SklF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
            
        }

        private void KadrRab_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("KadrRabForm"))
            {
                KadrRabform KdrF = new KadrRabform();
                KdrF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
        }

        private void Shef_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("ShefForm"))
            {
                ShefForm ShF = new ShefForm();
                ShF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
        }

        private void Buhg_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("BuhgForm"))
            {
                BuhgForm BF = new BuhgForm();
                BF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
        }

        private void Ofic_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("OficForm"))
            {
                OficForm OF = new OficForm();
                OF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
        }

        private void ZalAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccess("ZalAdminForm"))
            {
                ZalAdminForm ZAF = new ZalAdminForm();
                ZAF.Show();
            }
            else
            {
                MessageBox.Show("У вас не достаточно прав доступа");
            }
        }
    }
}
