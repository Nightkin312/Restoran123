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
    /// Логика взаимодействия для OficForm.xaml
    /// </summary>
    public partial class OficForm : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        //SqlConnection conOfic = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;");

        public OficForm()
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

        private void otrisOfic()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection conOfic = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand MenuCom = new SqlCommand("SELECT * FROM  Menu", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand ZakazKlCom = new SqlCommand("SELECT * FROM  Zakaz", con);
                SqlCommand Sost_zakaz_klCom = new SqlCommand("SELECT * FROM  Sost_zakaz", con);
                SqlCommand ChekCom = new SqlCommand("SELECT * FROM  Chek", con);
                SqlCommand StolCom = new SqlCommand("SELECT * FROM  Stol", con);
                SqlCommand KlientCom = new SqlCommand("SELECT * FROM  Klient", con);
                SqlCommand BronCom = new SqlCommand("SELECT * FROM  Bron", con);
                SqlCommand StrafCom = new SqlCommand("SELECT * FROM  Straf", con);

                con.Open(); //открытие подключения

                DataTable MenuDT = new DataTable();
                DataTable ZakazKlDT = new DataTable();
                DataTable Sost_zakaz_klDT = new DataTable();
                DataTable ChekDT = new DataTable();
                DataTable StolDT = new DataTable();
                DataTable KlientDT = new DataTable();
                DataTable BronDT = new DataTable();
                DataTable StrafDT = new DataTable();

                MenuDT.Load(MenuCom.ExecuteReader());
                ZakazKlDT.Load(ZakazKlCom.ExecuteReader());
                Sost_zakaz_klDT.Load(Sost_zakaz_klCom.ExecuteReader());
                ChekDT.Load(ChekCom.ExecuteReader());
                StolDT.Load(StolCom.ExecuteReader());
                KlientDT.Load(KlientCom.ExecuteReader());
                BronDT.Load(BronCom.ExecuteReader());
                StrafDT.Load(StrafCom.ExecuteReader());

                MenuTbl.ItemsSource = MenuDT.DefaultView;
                ZakazKlTbl.ItemsSource = ZakazKlDT.DefaultView;
                Sost_zakaz_klTbl.ItemsSource = Sost_zakaz_klDT.DefaultView;
                ChekTbl.ItemsSource = ChekDT.DefaultView;
                StolTbl.ItemsSource = StolDT.DefaultView;
                KlientTbl.ItemsSource = KlientDT.DefaultView;
                BronTbl.ItemsSource = BronDT.DefaultView;
                StrafTbl.ItemsSource = StrafDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //conOfic.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection conOfic = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand MenuCom = new SqlCommand("SELECT * FROM  Menu", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand ZakazKlCom = new SqlCommand("SELECT * FROM  Zakaz", con);
                SqlCommand Sost_zakaz_klCom = new SqlCommand("SELECT * FROM  Sost_zakaz", con);
                SqlCommand ChekCom = new SqlCommand("SELECT * FROM  Chek", con);
                SqlCommand StolCom = new SqlCommand("SELECT * FROM  Stol", con);
                SqlCommand KlientCom = new SqlCommand("SELECT * FROM  Klient", con);
                SqlCommand BronCom = new SqlCommand("SELECT * FROM  Bron", con);
                SqlCommand StrafCom = new SqlCommand("SELECT * FROM  Straf", con);

                con.Open(); //открытие подключения

                DataTable MenuDT = new DataTable();
                DataTable ZakazKlDT = new DataTable();
                DataTable Sost_zakaz_klDT = new DataTable();
                DataTable ChekDT = new DataTable();
                DataTable StolDT = new DataTable();
                DataTable KlientDT = new DataTable();
                DataTable BronDT = new DataTable();
                DataTable StrafDT = new DataTable();

                MenuDT.Load(MenuCom.ExecuteReader());
                ZakazKlDT.Load(ZakazKlCom.ExecuteReader());
                Sost_zakaz_klDT.Load(Sost_zakaz_klCom.ExecuteReader());
                ChekDT.Load(ChekCom.ExecuteReader());
                StolDT.Load(StolCom.ExecuteReader());
                KlientDT.Load(KlientCom.ExecuteReader());
                BronDT.Load(BronCom.ExecuteReader());
                StrafDT.Load(StrafCom.ExecuteReader());

                MenuTbl.ItemsSource = MenuDT.DefaultView;
                ZakazKlTbl.ItemsSource = ZakazKlDT.DefaultView;
                Sost_zakaz_klTbl.ItemsSource = Sost_zakaz_klDT.DefaultView;
                ChekTbl.ItemsSource = ChekDT.DefaultView;
                StolTbl.ItemsSource = StolDT.DefaultView;
                KlientTbl.ItemsSource = KlientDT.DefaultView;
                BronTbl.ItemsSource = BronDT.DefaultView;
                StrafTbl.ItemsSource = StrafDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //conOfic.Close();
            }
        }

        private void MenuTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)MenuTbl.SelectedItem;
            Name_bludo.Text = row["Name_bludo"].ToString();
            Opis_bludo.Text = row["Opis_bludo"].ToString();
            Cena_bludo.Text = row["Cena"].ToString();
        }

        private void GetSotrFKZak()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektSotrFKZAkcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKZak = new DataTable();
                SotrFKZak.Load(SelektSotrFKZAkcmd.ExecuteReader());
                ID_SotrFKZak.Items.Clear();
                foreach (DataRow Row in SotrFKZak.Rows)
                {
                    ID_SotrFKZak.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
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

        private void GetStolFKZak()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektStolFKZakcmd = new SqlCommand("SELECT [ID_Stol], [Nomer_stola] FROM [dbo].[Stol]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable StolFKZak = new DataTable();
                StolFKZak.Load(SelektStolFKZakcmd.ExecuteReader());
                ID_StolFKZak.Items.Clear();
                foreach (DataRow Row in StolFKZak.Rows)
                {
                    ID_StolFKZak.Items.Add(new IDComboBoxItem((int)Row["ID_Stol"], Row["Nomer_stola"].ToString()));
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

        private void GetKlientFKZak()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektKlientFKZakcmd = new SqlCommand("SELECT [ID_Klient], [FamKl] FROM [dbo].[Klient]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable KlientFKZak = new DataTable();
                KlientFKZak.Load(SelektKlientFKZakcmd.ExecuteReader());
                ID_KlientFKZak.Items.Clear();
                foreach (DataRow Row in KlientFKZak.Rows)
                {
                    ID_KlientFKZak.Items.Add(new IDComboBoxItem((int)Row["ID_Klient"], Row["FamKl"].ToString()));
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

        private void ID_KlientFKZak_DropDownOpened(object sender, EventArgs e)
        {
            GetKlientFKZak();
        }

        private void ID_StolFKZak_DropDownOpened(object sender, EventArgs e)
        {
            GetStolFKZak();
        }

        private void ID_SotrFKZak_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKZak();
        }

        private void ZakazKlTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ZakazKlTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)ZakazKlTbl.SelectedItem;
            Summ_zak.Text = row["Summ_zack"].ToString();
            Nom_zak.Text = row["Nom_zack"].ToString();
            Time_vid.Text = row["Time_vid"].ToString();
            Time_prin.Text = row["Time_prin"].ToString();

            GetSotrFKZak();
            GetStolFKZak();
            GetKlientFKZak();

            foreach (IDComboBoxItem item in ID_SotrFKZak.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKZak.SelectedIndex = ID_SotrFKZak.Items.IndexOf(item);
                }
            }
            foreach (IDComboBoxItem item in ID_StolFKZak.Items)
            {
                if (item.ID == (int)row["ID_Stol"])
                {
                    ID_StolFKZak.SelectedIndex = ID_StolFKZak.Items.IndexOf(item);
                }
            }
            foreach (IDComboBoxItem item in ID_KlientFKZak.Items)
            {
                if (item.ID == (int)row["ID_Klient"])
                {
                    ID_KlientFKZak.SelectedIndex = ID_KlientFKZak.Items.IndexOf(item);
                }
            }
        }

        private void AddZakazKl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddZakazKlCom = new SqlCommand("AddZakaz", con); //создание выборки username и pass из таблицы tbl_log
                AddZakazKlCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddZakazKlCom.Parameters.AddWithValue("@Summ_zack", Summ_zak.Text);
                AddZakazKlCom.Parameters.AddWithValue("@Nom_zack", Nom_zak.Text);
                AddZakazKlCom.Parameters.AddWithValue("@Time_vid", Time_vid.Text);
                AddZakazKlCom.Parameters.AddWithValue("@Time_prin", Time_prin.Text);
                AddZakazKlCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZak.SelectedItem as IDComboBoxItem).ID);
                AddZakazKlCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKZak.SelectedItem as IDComboBoxItem).ID);
                AddZakazKlCom.Parameters.AddWithValue("@ID_Stol", (int)(ID_StolFKZak.SelectedItem as IDComboBoxItem).ID);
                AddZakazKlCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void UpdZakazKl_Click(object sender, RoutedEventArgs e)
        {
            if (ID_SotrFKZak.SelectedItem == null) return;
            if (ID_KlientFKZak.SelectedItem == null) return;
            if (ID_StolFKZak.SelectedItem == null) return;
            if (ZakazKlTbl.SelectedItem == null) return;
            DataRowView ZakazKl = (DataRowView)ZakazKlTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdZakazCom = new SqlCommand("UpdateZakaz", con);
                UpdZakazCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdZakazCom.Parameters.AddWithValue("@ID_Zak", (int)ZakazKl["ID_Zack"]);
                UpdZakazCom.Parameters.AddWithValue("@Summ_zack", Summ_zak.Text);
                UpdZakazCom.Parameters.AddWithValue("@Nom_zack", Nom_zak.Text);
                UpdZakazCom.Parameters.AddWithValue("@Time_vid", Time_vid.Text);
                UpdZakazCom.Parameters.AddWithValue("@Time_prin", Time_prin.Text);
                UpdZakazCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZak.SelectedItem as IDComboBoxItem).ID);
                UpdZakazCom.Parameters.AddWithValue("@ID_Klient", (int)(ID_KlientFKZak.SelectedItem as IDComboBoxItem).ID);
                UpdZakazCom.Parameters.AddWithValue("@ID_Stol", (int)(ID_StolFKZak.SelectedItem as IDComboBoxItem).ID);
                UpdZakazCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void DelZakazKl_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazKlTbl.SelectedItem == null) return;
            DataRowView ZakazKl = (DataRowView)ZakazKlTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelZakazCom = new SqlCommand("DelZakaz", con);
                DelZakazCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelZakazCom.Parameters.AddWithValue("@ID_Zakaz", (int)ZakazKl["ID_Zack"]);
                DelZakazCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void GetZakKlFKBludo()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektZakKlFKBludocmd = new SqlCommand("SELECT [ID_Zack], [Nom_zack] FROM [dbo].[Zakaz]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable ZakKlFKBludo = new DataTable();
                ZakKlFKBludo.Load(SelektZakKlFKBludocmd.ExecuteReader());
                ID_ZakKlFKBludo.Items.Clear();
                foreach (DataRow Row in ZakKlFKBludo.Rows)
                {
                    ID_ZakKlFKBludo.Items.Add(new IDComboBoxItem((int)Row["ID_Zack"], Row["Nom_zack"].ToString()));
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

        private void GetBludoFKSostZak()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektBludoFKSostZakcmd = new SqlCommand("SELECT [ID_Bludo], [Name_bludo] FROM [dbo].[Menu]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable BludoFKSostZak = new DataTable();
                BludoFKSostZak.Load(SelektBludoFKSostZakcmd.ExecuteReader());
                ID_BludoFKSostZak.Items.Clear();
                foreach (DataRow Row in BludoFKSostZak.Rows)
                {
                    ID_BludoFKSostZak.Items.Add(new IDComboBoxItem((int)Row["ID_Bludo"], Row["Name_bludo"].ToString()));
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

        private void ID_BludoFKSostZak_DropDownOpened(object sender, EventArgs e)
        {
            GetBludoFKSostZak();
        }

        private void ID_ZakKlFKBludo_DropDownOpened(object sender, EventArgs e)
        {
            GetZakKlFKBludo();
        }

        private void Sost_zakaz_klTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sost_zakaz_klTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)Sost_zakaz_klTbl.SelectedItem;
            Kolvo_blud.Text = row["Kolvo"].ToString();

            GetBludoFKSostZak();
            GetZakKlFKBludo();

            foreach (IDComboBoxItem item in ID_BludoFKSostZak.Items)
            {
                if (item.ID == (int)row["ID_Bludo"])
                {
                    ID_BludoFKSostZak.SelectedIndex = ID_BludoFKSostZak.Items.IndexOf(item);
                }
            }
            foreach (IDComboBoxItem item in ID_ZakKlFKBludo.Items)
            {
                if (item.ID == (int)row["ID_Zack"])
                {
                    ID_ZakKlFKBludo.SelectedIndex = ID_ZakKlFKBludo.Items.IndexOf(item);
                }
            }
        }

        private void AddSost_zakaz_kl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddSost_zakaz_klCom = new SqlCommand("AddSost_zakaz", con); //создание выборки username и pass из таблицы tbl_log
                AddSost_zakaz_klCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddSost_zakaz_klCom.Parameters.AddWithValue("@Kolvo", Kolvo_blud.Text);
                AddSost_zakaz_klCom.Parameters.AddWithValue("@ID_Bludo", (int)(ID_BludoFKSostZak.SelectedItem as IDComboBoxItem).ID);
                AddSost_zakaz_klCom.Parameters.AddWithValue("@ID_Zack", (int)(ID_ZakKlFKBludo.SelectedItem as IDComboBoxItem).ID);
                AddSost_zakaz_klCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void UpdSost_zakaz_kl_Click(object sender, RoutedEventArgs e)
        {
            if (ID_BludoFKSostZak.SelectedItem == null) return;
            if (ID_ZakKlFKBludo.SelectedItem == null) return;
            if (Sost_zakaz_klTbl.SelectedItem == null) return;
            DataRowView Sost_zakaz_kl = (DataRowView)Sost_zakaz_klTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdSost_zakaz_klCom = new SqlCommand("UpdateSost_zakaz", con);
                UpdSost_zakaz_klCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdSost_zakaz_klCom.Parameters.AddWithValue("@ID_SostZak", (int)Sost_zakaz_kl["ID_Sost_zakaz"]);
                UpdSost_zakaz_klCom.Parameters.AddWithValue("@Kolvo", Kolvo_blud.Text);
                UpdSost_zakaz_klCom.Parameters.AddWithValue("@ID_Bludo", (int)(ID_BludoFKSostZak.SelectedItem as IDComboBoxItem).ID);
                UpdSost_zakaz_klCom.Parameters.AddWithValue("@ID_Zack", (int)(ID_ZakKlFKBludo.SelectedItem as IDComboBoxItem).ID);
                UpdSost_zakaz_klCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic ();
        }

        private void DelSost_zakaz_kl_Click(object sender, RoutedEventArgs e)
        {
            if (Sost_zakaz_klTbl.SelectedItem == null) return;
            DataRowView Sost_zakaz_kl = (DataRowView)Sost_zakaz_klTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelSost_zakaz_klCom = new SqlCommand("DelSost_zakaz", con);
                DelSost_zakaz_klCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelSost_zakaz_klCom.Parameters.AddWithValue("@ID_SostZak", (int)Sost_zakaz_kl["ID_Sost_zakaz"]);
                DelSost_zakaz_klCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void GetZakazKlFK()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektZakazKlFKcmd = new SqlCommand("SELECT [ID_Zack], [Nom_zack] FROM [dbo].[Zakaz]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable ZakazKlFK = new DataTable();
                ZakazKlFK.Load(SelektZakazKlFKcmd.ExecuteReader());
                ID_ZakazKlFK.Items.Clear();
                foreach (DataRow Row in ZakazKlFK.Rows)
                {
                    ID_ZakazKlFK.Items.Add(new IDComboBoxItem((int)Row["ID_Zack"], Row["Nom_zack"].ToString()));
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

        private void GetShtrafFKChek()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektShtrafFKChekcmd = new SqlCommand("SELECT [ID_Straf], [Summ_str] FROM [dbo].[Straf]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable ShtrafFKChek = new DataTable();
                ShtrafFKChek.Load(SelektShtrafFKChekcmd.ExecuteReader());
                ID_ShtrafFKChek.Items.Clear();
                foreach (DataRow Row in ShtrafFKChek.Rows)
                {
                    ID_ShtrafFKChek.Items.Add(new IDComboBoxItem((int)Row["ID_Straf"], Row["Summ_str"].ToString()));
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

        private void ID_ZakazKlFK_DropDownOpened(object sender, EventArgs e)
        {
            GetZakazKlFK();
        }

        private void ID_ShtrafFKChek_DropDownOpened(object sender, EventArgs e)
        {
            GetShtrafFKChek();
        }

        private void ChekTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChekTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)ChekTbl.SelectedItem;
            Nomer_chek.Text = row["Nomer_chek"].ToString();
            TIme_chek.Text = row["Time_chek"].ToString();
            Summ_chek.Text = row["Summ_chek"].ToString();

            GetZakazKlFK();
            GetShtrafFKChek();

            foreach (IDComboBoxItem item in ID_ZakazKlFK.Items)
            {
                if (item.ID == (int)row["ID_Zack"])
                {
                    ID_ZakazKlFK.SelectedIndex = ID_ZakazKlFK.Items.IndexOf(item);
                }
            }
            foreach (IDComboBoxItem item in ID_ShtrafFKChek.Items)
            {
                if (item.ID == (int)row["ID_Straf"])
                {
                    ID_ShtrafFKChek.SelectedIndex = ID_ShtrafFKChek.Items.IndexOf(item);
                }
            }
        }

        private void AddChek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddChek_klCom = new SqlCommand("AddChek", con); //создание выборки username и pass из таблицы tbl_log
                AddChek_klCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddChek_klCom.Parameters.AddWithValue("@Nomer_chek", Nomer_chek.Text);
                AddChek_klCom.Parameters.AddWithValue("@Time_chek", TIme_chek.Text);
                AddChek_klCom.Parameters.AddWithValue("@Summ_chek", Summ_chek.Text);
                AddChek_klCom.Parameters.AddWithValue("@ID_Zack", (int)(ID_ZakazKlFK.SelectedItem as IDComboBoxItem).ID);
                AddChek_klCom.Parameters.AddWithValue("@ID_Straf", (int)(ID_ShtrafFKChek.SelectedItem as IDComboBoxItem).ID);
                AddChek_klCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisOfic();
        }

        private void StolTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StolTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)StolTbl.SelectedItem;
            Ststus_stol.Text = row["Status_stol"].ToString();
            Nomer_stol.Text = row["Nomer_stola"].ToString();
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
            otrisOfic();
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
            otrisOfic();
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

        private void ID_KlientFKBron_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            otrisOfic();
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
            otrisOfic();
        }
    }
}
