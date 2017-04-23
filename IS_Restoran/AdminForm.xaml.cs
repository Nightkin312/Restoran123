using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WorkWithDatabase;
using WorkWithRegistry;

namespace IS_Restoran
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        //SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
        //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
        public AdminForm()
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

        private void Otris()
        {
            SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
            //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
            SqlCommand Userscom = new SqlCommand("SELECT * FROM  UsersView", con); //создание выборки username и pass из таблицы tbl_log
            SqlCommand Rollscom = new SqlCommand("SELECT * FROM  roll", con);

            con.Open(); //открытие подключения

            DataTable UsersDT = new DataTable();
            DataTable RollsDT = new DataTable();

            UsersDT.Load(Userscom.ExecuteReader());
            RollsDT.Load(Rollscom.ExecuteReader());

            UsersTbl.ItemsSource = UsersDT.DefaultView;
            RollTbl.ItemsSource = RollsDT.DefaultView;

            if (UsersTbl.HasItems)
            {
                UsersTbl.Columns[4].Visibility = Visibility.Hidden;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Otris();
        }

        /*private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*SqlConnection con = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddUserCom = new SqlCommand("AddLoginForm", con); //создание выборки username и pass из таблицы tbl_log
                AddUserCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddUserCom.Parameters.AddWithValue("@Logi", Login.Text);
                AddUserCom.Parameters.AddWithValue("@Pass", Password.Text);
                AddUserCom.Parameters.AddWithValue("@ID_Roll", Roll.Text);
            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                con.Close();
            }
        }*/

        private void PravDost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (PravDost.SelectedItem == null) return;
            DataRowView row = (DataRowView)PravDost.SelectedItem;
            Login.Text = row["Logi"].ToString();
            Password.Text = row["Pass"].ToString();
            Roll.Text = row["Name_roll"].ToString();
            AdminPrava.IsChecked = Convert.ToBoolean(row["AdminForm"]);
            SklPrava.IsChecked = Convert.ToBoolean(row["SklRabForm"]);
            KadrPrava.IsChecked = Convert.ToBoolean(row["KadrRabForm"]);
            ShefPrava.IsChecked = Convert.ToBoolean(row["ShefForm"]);
            BuhgPrava.IsChecked = Convert.ToBoolean(row["BuhgForm"]);
            OficPrava.IsChecked = Convert.ToBoolean(row["OficForm"]);
            ZalAdminPrava.IsChecked = Convert.ToBoolean(row["ZalAdminForm"]);*/
        }

        private void UsersTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)UsersTbl.SelectedItem;
            Login.Text = row["Логин"].ToString();
            Pass.Text = row["Пароль"].ToString();

            GetRolls();

            foreach(IDComboBoxItem item in Roll.Items)
            {
                if (item.ID ==  (int) row["ID_Roll"])
                {
                    Roll.SelectedIndex = Roll.Items.IndexOf(item);
                }
            }
        }

        private void RollTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RollTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)RollTbl.SelectedItem;
            RollName.Text = row["Name_roll"].ToString();
            AdminPrava.IsChecked = Convert.ToBoolean(row["AdminForm"]);
            SklPrava.IsChecked = Convert.ToBoolean(row["SklRabForm"]);
            KadrPrava.IsChecked = Convert.ToBoolean(row["KadrRabForm"]);
            ShefPrava.IsChecked = Convert.ToBoolean(row["ShefForm"]);
            BuhgPrava.IsChecked = Convert.ToBoolean(row["BuhgForm"]);
            OficPrava.IsChecked = Convert.ToBoolean(row["OficForm"]);
            ZalAdminPrava.IsChecked = Convert.ToBoolean(row["ZalAdminForm"]);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (Roll.SelectedItem == null) return;

            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddUserCom = new SqlCommand("AddLoginForm", con); //создание выборки username и pass из таблицы tbl_log
                AddUserCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddUserCom.Parameters.AddWithValue("@Logi", Login.Text);
                AddUserCom.Parameters.AddWithValue("@Pass", Pass.Text);
                AddUserCom.Parameters.AddWithValue("@ID_Roll", (int) (Roll.SelectedItem as IDComboBoxItem).ID);
                AddUserCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void UpdUser_Click(object sender, RoutedEventArgs e)
        {
            if (Roll.SelectedItem == null) return;
            if (UsersTbl.SelectedItem == null) return;
            DataRowView User = (DataRowView)UsersTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdUserCom = new SqlCommand("UpdateLoginForm", con); //создание выборки username и pass из таблицы tbl_log
                UpdUserCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdUserCom.Parameters.AddWithValue("@ID_LoginForm", (int) User["ID_Login"]);
                UpdUserCom.Parameters.AddWithValue("@Logi", Login.Text);
                UpdUserCom.Parameters.AddWithValue("@Pass", Pass.Text);
                UpdUserCom.Parameters.AddWithValue("@ID_Roll", (int)(Roll.SelectedItem as IDComboBoxItem).ID);
                UpdUserCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void DelUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersTbl.SelectedItem == null) return;
            DataRowView User = (DataRowView)UsersTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelUserCom = new SqlCommand("DelLoginForm", con); //создание выборки username и pass из таблицы tbl_log
                DelUserCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelUserCom.Parameters.AddWithValue("@ID_LoginForm", (int)User["ID_Login"]);
                DelUserCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void AddRoll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddRollCom = new SqlCommand("AddRoll", con); //создание выборки username и pass из таблицы tbl_log
                AddRollCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddRollCom.Parameters.AddWithValue("@Name_roll", RollName.Text);
                AddRollCom.Parameters.AddWithValue("@AdminForm", AdminPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@SklRabForm", SklPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@KadrRabForm", KadrPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@ShefForm", ShefPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@BuhgForm", BuhgPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@OficForm", OficPrava.IsChecked);
                AddRollCom.Parameters.AddWithValue("@ZalAdminForm", ZalAdminPrava.IsChecked);
                AddRollCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void UpdRoll_Click(object sender, RoutedEventArgs e)
        {
            if (RollTbl.SelectedItem == null) return;
            DataRowView Roll = (DataRowView)RollTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdRollCom = new SqlCommand("UpdateRoll", con); //создание выборки username и pass из таблицы tbl_log
                UpdRollCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdRollCom.Parameters.AddWithValue("@ID_Roll", (int)Roll["ID_Roll"]);
                UpdRollCom.Parameters.AddWithValue("@Name_roll", RollName.Text);
                UpdRollCom.Parameters.AddWithValue("@AdminForm", AdminPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@SklRabForm", SklPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@KadrRabForm", KadrPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@ShefForm", ShefPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@BuhgForm", BuhgPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@OficForm", OficPrava.IsChecked);
                UpdRollCom.Parameters.AddWithValue("@ZalAdminForm", ZalAdminPrava.IsChecked);
                UpdRollCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void DelRoll_Click(object sender, RoutedEventArgs e)
        {
            if (RollTbl.SelectedItem == null) return;
            DataRowView Roll = (DataRowView)RollTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelRollCom = new SqlCommand("DelRoll", con); //создание выборки username и pass из таблицы tbl_log
                DelRollCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelRollCom.Parameters.AddWithValue("@ID_Roll", (int)Roll["ID_Roll"]);
                DelRollCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            Otris();
        }

        private void Roll_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void Roll_DropDownOpened(object sender, EventArgs e)
        {
            GetRolls();
        }

        private void GetRolls()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektRollscmd = new SqlCommand("SELECT ID_Roll, Name_roll FROM Roll", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable table = new DataTable();
                table.Load(SelektRollscmd.ExecuteReader());
                Roll.Items.Clear();
                foreach (DataRow Row in table.Rows)
                {
                    Roll.Items.Add(new IDComboBoxItem((int)Row["ID_Roll"], Row["Name_roll"].ToString()));

                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
        }
    }


}
