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
    /// Логика взаимодействия для ZalAdminForm.xaml
    /// </summary>
    public partial class ZalAdminForm : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        //SqlConnection conZalAdmin = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;");

        public ZalAdminForm()
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

        private void otrisZalAdmin()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand StolCom = new SqlCommand("SELECT * FROM  Stol", con);
                SqlCommand KlientCom = new SqlCommand("SELECT * FROM  Klient", con);
                SqlCommand BronCom = new SqlCommand("SELECT * FROM  Bron", con);
                SqlCommand StrafCom = new SqlCommand("SELECT * FROM  Straf", con);
                SqlCommand MebelCom = new SqlCommand("SELECT * FROM  Mebel", con);
                SqlCommand SpisanieCom = new SqlCommand("SELECT * FROM  Spisanie", con);
                SqlCommand ZamenaCom = new SqlCommand("SELECT * FROM  Zamena", con);

                con.Open(); //открытие подключения

                DataTable StolDT = new DataTable();
                DataTable KlientDT = new DataTable();
                DataTable BronDT = new DataTable();
                DataTable StrafDT = new DataTable();
                DataTable MebelDT = new DataTable();
                DataTable SpisanieDT = new DataTable();
                DataTable ZamenaDT = new DataTable();

                StolDT.Load(StolCom.ExecuteReader());
                KlientDT.Load(KlientCom.ExecuteReader());
                BronDT.Load(BronCom.ExecuteReader());
                StrafDT.Load(StrafCom.ExecuteReader());
                MebelDT.Load(MebelCom.ExecuteReader());
                SpisanieDT.Load(SpisanieCom.ExecuteReader());
                ZamenaDT.Load(ZamenaCom.ExecuteReader());

                StolTbl.ItemsSource = StolDT.DefaultView;
                KlientTbl.ItemsSource = KlientDT.DefaultView;
                BronTbl.ItemsSource = BronDT.DefaultView;
                StrafTbl.ItemsSource = StrafDT.DefaultView;
                MebelTbl.ItemsSource = MebelDT.DefaultView;
                SpisanieTbl.ItemsSource = SpisanieDT.DefaultView;
                ZamenaTbl.ItemsSource = ZamenaDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //conZalAdmin.Close();
            }
        }

        private void ZalAdminForm1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand StolCom = new SqlCommand("SELECT * FROM  Stol", con);
                SqlCommand KlientCom = new SqlCommand("SELECT * FROM  Klient", con);
                SqlCommand BronCom = new SqlCommand("SELECT * FROM  Bron", con);
                SqlCommand StrafCom = new SqlCommand("SELECT * FROM  Straf", con);
                SqlCommand MebelCom = new SqlCommand("SELECT * FROM  Mebel", con);
                SqlCommand SpisanieCom = new SqlCommand("SELECT * FROM  Spisanie", con);
                SqlCommand ZamenaCom = new SqlCommand("SELECT * FROM  Zamena", con);

                con.Open(); //открытие подключения

                DataTable StolDT = new DataTable();
                DataTable KlientDT = new DataTable();
                DataTable BronDT = new DataTable();
                DataTable StrafDT = new DataTable();
                DataTable MebelDT = new DataTable();
                DataTable SpisanieDT = new DataTable();
                DataTable ZamenaDT = new DataTable();

                StolDT.Load(StolCom.ExecuteReader());
                KlientDT.Load(KlientCom.ExecuteReader());
                BronDT.Load(BronCom.ExecuteReader());
                StrafDT.Load(StrafCom.ExecuteReader());
                MebelDT.Load(MebelCom.ExecuteReader());
                SpisanieDT.Load(SpisanieCom.ExecuteReader());
                ZamenaDT.Load(ZamenaCom.ExecuteReader());

                StolTbl.ItemsSource = StolDT.DefaultView;
                KlientTbl.ItemsSource = KlientDT.DefaultView;
                BronTbl.ItemsSource = BronDT.DefaultView;
                StrafTbl.ItemsSource = StrafDT.DefaultView;
                MebelTbl.ItemsSource = MebelDT.DefaultView;
                SpisanieTbl.ItemsSource = SpisanieDT.DefaultView;
                ZamenaTbl.ItemsSource = ZamenaDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //conZalAdmin.Close();
            }
        }

        private void StolTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StolTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)StolTbl.SelectedItem;
            Ststus_stol.Text = row["Status_stol"].ToString();
            Nomer_stol.Text = row["Nomer_stola"].ToString();
        }

        private void AddStol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddStolCom = new SqlCommand("AddStol", con); //создание выборки username и pass из таблицы tbl_log
                AddStolCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddStolCom.Parameters.AddWithValue("@Status_stol", Ststus_stol.Text);
                AddStolCom.Parameters.AddWithValue("@Nomer_stola", Nomer_stol.Text);
                //AddOtpCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKOtp.SelectedItem as IDComboBoxItem).ID);
                AddStolCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdStol_Click(object sender, RoutedEventArgs e)
        {
            if (StolTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)StolTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdStolCom = new SqlCommand("UpdateStol", con);
                UpdStolCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdStolCom.Parameters.AddWithValue("@ID_Stol", (int)Vidkvit["ID_Stol"]);
                UpdStolCom.Parameters.AddWithValue("@Status_stol", Ststus_stol.Text);
                UpdStolCom.Parameters.AddWithValue("@Nomer_stola", Nomer_stol.Text);
                UpdStolCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelStol_Click(object sender, RoutedEventArgs e)
        {
            if (StolTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)StolTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelVid_kvitanciiCom = new SqlCommand("DelStol", con);
                DelVid_kvitanciiCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelVid_kvitanciiCom.Parameters.AddWithValue("@ID_Stol", (int)Vidkvit["ID_Stol"]); ;
                DelVid_kvitanciiCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void KlientTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KlientTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)KlientTbl.SelectedItem;
            Fam_Kl.Text = row["FamKl"].ToString();
            Im_Kl.Text = row["ImKl"].ToString();
            Otch_Kl.Text = row["OtKl"].ToString();
            Date_vnes.Text = row["Date_Vn"].ToString();
        }

        private void AddKlient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddKlientCom = new SqlCommand("[dbo].[AddKlient]", con); //создание выборки username и pass из таблицы tbl_log
                AddKlientCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddKlientCom.Parameters.AddWithValue("@FamKl", Fam_Kl.Text);
                AddKlientCom.Parameters.AddWithValue("@ImKl", Im_Kl.Text);
                AddKlientCom.Parameters.AddWithValue("@OtKl", Otch_Kl.Text);
                AddKlientCom.Parameters.AddWithValue("@Date_Vn", Date_vnes.Text);
                //AddOtpCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKOtp.SelectedItem as IDComboBoxItem).ID);
                AddKlientCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdKlient_Click(object sender, RoutedEventArgs e)
        {
            if (KlientTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)KlientTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdKlientCom = new SqlCommand("UpdateKlient", con);
                UpdKlientCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdKlientCom.Parameters.AddWithValue("@ID_Kl", (int)Vidkvit["ID_Klient"]);
                UpdKlientCom.Parameters.AddWithValue("@FamKl", Fam_Kl.Text);
                UpdKlientCom.Parameters.AddWithValue("@ImKl", Im_Kl.Text);
                UpdKlientCom.Parameters.AddWithValue("@OtKl", Otch_Kl.Text);
                UpdKlientCom.Parameters.AddWithValue("@Date_Vn", Date_vnes.Text);
                UpdKlientCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelKlient_Click(object sender, RoutedEventArgs e)
        {
            if (KlientTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)KlientTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelClientCom = new SqlCommand("DelKlient", con);
                DelClientCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelClientCom.Parameters.AddWithValue("@ID_Kl", (int)Vidkvit["ID_Klient"]);
                DelClientCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void GetStolFK()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektStolcmd = new SqlCommand("SELECT ID_Stol, Nomer_stola FROM [dbo].[Stol]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable StolFK = new DataTable();
                StolFK.Load(SelektStolcmd.ExecuteReader());
                ID_StolFKBron.Items.Clear();
                foreach (DataRow Row in StolFK.Rows)
                {
                    ID_StolFKBron.Items.Add(new IDComboBoxItem((int)Row["ID_Stol"], Row["Nomer_stola"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void GetKlientFK()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektKlientFKZakcmd = new SqlCommand("SELECT [ID_Klient], [FamKl] FROM [dbo].[Klient]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable KlientFKZak = new DataTable();
                KlientFKZak.Load(SelektKlientFKZakcmd.ExecuteReader());
                ID_KlientFKBron.Items.Clear();
                foreach (DataRow Row in KlientFKZak.Rows)
                {
                    ID_KlientFKBron.Items.Add(new IDComboBoxItem((int)Row["ID_Klient"], Row["FamKl"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
        }

        private void GetSotrFKBron()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSotrFKUchcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKUch = new DataTable();
                SotrFKUch.Load(SelektSotrFKUchcmd.ExecuteReader());
                ID_SotrFKBron.Items.Clear();
                foreach (DataRow Row in SotrFKUch.Rows)
                {
                    ID_SotrFKBron.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void ID_StolFKBron_DropDownOpened(object sender, EventArgs e)
        {
            GetStolFK();
        }

        private void ID_KlientFKBron_DropDownOpened(object sender, EventArgs e)
        {
            GetKlientFK();
        }

        private void ID_SotrFKBron_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKBron();
        }

        private void BronTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BronTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)BronTbl.SelectedItem;
            Date_Bron.Text = row["DateBr"].ToString();

            GetStolFK();
            GetSotrFKBron();
            GetKlientFK();

            foreach (IDComboBoxItem item in ID_StolFKBron.Items)
            {
                if (item.ID == (int)row["ID_Stol"])
                {
                    ID_StolFKBron.SelectedIndex = ID_StolFKBron.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_KlientFKBron.Items)
            {
                if (item.ID == (int)row["ID_Klient"])
                {
                    ID_KlientFKBron.SelectedIndex = ID_KlientFKBron.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_SotrFKBron.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKBron.SelectedIndex = ID_SotrFKBron.Items.IndexOf(item);
                }
            }
        }

        private void AddBron_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddBronCom = new SqlCommand("[dbo].[AddBron]", con); //создание выборки username и pass из таблицы tbl_log
                AddBronCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddBronCom.Parameters.AddWithValue("@DateBr", Date_Bron.Text);
                AddBronCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKBron.SelectedItem as IDComboBoxItem).ID);
                AddBronCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKBron.SelectedItem as IDComboBoxItem).ID);
                AddBronCom.Parameters.AddWithValue("@ID_Stol", (int)(ID_StolFKBron.SelectedItem as IDComboBoxItem).ID);
                AddBronCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdBron_Click(object sender, RoutedEventArgs e)
        {
            if (BronTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)BronTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdBronCom = new SqlCommand("UpdateBron", con);
                UpdBronCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdBronCom.Parameters.AddWithValue("@ID_Bron", (int)Vidkvit["ID_Bron"]);
                UpdBronCom.Parameters.AddWithValue("@DateBr", Date_Bron.Text);
                UpdBronCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKBron.SelectedItem as IDComboBoxItem).ID);
                UpdBronCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKBron.SelectedItem as IDComboBoxItem).ID);
                UpdBronCom.Parameters.AddWithValue("@ID_Stol", (int)(ID_StolFKBron.SelectedItem as IDComboBoxItem).ID);
                UpdBronCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelBron_Click(object sender, RoutedEventArgs e)
        {
            if (BronTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)BronTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelBronCom = new SqlCommand("DelBron", con);
                DelBronCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelBronCom.Parameters.AddWithValue("@ID_Bron", (int)Vidkvit["ID_Bron"]);
                DelBronCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void GetMebelFKSht()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektStolcmd = new SqlCommand("SELECT ID_Mebel, Nomer_mebel FROM [dbo].[Mebel]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable StolFK = new DataTable();
                StolFK.Load(SelektStolcmd.ExecuteReader());
                ID_MebelFKStraf.Items.Clear();
                foreach (DataRow Row in StolFK.Rows)
                {
                    ID_MebelFKStraf.Items.Add(new IDComboBoxItem((int)Row["ID_Mebel"], Row["Nomer_mebel"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void GetKlientFKSht()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektKlientFKZakcmd = new SqlCommand("SELECT [ID_Klient], [FamKl] FROM [dbo].[Klient]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable KlientFKZak = new DataTable();
                KlientFKZak.Load(SelektKlientFKZakcmd.ExecuteReader());
                ID_KlientFKStraf.Items.Clear();
                foreach (DataRow Row in KlientFKZak.Rows)
                {
                    ID_KlientFKStraf.Items.Add(new IDComboBoxItem((int)Row["ID_Klient"], Row["FamKl"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
        }

        private void GetSotrFKSht()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSotrFKUchcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKUch = new DataTable();
                SotrFKUch.Load(SelektSotrFKUchcmd.ExecuteReader());
                ID_SotrFKStraf.Items.Clear();
                foreach (DataRow Row in SotrFKUch.Rows)
                {
                    ID_SotrFKStraf.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void ID_KlientFKStraf_DropDownOpened(object sender, EventArgs e)
        {
            GetKlientFKSht();
        }

        private void ID_SotrFKStraf_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKSht();
        }

        private void ID_MebelFKStraf_DropDownOpened(object sender, EventArgs e)
        {
            GetMebelFKSht();
        }

        private void StrafTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StrafTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)StrafTbl.SelectedItem;
            Summ_Shtraf.Text = row["Summ_str"].ToString();
            Usherb.Text = row["Usherb"].ToString();

            GetKlientFKSht();
            GetSotrFKSht();
            GetMebelFKSht();

            foreach (IDComboBoxItem item in ID_KlientFKStraf.Items)
            {
                if (item.ID == (int)row["ID_Klient"])
                {
                    ID_KlientFKStraf.SelectedIndex = ID_KlientFKStraf.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_SotrFKStraf.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKStraf.SelectedIndex = ID_SotrFKStraf.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_MebelFKStraf.Items)
            {
                if (item.ID == (int)row["ID_Mebel"])
                {
                    ID_MebelFKStraf.SelectedIndex = ID_MebelFKStraf.Items.IndexOf(item);
                }
            }
        }

        private void AddStraf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddStrafCom = new SqlCommand("[dbo].[AddStraf]", con); //создание выборки username и pass из таблицы tbl_log
                AddStrafCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddStrafCom.Parameters.AddWithValue("@Summ_str", Summ_Shtraf.Text);
                AddStrafCom.Parameters.AddWithValue("@Usherb", Usherb.Text);
                AddStrafCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKStraf.SelectedItem as IDComboBoxItem).ID);
                AddStrafCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKStraf.SelectedItem as IDComboBoxItem).ID);
                AddStrafCom.Parameters.AddWithValue("@ID_Mebel", (int)(ID_MebelFKStraf.SelectedItem as IDComboBoxItem).ID);
                AddStrafCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdStraf_Click(object sender, RoutedEventArgs e)
        {
            if (StrafTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)StrafTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdStrafCom = new SqlCommand("UpdateStraf", con);
                UpdStrafCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdStrafCom.Parameters.AddWithValue("@ID_Strf", (int)Vidkvit["ID_Straf"]);
                UpdStrafCom.Parameters.AddWithValue("@Summ_str", Summ_Shtraf.Text);
                UpdStrafCom.Parameters.AddWithValue("@Usherb", Usherb.Text);
                UpdStrafCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKStraf.SelectedItem as IDComboBoxItem).ID);
                UpdStrafCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKStraf.SelectedItem as IDComboBoxItem).ID);
                UpdStrafCom.Parameters.AddWithValue("@ID_Mebel", (int)(ID_MebelFKStraf.SelectedItem as IDComboBoxItem).ID);
                UpdStrafCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelStraf_Click(object sender, RoutedEventArgs e)
        {
            if (StrafTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)StrafTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelStrafCom = new SqlCommand("DelStraf", con);
                DelStrafCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelStrafCom.Parameters.AddWithValue("@ID_Str", (int)Vidkvit["ID_Straf"]);
                DelStrafCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void MebelTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MebelTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)MebelTbl.SelectedItem;
            Name_mebel.Text = row["Naim_mebel"].ToString();
            Nomer_mebel.Text = row["Nomer_mebel"].ToString();
            Sost_mebel.Text = row["Sost_mebel"].ToString();
        }

        private void AddMebel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddMebelCom = new SqlCommand("[dbo].[AddMebel]", con); //создание выборки username и pass из таблицы tbl_log
                AddMebelCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddMebelCom.Parameters.AddWithValue("@Naim_mebel", Name_mebel.Text);
                AddMebelCom.Parameters.AddWithValue("@Nomer_mebel", Nomer_mebel.Text);
                AddMebelCom.Parameters.AddWithValue("@Sost_mebel", Sost_mebel.Text);
                AddMebelCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdMebel_Click(object sender, RoutedEventArgs e)
        {
            if (MebelTbl.SelectedItem == null) return;
            DataRowView Mebel = (DataRowView)MebelTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdMebelCom = new SqlCommand("UpdateMebel", con);
                UpdMebelCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdMebelCom.Parameters.AddWithValue("@ID_Mebel", (int)Mebel["ID_Mebel"]);
                UpdMebelCom.Parameters.AddWithValue("@Naim_mebel", Name_mebel.Text);
                UpdMebelCom.Parameters.AddWithValue("@Nomer_mebel", Nomer_mebel.Text);
                UpdMebelCom.Parameters.AddWithValue("@Sost_mebel", Sost_mebel.Text);
                UpdMebelCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelMebel_Click(object sender, RoutedEventArgs e)
        {
            if (MebelTbl.SelectedItem == null) return;
            DataRowView Mebel = (DataRowView)MebelTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelMebelCom = new SqlCommand("DelMebel", con);
                DelMebelCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelMebelCom.Parameters.AddWithValue("@ID_Meb", (int)Mebel["ID_Mebel"]);
                DelMebelCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void GetMebelFKSpis()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektStolcmd = new SqlCommand("SELECT ID_Mebel, Nomer_mebel FROM [dbo].[Mebel]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable StolFK = new DataTable();
                StolFK.Load(SelektStolcmd.ExecuteReader());
                ID_MebelFKSpis.Items.Clear();
                foreach (DataRow Row in StolFK.Rows)
                {
                    ID_MebelFKSpis.Items.Add(new IDComboBoxItem((int)Row["ID_Mebel"], Row["Nomer_mebel"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void GetSotrFKSpis()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSotrFKUchcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKUch = new DataTable();
                SotrFKUch.Load(SelektSotrFKUchcmd.ExecuteReader());
                ID_SotrFKSpis.Items.Clear();
                foreach (DataRow Row in SotrFKUch.Rows)
                {
                    ID_SotrFKSpis.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void GetSotrFKZAm()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSotrFKUchcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKUch = new DataTable();
                SotrFKUch.Load(SelektSotrFKUchcmd.ExecuteReader());
                ID_SotrFKZamena.Items.Clear();
                foreach (DataRow Row in SotrFKUch.Rows)
                {
                    ID_SotrFKZamena.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void GetSpis()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSpiscmd = new SqlCommand("SELECT [ID_Spisanie], [Nomer_spis] FROM [dbo].[Spisanie]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SelektSpis = new DataTable();
                SelektSpis.Load(SelektSpiscmd.ExecuteReader());
                ID_SpisanieFKZamena.Items.Clear();
                foreach (DataRow Row in SelektSpis.Rows)
                {
                    ID_SpisanieFKZamena.Items.Add(new IDComboBoxItem((int)Row["ID_Spisanie"], Row["Nomer_spis"].ToString()));
                }

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
        }

        private void ID_SotrFKSpis_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKSpis();
        }

        private void ID_MebelFKSpis_DropDownOpened(object sender, EventArgs e)
        {
            GetMebelFKSpis();
        }

        private void SpisanieTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpisanieTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)SpisanieTbl.SelectedItem;
            Povregd.Text = row["Povregd"].ToString();
            Nomer_spis.Text = row["Nomer_spis"].ToString();

            GetSotrFKSpis();
            GetMebelFKSpis();

            foreach (IDComboBoxItem item in ID_SotrFKSpis.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKSpis.SelectedIndex = ID_SotrFKSpis.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_MebelFKSpis.Items)
            {
                if (item.ID == (int)row["ID_Mebel"])
                {
                    ID_MebelFKSpis.SelectedIndex = ID_MebelFKSpis.Items.IndexOf(item);
                }
            }
        }

        private void AddSpisanie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddSpisanieCom = new SqlCommand("[dbo].[AddSpisanie]", con); //создание выборки username и pass из таблицы tbl_log
                AddSpisanieCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddSpisanieCom.Parameters.AddWithValue("@Povregd", Povregd.Text);
                AddSpisanieCom.Parameters.AddWithValue("@Nomer_spis", Nomer_spis.Text);
                AddSpisanieCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKSpis.SelectedItem as IDComboBoxItem).ID);
                AddSpisanieCom.Parameters.AddWithValue("@ID_Mebel", (int)(ID_MebelFKSpis.SelectedItem as IDComboBoxItem).ID);
                AddSpisanieCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdSpisanie_Click(object sender, RoutedEventArgs e)
        {
            if (SpisanieTbl.SelectedItem == null) return;
            DataRowView Spisanie = (DataRowView)SpisanieTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdSpisanieCom = new SqlCommand("[dbo].[UpdateSpisanie]", con);
                UpdSpisanieCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdSpisanieCom.Parameters.AddWithValue("@ID_Spis", (int)Spisanie["ID_Spisanie"]);
                UpdSpisanieCom.Parameters.AddWithValue("@Povregd", Povregd.Text);
                UpdSpisanieCom.Parameters.AddWithValue("@Nomer_spis", Nomer_spis.Text);
                UpdSpisanieCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKSpis.SelectedItem as IDComboBoxItem).ID);
                UpdSpisanieCom.Parameters.AddWithValue("@ID_Mebel", (int)(ID_MebelFKSpis.SelectedItem as IDComboBoxItem).ID);
                UpdSpisanieCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelSpisanie_Click(object sender, RoutedEventArgs e)
        {
            if (SpisanieTbl.SelectedItem == null) return;
            DataRowView Spisanie = (DataRowView)SpisanieTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelSpisanieCom = new SqlCommand("[dbo].[DelSpisanie]", con);
                DelSpisanieCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelSpisanieCom.Parameters.AddWithValue("@ID_Spis", (int)Spisanie["ID_Spisanie"]);
                DelSpisanieCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void ID_SotrFKZamena_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKZAm();
        }

        private void ID_SpisanieFKZamena_DropDownOpened(object sender, EventArgs e)
        {
            GetSpis();
        }

        private void ZamenaTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ZamenaTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)ZamenaTbl.SelectedItem;
            Prichina.Text = row["Prichina"].ToString();
            Nomer_zamena.Text = row["Nomer_zamena"].ToString();

            GetSotrFKZAm();
            GetSpis();

            foreach (IDComboBoxItem item in ID_SotrFKZamena.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKZamena.SelectedIndex = ID_SotrFKZamena.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_SpisanieFKZamena.Items)
            {
                if (item.ID == (int)row["ID_Spisanie"])
                {
                    ID_SpisanieFKZamena.SelectedIndex = ID_SpisanieFKZamena.Items.IndexOf(item);
                }
            }
        }

        private void AddZamena_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddZamenaCom = new SqlCommand("[dbo].[AddZamena]", con); //создание выборки username и pass из таблицы tbl_log
                AddZamenaCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddZamenaCom.Parameters.AddWithValue("@Prichina", Prichina.Text);
                AddZamenaCom.Parameters.AddWithValue("@Nomer_zamena", Nomer_zamena.Text);
                AddZamenaCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZamena.SelectedItem as IDComboBoxItem).ID);
                AddZamenaCom.Parameters.AddWithValue("@ID_Spisanie", (int)(ID_SpisanieFKZamena.SelectedItem as IDComboBoxItem).ID);
                AddZamenaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisZalAdmin();
        }

        private void UpdZamena_Click(object sender, RoutedEventArgs e)
        {
            if (ZamenaTbl.SelectedItem == null) return;
            DataRowView Spisanie = (DataRowView)ZamenaTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdZamenaCom = new SqlCommand("[dbo].[UpdateZamena]", con);
                UpdZamenaCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdZamenaCom.Parameters.AddWithValue("@ID_Zamena", (int)Spisanie["ID_Zamena"]);
                UpdZamenaCom.Parameters.AddWithValue("@Prichina", Prichina.Text);
                UpdZamenaCom.Parameters.AddWithValue("@Nomer_zamena", Nomer_zamena.Text);
                UpdZamenaCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZamena.SelectedItem as IDComboBoxItem).ID);
                UpdZamenaCom.Parameters.AddWithValue("@ID_Spisanie", (int)(ID_SpisanieFKZamena.SelectedItem as IDComboBoxItem).ID);
                UpdZamenaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }

        private void DelZamena_Click(object sender, RoutedEventArgs e)
        {
            if (ZamenaTbl.SelectedItem == null) return;
            DataRowView Spisanie = (DataRowView)ZamenaTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelZamenaCom = new SqlCommand("[dbo].[DelZamena]", con);
                DelZamenaCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelZamenaCom.Parameters.AddWithValue("@ID_Zamena", (int)Spisanie["ID_Zamena"]);
                DelZamenaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisZalAdmin();
        }
    }
}
