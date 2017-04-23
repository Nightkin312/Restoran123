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
    /// Логика взаимодействия для BuhgForm.xaml
    /// </summary>
    public partial class BuhgForm : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        //SqlConnection conBuhgForm = new SqlConnection(@"server=JSAY; database=Restoran; Integrated Security = true;");

        public BuhgForm()
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

        private void otrisBuhg()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand Vid_kvitCom = new SqlCommand("SELECT * FROM  Vid_kvitancii", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand KvitanciyaCom = new SqlCommand("SELECT * FROM  Kvitanciya", con);
                SqlCommand UchViplPoGKHCom = new SqlCommand("SELECT * FROM  Uchot_vplat_po_GKH", con);
                SqlCommand ChekCom = new SqlCommand("SELECT * FROM  Chek", con);

                con.Open(); //открытие подключения

                DataTable Vid_kvitDT = new DataTable();
                DataTable KvitanciyaDT = new DataTable();
                DataTable UchViplPoGKHDT = new DataTable();
                DataTable ChekDT = new DataTable();

                Vid_kvitDT.Load(Vid_kvitCom.ExecuteReader());
                KvitanciyaDT.Load(KvitanciyaCom.ExecuteReader());
                UchViplPoGKHDT.Load(UchViplPoGKHCom.ExecuteReader());
                ChekDT.Load(ChekCom.ExecuteReader());

                Vid_kvitTbl.ItemsSource = Vid_kvitDT.DefaultView;
                KvitTbl.ItemsSource = KvitanciyaDT.DefaultView;
                UchViplPoGKHTbl.ItemsSource = UchViplPoGKHDT.DefaultView;
                ChekTbl.ItemsSource = ChekDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand Vid_kvitCom = new SqlCommand("SELECT * FROM  Vid_kvitancii", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand KvitanciyaCom = new SqlCommand("SELECT * FROM  Kvitanciya", con);
                SqlCommand UchViplPoGKHCom = new SqlCommand("SELECT * FROM  Uchot_vplat_po_GKH", con);
                SqlCommand ChekCom = new SqlCommand("SELECT * FROM  Chek", con);

                con.Open(); //открытие подключения

                DataTable Vid_kvitDT = new DataTable();
                DataTable KvitanciyaDT = new DataTable();
                DataTable UchViplPoGKHDT = new DataTable();
                DataTable ChekDT = new DataTable();

                Vid_kvitDT.Load(Vid_kvitCom.ExecuteReader());
                KvitanciyaDT.Load(KvitanciyaCom.ExecuteReader());
                UchViplPoGKHDT.Load(UchViplPoGKHCom.ExecuteReader());
                ChekDT.Load(ChekCom.ExecuteReader());

                Vid_kvitTbl.ItemsSource = Vid_kvitDT.DefaultView;
                KvitTbl.ItemsSource = KvitanciyaDT.DefaultView;
                UchViplPoGKHTbl.ItemsSource = UchViplPoGKHDT.DefaultView;
                ChekTbl.ItemsSource = ChekDT.DefaultView;

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
            }
        }

        private void Vid_kvitTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Vid_kvitTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)Vid_kvitTbl.SelectedItem;
            Naim_vid_kvit.Text = row["Naiminovanie_vida"].ToString();
        }

        private void AddVid_kvit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddVid_kvitCom = new SqlCommand("AddVid_kvitancii", con); //создание выборки username и pass из таблицы tbl_log
                AddVid_kvitCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddVid_kvitCom.Parameters.AddWithValue("@Naim_vid", Naim_vid_kvit.Text);
                //AddOtpCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKOtp.SelectedItem as IDComboBoxItem).ID);
                AddVid_kvitCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisBuhg();
        }

        private void UpdVid_kvitm_Click(object sender, RoutedEventArgs e)
        {
            if (Vid_kvitTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)Vid_kvitTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdVid_kvitCom = new SqlCommand("UpdateVid_kvit", con);
                UpdVid_kvitCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdVid_kvitCom.Parameters.AddWithValue("@ID_Naim", (int)Vidkvit["ID_Vid_kvitancii"]);
                UpdVid_kvitCom.Parameters.AddWithValue("@Naim_vid", Naim_vid_kvit.Text);
                UpdVid_kvitCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisBuhg();
        }

        private void DelVid_kvit_Click(object sender, RoutedEventArgs e)
        {
            if (Vid_kvitTbl.SelectedItem == null) return;
            DataRowView Vidkvit = (DataRowView)Vid_kvitTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelVid_kvitanciiCom = new SqlCommand("DelVid_kvitancii", con);
                DelVid_kvitanciiCom.CommandType = CommandType.StoredProcedure;;
                con.Open(); //открытие подключения
                DelVid_kvitanciiCom.Parameters.AddWithValue("@ID_Naim", (int)Vidkvit["ID_Vid_kvitancii"]);
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
            otrisBuhg();
        }

        private void GetVid_kvitFK()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektID_Vid_kvitFKcmd = new SqlCommand("SELECT [ID_Vid_kvitancii], [Naiminovanie_vida] FROM [dbo].[Vid_kvitancii]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Vid_kvitFK = new DataTable();
                Vid_kvitFK.Load(SelektID_Vid_kvitFKcmd.ExecuteReader());
                ID_Vid_kvitFK.Items.Clear();
                foreach (DataRow Row in Vid_kvitFK.Rows)
                {
                    ID_Vid_kvitFK.Items.Add(new IDComboBoxItem((int)Row["ID_Vid_kvitancii"], Row["Naiminovanie_vida"].ToString()));
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

        private void ID_Vid_kvitFK_DropDownOpened(object sender, EventArgs e)
        {
            GetVid_kvitFK();
        }

        private void KvitTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KvitTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)KvitTbl.SelectedItem;
            Raschetny_period.Text = row["Raschetny_period"].ToString();
            Summa_k_oplate.Text = row["Summa_k_oplate"].ToString();
            Nomer_scheta_kvit.Text = row["Nomer_scheta"].ToString();

            GetVid_kvitFK();

            foreach (IDComboBoxItem item in ID_Vid_kvitFK.Items)
            {
                if (item.ID == (int)row["ID_Vid_kvitancii"])
                {
                    ID_Vid_kvitFK.SelectedIndex = ID_Vid_kvitFK.Items.IndexOf(item);
                }
            }
        }

        private void AddKvit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddKvitanciyaCom = new SqlCommand("AddKvitanciya", con); //создание выборки username и pass из таблицы tbl_log
                AddKvitanciyaCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddKvitanciyaCom.Parameters.AddWithValue("@Raschetny_period", Raschetny_period.Text);
                AddKvitanciyaCom.Parameters.AddWithValue("@Summa_k_oplate", Summa_k_oplate.Text);
                AddKvitanciyaCom.Parameters.AddWithValue("@Nomer_scheta", Nomer_scheta_kvit.Text);
                AddKvitanciyaCom.Parameters.AddWithValue("@ID_Vid_kvitancii", (int)(ID_Vid_kvitFK.SelectedItem as IDComboBoxItem).ID);
                AddKvitanciyaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisBuhg();
        }

        private void UpdKvit_Click(object sender, RoutedEventArgs e)
        {
            if (KvitTbl.SelectedItem == null) return;
            DataRowView Kvit = (DataRowView)KvitTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdKvitanciyaCom = new SqlCommand("UpdateKvitanciya", con);
                UpdKvitanciyaCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdKvitanciyaCom.Parameters.AddWithValue("@ID_Kvit", (int)Kvit["ID_Kvitanciya"]);
                UpdKvitanciyaCom.Parameters.AddWithValue("@Raschetny_period", Raschetny_period.Text);
                UpdKvitanciyaCom.Parameters.AddWithValue("@Summa_k_oplate", Summa_k_oplate.Text);
                UpdKvitanciyaCom.Parameters.AddWithValue("@Nomer_scheta", Nomer_scheta_kvit.Text);
                UpdKvitanciyaCom.Parameters.AddWithValue("@ID_Vid_kvitancii", (int)(ID_Vid_kvitFK.SelectedItem as IDComboBoxItem).ID);
                UpdKvitanciyaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisBuhg();
        }

        private void DelKvit_Click(object sender, RoutedEventArgs e)
        {
            if (KvitTbl.SelectedItem == null) return;
            DataRowView Kvit = (DataRowView)KvitTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelKvitanciyaCom = new SqlCommand("DelKvitanciya", con);
                DelKvitanciyaCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelKvitanciyaCom.Parameters.AddWithValue("@ID_Kvit", (int)Kvit["ID_Kvitanciya"]);
                DelKvitanciyaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisBuhg();
        }

        private void GetKvitFK()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektKvitFKcmd = new SqlCommand("SELECT [ID_Kvitanciya], [Nomer_scheta] FROM [dbo].[Kvitanciya]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable KvitFK = new DataTable();
                KvitFK.Load(SelektKvitFKcmd.ExecuteReader());
                ID_KvitFK.Items.Clear();
                foreach (DataRow Row in KvitFK.Rows)
                {
                    ID_KvitFK.Items.Add(new IDComboBoxItem((int)Row["ID_Kvitanciya"], Row["Nomer_scheta"].ToString()));
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

        private void GetSotrFKUch()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand SelektSotrFKUchcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKUch = new DataTable();
                SotrFKUch.Load(SelektSotrFKUchcmd.ExecuteReader());
                ID_SotrFKUch.Items.Clear();
                foreach (DataRow Row in SotrFKUch.Rows)
                {
                    ID_SotrFKUch.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
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

        private void ID_KvitFK_DropDownOpened(object sender, EventArgs e)
        {
            GetKvitFK();
        }

        private void ID_SotrFKUch_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKUch();
        }

        private void UchViplPoGKHTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UchViplPoGKHTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)UchViplPoGKHTbl.SelectedItem;
            Date_oplata.Text = row["Date_oplata"].ToString();

            GetKvitFK();
            GetSotrFKUch();

            foreach (IDComboBoxItem item in ID_KvitFK.Items)
            {
                if (item.ID == (int)row["ID_Kvitanciya"])
                {
                    ID_KvitFK.SelectedIndex = ID_KvitFK.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_SotrFKUch.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKUch.SelectedIndex = ID_SotrFKUch.Items.IndexOf(item);
                }
            }
        }

        private void AddUchViplPoGKH_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand AddUchViplPoGKHCom = new SqlCommand("[dbo].[AddUchot_vplat_po_GKH]", con); //создание выборки username и pass из таблицы tbl_log
                AddUchViplPoGKHCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddUchViplPoGKHCom.Parameters.AddWithValue("@Date_oplata", Date_oplata.Text);
                AddUchViplPoGKHCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKUch.SelectedItem as IDComboBoxItem).ID);
                AddUchViplPoGKHCom.Parameters.AddWithValue("@ID_Kvitanciya", (int)(ID_KvitFK.SelectedItem as IDComboBoxItem).ID);
                AddUchViplPoGKHCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisBuhg();
        }

        private void UpdUchViplPoGKH_Click(object sender, RoutedEventArgs e)
        {
            if (UchViplPoGKHTbl.SelectedItem == null) return;
            DataRowView UchViplPoGKH = (DataRowView)UchViplPoGKHTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdUchViplPoGKHCom = new SqlCommand("[dbo].[UpdateUchot_vplat_po_GKH]", con);
                UpdUchViplPoGKHCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdUchViplPoGKHCom.Parameters.AddWithValue("@ID_Uch", (int)UchViplPoGKH["ID_UCHOT_vplat_po_GKH"]);
                UpdUchViplPoGKHCom.Parameters.AddWithValue("@Date_oplata", Date_oplata.Text);
                UpdUchViplPoGKHCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKUch.SelectedItem as IDComboBoxItem).ID);
                UpdUchViplPoGKHCom.Parameters.AddWithValue("@ID_Kvitanciya", (int)(ID_KvitFK.SelectedItem as IDComboBoxItem).ID);
                UpdUchViplPoGKHCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisBuhg();
        }

        private void DelUchViplPoGKH_Click(object sender, RoutedEventArgs e)
        {
            if (UchViplPoGKHTbl.SelectedItem == null) return;
            DataRowView UchViplPoGKH = (DataRowView)UchViplPoGKHTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                SqlCommand DelUchViplPoGKHCom = new SqlCommand("[dbo].[DelUchot_vplat_po_GKH]", con);
                DelUchViplPoGKHCom.CommandType = CommandType.StoredProcedure; ;
                con.Open(); //открытие подключения
                DelUchViplPoGKHCom.Parameters.AddWithValue("@ID_Uch", (int)UchViplPoGKH["ID_UCHOT_vplat_po_GKH"]);
                DelUchViplPoGKHCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                // con.Close();
            }
            otrisBuhg();
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
            otrisBuhg();
        }

        private void UpdChek_Click(object sender, RoutedEventArgs e)
        {
            if (ID_ZakazKlFK.SelectedItem == null) return;
            if (ID_ShtrafFKChek.SelectedItem == null) return;
            if (ChekTbl.SelectedItem == null) return;
            DataRowView Chek = (DataRowView)ChekTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdChekCom = new SqlCommand("UpdateChek", con);
                UpdChekCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdChekCom.Parameters.AddWithValue("@ID_Chek", (int)Chek["ID_Chek"]);
                UpdChekCom.Parameters.AddWithValue("@Nomer_chek", Nomer_chek.Text);
                UpdChekCom.Parameters.AddWithValue("@Time_chek", TIme_chek.Text);
                UpdChekCom.Parameters.AddWithValue("@Summ_chek", Summ_chek.Text);
                UpdChekCom.Parameters.AddWithValue("@ID_Zack", (int)(ID_ZakazKlFK.SelectedItem as IDComboBoxItem).ID);
                UpdChekCom.Parameters.AddWithValue("@ID_Straf", (int)(ID_ShtrafFKChek.SelectedItem as IDComboBoxItem).ID);
                UpdChekCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisBuhg();
        }

        private void DelChek_Click(object sender, RoutedEventArgs e)
        {
            if (ChekTbl.SelectedItem == null) return;
            DataRowView Chek = (DataRowView)ChekTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelChekCom = new SqlCommand("DelChek", con);
                DelChekCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelChekCom.Parameters.AddWithValue("@ID_Chek", (int)Chek["ID_Chek"]);
                DelChekCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //  con.Close();
            }
            otrisBuhg();
        }
    }
}
