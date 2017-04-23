using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
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
using WorkWithDatabase;
using WorkWithRegistry;
using WorkWithCrypt;

namespace IS_Restoran
{
    /// <summary>
    /// Логика взаимодействия для ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        public ConnectWindow()
        {
            InitializeComponent();
        }


                 private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh.IsEnabled = false;
            DataSourceList.Items.Clear();
            DataSourceList.Items.Add(@"KINPC\MSSQLSERVER1");
            SqlDataSourceEnumerator ServerList = SqlDataSourceEnumerator.Instance;
            // Блокируем кнопку обновления и запускаем поиск списка серверов в отдельном потоке
            Task.Run(() =>
            {
                DataTable ServerTable = ServerList.GetDataSources();
                foreach (DataRow row in ServerTable.Rows)
                {
                    DataSourceList.Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        DataSourceList.Items.Add(row[0]);
                    }));
                }

                Refresh.Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    Refresh.IsEnabled = true;
                }));

            });

        }
       
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection TestConnection = new SqlConnection();
                //TestConnection = Connector.Connection(DataSourceList.SelectedItem.ToString(),
                // User.Text, Pass.Password, "Restoran");
                TestConnection.ConnectionString = new SqlConnectionStringBuilder()
                {
                    DataSource = @"KINPC\MSSQLSERVER1",
                    UserID = "sa",
                    Password = "1234",
                    InitialCatalog = "master",
                    PersistSecurityInfo = true,
                    ConnectTimeout = 5
                }.ConnectionString;
                TestConnection.Open();
                //MessageBox.Show("Norm");
                // Получаем список баз данных с сервера
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", TestConnection))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        DatabaseList.Items.Clear();
                        while (dr.Read())
                        {
                            DatabaseList.Items.Add(dr[0].ToString());
                        }
                    }
                }
                TestConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           // ToAuthControl();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey AuthInfo = RegistryInfo.Key();

                AuthInfo.SetValue("DataSource", DataSourceList.SelectedItem.ToString() /*@"KINPC\MSSQLSERVER1"*/);
                AuthInfo.SetValue("InitialCatalog", DatabaseList.SelectedItem.ToString());
                AuthInfo.SetValue("UserID", User.Text);
                AuthInfo.SetValue("Password", Pass.Password);

               
            }
            catch
            {
                MessageBox.Show("Не удалось записать данные в реестр!", "Ошибка сохранения подключения");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataSourceList.Items.Add("localhost");
        }
    }
    }

