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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using WorkWithRegistry;
using WorkWithDatabase;

namespace IS_Restoran
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        public MainWindow()
        {
            InitializeComponent();

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

        private void btn_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand com = new SqlCommand("SELECT * FROM LoginForm WHERE Logi=@username and Pass=@password", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                com.Parameters.AddWithValue("@username", txt_bxUser.Text); //в txt_bxUser вписываем поле user
                com.Parameters.AddWithValue("@password", txt_bxPass.Password);  //в txt_bxUser вписываем поле pass
                SqlDataReader Dr = com.ExecuteReader(); //считывание com
                if (Dr.HasRows == true) //если строки в БД = true
                {
                    this.Hide();
                    MainMenu GO = new MainMenu(txt_bxUser.Text); //вывод главной формы
                    GO.Show(); //показываем главную форму
                    
                }
                else //в противном случае
                {
                    MessageBox.Show("Имя пользователя и/или пароль неверные"); //выводим сообщение
                }
            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
        }


        private void btn_ext_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Avtoriz_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Con_btn_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindow Connection = new ConnectWindow();
            Connection.Show();
        }
    }
}
