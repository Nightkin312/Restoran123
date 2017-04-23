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
    /// Логика взаимодействия для KadrRabform.xaml
    /// </summary>
    public partial class KadrRabform : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

       // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");

        public KadrRabform()
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

        private void otrisKdarRab()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DolgnostCom = new SqlCommand("SELECT * FROM  Dolgnost", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand TreningCom = new SqlCommand("SELECT * FROM  Trening", con);
                SqlCommand RezumCom = new SqlCommand("SELECT * FROM  Rezum", con);
                SqlCommand Shtatnaya_shemaCom = new SqlCommand("SELECT * FROM  Shtatnaya_shema", con);
                SqlCommand Prin_na_rabCom = new SqlCommand("SELECT * FROM  Prin_na_rab", con);
                SqlCommand SotrCom = new SqlCommand("SELECT * FROM  Sotr", con);
                SqlCommand Kontakt_sotrCom = new SqlCommand("SELECT * FROM  Kontakt_sotr", con);
                SqlCommand BolnichnyCom = new SqlCommand("SELECT * FROM  Bolnichny", con);
                SqlCommand OtpuskCom = new SqlCommand("SELECT * FROM  Otpusk", con);

                con.Open(); //открытие подключения

                DataTable DolgnostDT = new DataTable();
                DataTable TreningDT = new DataTable();
                DataTable RezumDT = new DataTable();
                DataTable Shtatnaya_shemaDT = new DataTable();
                DataTable Prin_na_rabDT = new DataTable();
                DataTable SotrDT = new DataTable();
                DataTable Kontakt_sotrDT = new DataTable();
                DataTable BolnicnyDT = new DataTable();
                DataTable OtpuskDT = new DataTable();

                DolgnostDT.Load(DolgnostCom.ExecuteReader());
                TreningDT.Load(TreningCom.ExecuteReader());
                RezumDT.Load(RezumCom.ExecuteReader());
                Shtatnaya_shemaDT.Load(Shtatnaya_shemaCom.ExecuteReader());
                Prin_na_rabDT.Load(Prin_na_rabCom.ExecuteReader());
                SotrDT.Load(SotrCom.ExecuteReader());
                Kontakt_sotrDT.Load(Kontakt_sotrCom.ExecuteReader());
                BolnicnyDT.Load(BolnichnyCom.ExecuteReader());
                OtpuskDT.Load(OtpuskCom.ExecuteReader());

                DolgnostTbl.ItemsSource = DolgnostDT.DefaultView;
                TreningTbl.ItemsSource = TreningDT.DefaultView;
                RezumTbl.ItemsSource = RezumDT.DefaultView;
                Shtatnaya_shemaTbl.ItemsSource = Shtatnaya_shemaDT.DefaultView;
                Prin_na_rabTbl.ItemsSource = Prin_na_rabDT.DefaultView;
                SotrTbl.ItemsSource = SotrDT.DefaultView;
                Kontakt_sotrTbl.ItemsSource = Kontakt_sotrDT.DefaultView;
                BolnichnyTbl.ItemsSource = BolnicnyDT.DefaultView;
                OtpuskTbl.ItemsSource = OtpuskDT.DefaultView;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DolgnostCom = new SqlCommand("SELECT * FROM  Dolgnost", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand TreningCom = new SqlCommand("SELECT * FROM  Trening", con);
                SqlCommand RezumCom = new SqlCommand("SELECT * FROM  Rezum", con);
                SqlCommand Shtatnaya_shemaCom = new SqlCommand("SELECT * FROM  Shtatnaya_shema", con);
                SqlCommand Prin_na_rabCom = new SqlCommand("SELECT * FROM  Prin_na_rab", con);
                SqlCommand SotrCom = new SqlCommand("SELECT * FROM  Sotr", con);
                SqlCommand Kontakt_sotrCom = new SqlCommand("SELECT * FROM  Kontakt_sotr", con);
                SqlCommand BolnichnyCom = new SqlCommand("SELECT * FROM  Bolnichny", con);
                SqlCommand OtpuskCom = new SqlCommand("SELECT * FROM  Otpusk", con);

                con.Open(); //открытие подключения

                DataTable DolgnostDT = new DataTable();
                DataTable TreningDT = new DataTable();
                DataTable RezumDT = new DataTable();
                DataTable Shtatnaya_shemaDT = new DataTable();
                DataTable Prin_na_rabDT = new DataTable();
                DataTable SotrDT = new DataTable();
                DataTable Kontakt_sotrDT = new DataTable();
                DataTable BolnicnyDT = new DataTable();
                DataTable OtpuskDT = new DataTable();

                DolgnostDT.Load(DolgnostCom.ExecuteReader());
                TreningDT.Load(TreningCom.ExecuteReader());
                RezumDT.Load(RezumCom.ExecuteReader());
                Shtatnaya_shemaDT.Load(Shtatnaya_shemaCom.ExecuteReader());
                Prin_na_rabDT.Load(Prin_na_rabCom.ExecuteReader());
                SotrDT.Load(SotrCom.ExecuteReader());
                Kontakt_sotrDT.Load(Kontakt_sotrCom.ExecuteReader());
                BolnicnyDT.Load(BolnichnyCom.ExecuteReader());
                OtpuskDT.Load(OtpuskCom.ExecuteReader());

                DolgnostTbl.ItemsSource = DolgnostDT.DefaultView;
                TreningTbl.ItemsSource = TreningDT.DefaultView;
                RezumTbl.ItemsSource = RezumDT.DefaultView;
                Shtatnaya_shemaTbl.ItemsSource = Shtatnaya_shemaDT.DefaultView;
                Prin_na_rabTbl.ItemsSource = Prin_na_rabDT.DefaultView;
                SotrTbl.ItemsSource = SotrDT.DefaultView;
                Kontakt_sotrTbl.ItemsSource = Kontakt_sotrDT.DefaultView;
                BolnichnyTbl.ItemsSource = BolnicnyDT.DefaultView;
                OtpuskTbl.ItemsSource = OtpuskDT.DefaultView;

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

        private void DolgnostTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DolgnostTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)DolgnostTbl.SelectedItem;
            Name_dolgnost.Text = row["Dolgnost"].ToString();
        }

        private void AddDolgnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddDolgCom = new SqlCommand("AddDolgnost", con); //создание выборки username и pass из таблицы tbl_log
                AddDolgCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddDolgCom.Parameters.AddWithValue("@Dolgnost", Name_dolgnost.Text);
                AddDolgCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void UpdDolgnost_Click(object sender, RoutedEventArgs e)
        {
            if (DolgnostTbl.SelectedItem == null) return;
            DataRowView Dolg = (DataRowView)DolgnostTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdDolgCom = new SqlCommand("UpdateDolgnost", con); //создание выборки username и pass из таблицы tbl_log
                
                UpdDolgCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdDolgCom.Parameters.AddWithValue("@ID_Dolg", (int)Dolg["ID_Dolgnost"]);
                UpdDolgCom.Parameters.AddWithValue("@Dolg", Name_dolgnost.Text); ;
                UpdDolgCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelDolgnost_Click(object sender, RoutedEventArgs e)
        {
            if (DolgnostTbl.SelectedItem == null) return;
            DataRowView Dolg = (DataRowView)DolgnostTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelDolgCom = new SqlCommand("DelDolgnost", con); //создание выборки username и pass из таблицы tbl_log
                DelDolgCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelDolgCom.Parameters.AddWithValue("@ID_Dolg", (int)Dolg["ID_Dolgnost"]);
                DelDolgCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void TreningTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TreningTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)TreningTbl.SelectedItem;
            Date_proved.Text = row["Date_proved"].ToString();
            Kolvo_chas.Text = row["Kolvo_chas"].ToString();
        }

        private void AddTrening_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddTreningCom = new SqlCommand("[dbo].[AddTrening]", con); //создание выборки username и pass из таблицы tbl_log
                AddTreningCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddTreningCom.Parameters.AddWithValue("@Date_proved", Date_proved.Text);
                AddTreningCom.Parameters.AddWithValue("@Kolvo_chas", Kolvo_chas.Text);
                AddTreningCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void UpdTrening_Click(object sender, RoutedEventArgs e)
        {
            if (TreningTbl.SelectedItem == null) return;
            DataRowView Tren = (DataRowView)TreningTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdTreningCom = new SqlCommand("[dbo].[UpdateTrening]", con); //создание выборки username и pass из таблицы tbl_log

                UpdTreningCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdTreningCom.Parameters.AddWithValue("@ID_Tren", (int)Tren["ID_Trening"]);
                UpdTreningCom.Parameters.AddWithValue("@Date_proved", Date_proved.Text);
                UpdTreningCom.Parameters.AddWithValue("@Kolvo_chas", Kolvo_chas.Text);
                UpdTreningCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void DelTrening_Click(object sender, RoutedEventArgs e)
        {
            if (TreningTbl.SelectedItem == null) return;
            DataRowView Tren = (DataRowView)TreningTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelTreningCom = new SqlCommand("[dbo].[DelTrening]", con); //создание выборки username и pass из таблицы tbl_log
                DelTreningCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelTreningCom.Parameters.AddWithValue("@ID_Tren", (int)Tren["ID_Trening"]);
                DelTreningCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void RezumTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RezumTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)RezumTbl.SelectedItem;
            Name_file_rezum.Text = row["Name_file_rezum"].ToString();
            Location_file.Text = row["Location_file"].ToString();
        }

        private void AddRezum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddRezumCom = new SqlCommand("[dbo].[AddRezum]", con); //создание выборки username и pass из таблицы tbl_log
                AddRezumCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddRezumCom.Parameters.AddWithValue("@Name_file_rezum", Name_file_rezum.Text);
                AddRezumCom.Parameters.AddWithValue("@Location_file", Location_file.Text);
                AddRezumCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void UpdRezum_Click(object sender, RoutedEventArgs e)
        {
            if (RezumTbl.SelectedItem == null) return;
            DataRowView Rez = (DataRowView)RezumTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdRezumCom = new SqlCommand("[dbo].[UpdateRezum]", con); //создание выборки username и pass из таблицы tbl_log

                UpdRezumCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdRezumCom.Parameters.AddWithValue("@ID_Rez", (int)Rez["ID_Rezum"]);
                UpdRezumCom.Parameters.AddWithValue("@Name_file_rezum", Name_file_rezum.Text);
                UpdRezumCom.Parameters.AddWithValue("@Location_file", Location_file.Text); ;
                UpdRezumCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelRezum_Click(object sender, RoutedEventArgs e)
        {
            if (RezumTbl.SelectedItem == null) return;
            DataRowView Rez = (DataRowView)RezumTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelRezumCom = new SqlCommand("[dbo].[DelRezum]", con); //создание выборки username и pass из таблицы tbl_log
                DelRezumCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelRezumCom.Parameters.AddWithValue("@ID_Rezum", (int)Rez["ID_Rezum"]);
                DelRezumCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
             //   con.Close();
            }
            otrisKdarRab();
        }

        private void GetDolg()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektZakazcmd = new SqlCommand("SELECT [ID_Dolgnost], [Dolgnost] FROM [dbo].[Dolgnost]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Dolg = new DataTable();
                Dolg.Load(SelektZakazcmd.ExecuteReader());
                ID_DolgnostFK.Items.Clear();
                foreach (DataRow Row in Dolg.Rows)
                {
                    ID_DolgnostFK.Items.Add(new IDComboBoxItem((int)Row["ID_Dolgnost"], Row["Dolgnost"].ToString()));

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

        private void ID_DolgnostFK_DropDownOpened(object sender, EventArgs e)
        {
            GetDolg();
        }

        private void Shtatnaya_shemaTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Shtatnaya_shemaTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)Shtatnaya_shemaTbl.SelectedItem;
            Oklad.Text = row["Oklad"].ToString();
            Stavka.Text = row["Stavka"].ToString();

            GetDolg();

            foreach (IDComboBoxItem item in ID_DolgnostFK.Items)
            {
                if (item.ID == (int)row["ID_Dolgnost"])
                {
                    ID_DolgnostFK.SelectedIndex = ID_DolgnostFK.Items.IndexOf(item);
                }
            }
        }

        private void AddShtatnaya_shema_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddShtatnaya_shemaCom = new SqlCommand("[dbo].[AddShtatnaya_shema]", con); //создание выборки username и pass из таблицы tbl_log
                AddShtatnaya_shemaCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddShtatnaya_shemaCom.Parameters.AddWithValue("@Oklad", Oklad.Text);
                AddShtatnaya_shemaCom.Parameters.AddWithValue("@Stavka", Stavka.Text);;
                AddShtatnaya_shemaCom.Parameters.AddWithValue("@ID_Dolgnost", (int)(ID_DolgnostFK.SelectedItem as IDComboBoxItem).ID);
                AddShtatnaya_shemaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void UpdShtatnaya_shema_Click(object sender, RoutedEventArgs e)
        {
            if (ID_DolgnostFK.SelectedItem == null) return;
            if (Shtatnaya_shemaTbl.SelectedItem == null) return;
            DataRowView StSh = (DataRowView)Shtatnaya_shemaTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdShtatnaya_shemaCom = new SqlCommand("UpdateShtatnaya_shema", con);
                UpdShtatnaya_shemaCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdShtatnaya_shemaCom.Parameters.AddWithValue("@ID_ShtSh", (int)StSh["ID_Shtatnaya_shema"]);
                UpdShtatnaya_shemaCom.Parameters.AddWithValue("@Oklad", Oklad.Text);
                UpdShtatnaya_shemaCom.Parameters.AddWithValue("@Stavka", Stavka.Text); ;
                UpdShtatnaya_shemaCom.Parameters.AddWithValue("@ID_Dolgnost", (int)(ID_DolgnostFK.SelectedItem as IDComboBoxItem).ID);
                UpdShtatnaya_shemaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }
        private void DelShtatnaya_shema_Click(object sender, RoutedEventArgs e)
        {
            if (Shtatnaya_shemaTbl.SelectedItem == null) return;
            DataRowView StSh = (DataRowView)Shtatnaya_shemaTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelShtatnaya_shemaCom = new SqlCommand("[dbo].[DelShtatnaya_shema]", con);
                DelShtatnaya_shemaCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelShtatnaya_shemaCom.Parameters.AddWithValue("@ID_Shem", (int)StSh["ID_Shtatnaya_shema"]);
                DelShtatnaya_shemaCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void GetRezum()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektRezumcmd = new SqlCommand("SELECT ID_Rezum, Name_file_rezum FROM [dbo].[Rezum]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Rez = new DataTable();
                Rez.Load(SelektRezumcmd.ExecuteReader());
                ID_RezumFK.Items.Clear();
                foreach (DataRow Row in Rez.Rows)
                {
                    ID_RezumFK.Items.Add(new IDComboBoxItem((int)Row["ID_Rezum"], Row["Name_file_rezum"].ToString()));

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

        private void GetShtSh()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektShtShcmd = new SqlCommand("SELECT ID_Shtatnaya_shema, Oklad FROM [dbo].[Shtatnaya_shema]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable ShtSh = new DataTable();
                ShtSh.Load(SelektShtShcmd.ExecuteReader());
                ID_Shtatnaya_shemaFK.Items.Clear();
                foreach (DataRow Row in ShtSh.Rows)
                {
                    ID_Shtatnaya_shemaFK.Items.Add(new IDComboBoxItem((int)Row["ID_Shtatnaya_shema"], Row["Oklad"].ToString()));

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

        private void GetTrening()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektTreningcmd = new SqlCommand("SELECT [ID_Trening], [Date_proved] FROM [dbo].[Trening]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Tren = new DataTable();
                Tren.Load(SelektTreningcmd.ExecuteReader());
                ID_TreningFK.Items.Clear();
                foreach (DataRow Row in Tren.Rows)
                {
                    ID_TreningFK.Items.Add(new IDComboBoxItem((int)Row["ID_Trening"], Row["Date_proved"].ToString()));

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

        private void ID_RezumFK_DropDownOpened(object sender, EventArgs e)
        {
            GetRezum();
        }

        private void ID_Shtatnaya_shema_DropDownOpened(object sender, EventArgs e)
        {
            GetShtSh();
        }

        private void ID_TreningFK_DropDownOpened(object sender, EventArgs e)
        {
            GetTrening();
        }

        private void Prin_na_rabTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Prin_na_rabTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)Prin_na_rabTbl.SelectedItem;
            Date_prin_na_rab.Text = row["Date_prin_na_rab"].ToString();

            GetRezum();
            GetShtSh();
            GetTrening();

            foreach (IDComboBoxItem item in ID_RezumFK.Items)
            {
                if (item.ID == (int)row["ID_Rezum"])
                {
                    ID_RezumFK.SelectedIndex = ID_RezumFK.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_Shtatnaya_shemaFK.Items)
            {
                if (item.ID == (int)row["ID_Shtatnaya_shema"])
                {
                    ID_Shtatnaya_shemaFK.SelectedIndex = ID_Shtatnaya_shemaFK.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_TreningFK.Items)
            {
                if (item.ID == (int)row["ID_Trening"])
                {
                    ID_TreningFK.SelectedIndex = ID_TreningFK.Items.IndexOf(item);
                }
            }
        }

        private void AddPrin_na_rab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddPrin_na_rabCom = new SqlCommand("[dbo].[AddPrin_na_rab]", con); //создание выборки username и pass из таблицы tbl_log
                AddPrin_na_rabCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddPrin_na_rabCom.Parameters.AddWithValue("@Date_prin_na_rab", Date_prin_na_rab.Text);
                AddPrin_na_rabCom.Parameters.AddWithValue("@ID_Rezum", (int)(ID_RezumFK.SelectedItem as IDComboBoxItem).ID);
                AddPrin_na_rabCom.Parameters.AddWithValue("@ID_Shtatnaya_shema", (int)(ID_Shtatnaya_shemaFK.SelectedItem as IDComboBoxItem).ID);
                AddPrin_na_rabCom.Parameters.AddWithValue("@ID_Trening", (int)(ID_TreningFK.SelectedItem as IDComboBoxItem).ID);
                AddPrin_na_rabCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void UpdPrin_na_rab_Click(object sender, RoutedEventArgs e)
        {
            if (ID_RezumFK.SelectedItem == null) return;
            if (ID_Shtatnaya_shemaFK.SelectedItem == null) return;
            if (ID_TreningFK.SelectedItem == null) return;
            if (Prin_na_rabTbl.SelectedItem == null) return;
            DataRowView PrinNaRab = (DataRowView)Prin_na_rabTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdPrin_na_rabCom = new SqlCommand("[dbo].[UpdatePrin_na_rab]", con);
                UpdPrin_na_rabCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdPrin_na_rabCom.Parameters.AddWithValue("@ID_Prin", (int)PrinNaRab["ID_Prin_na_rab"]);
                UpdPrin_na_rabCom.Parameters.AddWithValue("@Date_prin_na_rab", Date_prin_na_rab.Text);
                UpdPrin_na_rabCom.Parameters.AddWithValue("@ID_Rezum", (int)(ID_RezumFK.SelectedItem as IDComboBoxItem).ID);
                UpdPrin_na_rabCom.Parameters.AddWithValue("@ID_Shtatnaya_shema", (int)(ID_Shtatnaya_shemaFK.SelectedItem as IDComboBoxItem).ID);
                UpdPrin_na_rabCom.Parameters.AddWithValue("@ID_Trening", (int)(ID_TreningFK.SelectedItem as IDComboBoxItem).ID);
                UpdPrin_na_rabCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelPrin_na_rab_Click(object sender, RoutedEventArgs e)
        {
            if (Prin_na_rabTbl.SelectedItem == null) return;
            DataRowView PrinNaRab = (DataRowView)Prin_na_rabTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelPrin_na_rabCom = new SqlCommand("[dbo].[DelPrin_na_rab]", con);
                DelPrin_na_rabCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelPrin_na_rabCom.Parameters.AddWithValue("@ID_PrinRab", (int)PrinNaRab["ID_Prin_na_rab"]);
                DelPrin_na_rabCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void GetPrinNaRab()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektPrinNaRabcmd = new SqlCommand("SELECT [ID_Prin_na_rab], [Name_file_rezum] FROM [dbo].[Prin_na_rab], [dbo].[Rezum] WHERE Prin_na_rab.ID_Rezum = Rezum.ID_Rezum", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable PrinNaRab = new DataTable();
                PrinNaRab.Load(SelektPrinNaRabcmd.ExecuteReader());
                ID_Prin_na_rabFK.Items.Clear();
                foreach (DataRow Row in PrinNaRab.Rows)
                {
                    ID_Prin_na_rabFK.Items.Add(new IDComboBoxItem((int)Row["ID_Prin_na_rab"], Row["Name_file_rezum"].ToString()));
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

        private void ID_Prin_na_rabFK_DropDownOpened(object sender, EventArgs e)
        {
            GetPrinNaRab();
        }

        private void SotrTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SotrTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)SotrTbl.SelectedItem;
            Name_sotr.Text = row["Name_sotr"].ToString();
            Fam_sotr.Text = row["Fam_sotr"].ToString();
            Otch_sotr.Text = row["Otch_sotr"].ToString();
            Stag.Text = row["Stag"].ToString();
            Pol.Text = row["Pol"].ToString();

            GetPrinNaRab();

            foreach (IDComboBoxItem item in ID_Prin_na_rabFK.Items)
            {
                if (item.ID == (int)row["ID_Prin_na_rab"])
                {
                    ID_Prin_na_rabFK.SelectedIndex = ID_Prin_na_rabFK.Items.IndexOf(item);
                }
            }
        }

        private void AddSotr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddSotrCom = new SqlCommand("[dbo].[AddSotr]", con); //создание выборки username и pass из таблицы tbl_log
                AddSotrCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddSotrCom.Parameters.AddWithValue("@Name_sotr", Name_sotr.Text);
                AddSotrCom.Parameters.AddWithValue("@Fam_sotr", Fam_sotr.Text);
                AddSotrCom.Parameters.AddWithValue("@Otch_sotr", Otch_sotr.Text);
                AddSotrCom.Parameters.AddWithValue("@Stag", Stag.Text);
                AddSotrCom.Parameters.AddWithValue("@Pol", Pol.Text);
                AddSotrCom.Parameters.AddWithValue("@ID_Prin_na_rab", (int)(ID_Prin_na_rabFK.SelectedItem as IDComboBoxItem).ID);
                AddSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
             //   con.Close();
            }
            otrisKdarRab();
        }

        private void UpdSotr_Click(object sender, RoutedEventArgs e)
        {
            if (ID_Prin_na_rabFK.SelectedItem == null) return;
            if (SotrTbl.SelectedItem == null) return;
            DataRowView Sotr = (DataRowView)SotrTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdSotrCom = new SqlCommand("UpdateSotr", con);
                UpdSotrCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdSotrCom.Parameters.AddWithValue("@ID_Sotr", (int)Sotr["ID_Sotr"]);
                UpdSotrCom.Parameters.AddWithValue("@Name_sotr", Name_sotr.Text);
                UpdSotrCom.Parameters.AddWithValue("@Fam_sotr", Fam_sotr.Text);
                UpdSotrCom.Parameters.AddWithValue("@Otch_sotr", Otch_sotr.Text);
                UpdSotrCom.Parameters.AddWithValue("@Stag", Stag.Text);
                UpdSotrCom.Parameters.AddWithValue("@Pol", Pol.Text);
                UpdSotrCom.Parameters.AddWithValue("@ID_Prin_na_rab", (int)(ID_Prin_na_rabFK.SelectedItem as IDComboBoxItem).ID);
                UpdSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisKdarRab();
        }

        private void DelSotr_Click(object sender, RoutedEventArgs e)
        {
            if (SotrTbl.SelectedItem == null) return;
            DataRowView Sotr = (DataRowView)SotrTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelSotrCom = new SqlCommand("[dbo].[DelSotr]", con);
                DelSotrCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelSotrCom.Parameters.AddWithValue("@ID_Sotr", (int)Sotr["ID_Sotr"]);
                DelSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void GetSotrFKKont()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektSotrcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Sotr = new DataTable();
                Sotr.Load(SelektSotrcmd.ExecuteReader());
                ID_SotrFKKont.Items.Clear();
                foreach (DataRow Row in Sotr.Rows)
                {
                    ID_SotrFKKont.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
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

        private void ID_SotrFKKont_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKKont();
        }

        private void Kontakt_sotrTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kontakt_sotrTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)Kontakt_sotrTbl.SelectedItem;
            Telefon.Text = row["Telefon_sotr"].ToString();
            Pochta.Text = row["Pochta_sotr"].ToString();
            Adres_sotr.Text = row["Adres_sotr"].ToString();

            GetSotrFKKont();

            foreach (IDComboBoxItem item in ID_SotrFKKont.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKKont.SelectedIndex = ID_SotrFKKont.Items.IndexOf(item);
                }
            }
        }

        private void AddKontakt_sotr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddKonSotrCom = new SqlCommand("[dbo].[AddKontakt_sotr]", con); //создание выборки username и pass из таблицы tbl_log
                AddKonSotrCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddKonSotrCom.Parameters.AddWithValue("@Telefon_sotr", Telefon.Text);
                AddKonSotrCom.Parameters.AddWithValue("@Pochta_sotr", Pochta.Text);
                AddKonSotrCom.Parameters.AddWithValue("@Adres_sotr", Adres_sotr.Text);
                AddKonSotrCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKKont.SelectedItem as IDComboBoxItem).ID);
                AddKonSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
             //   con.Close();
            }
            otrisKdarRab();
        }

        private void UpdKontakt_sotr_Click(object sender, RoutedEventArgs e)
        {
            if (ID_SotrFKKont.SelectedItem == null) return;
            if (Kontakt_sotrTbl.SelectedItem == null) return;
            DataRowView Sotr = (DataRowView)Kontakt_sotrTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
             //   SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdKonSotrCom = new SqlCommand("UpdateKontakt_sotr", con);
                UpdKonSotrCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdKonSotrCom.Parameters.AddWithValue("@ID_Kont", (int)Sotr["ID_Kontakt"]);
                UpdKonSotrCom.Parameters.AddWithValue("@Telefon_sotr", Telefon.Text);
                UpdKonSotrCom.Parameters.AddWithValue("@Pochta_sotr", Pochta.Text);
                UpdKonSotrCom.Parameters.AddWithValue("@Adres_sotr", Adres_sotr.Text);
                UpdKonSotrCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKKont.SelectedItem as IDComboBoxItem).ID);
                UpdKonSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelKontakt_sotr_Click(object sender, RoutedEventArgs e)
        {
            if (Kontakt_sotrTbl.SelectedItem == null) return;
            DataRowView Sotr = (DataRowView)Kontakt_sotrTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
             //   SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelKonSotrCom = new SqlCommand("[dbo].[DelKontakt_sotr]", con);
                DelKonSotrCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelKonSotrCom.Parameters.AddWithValue("@ID_Kont", (int)Sotr["ID_Kontakt"]);
                DelKonSotrCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void GetSotrFKBol()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
             //   SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektSotrFKBolcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKBol = new DataTable();
                SotrFKBol.Load(SelektSotrFKBolcmd.ExecuteReader());
                ID_SotrFKBol.Items.Clear();
                foreach (DataRow Row in SotrFKBol.Rows)
                {
                    ID_SotrFKBol.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
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

        private void ID_SotrFKBol_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKBol();
        }

        private void BolnichnyTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BolnichnyTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)BolnichnyTbl.SelectedItem;
            Kolichestvo_dney.Text = row["Kolichestvo_dney"].ToString();
            Date_vidachi.Text = row["Date_vidachi"].ToString();

            GetSotrFKBol();

            foreach (IDComboBoxItem item in ID_SotrFKBol.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKBol.SelectedIndex = ID_SotrFKBol.Items.IndexOf(item);
                }
            }
        }

        private void AddBolnichny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddBolCom = new SqlCommand("[dbo].[AddBolnichny]", con); //создание выборки username и pass из таблицы tbl_log
                AddBolCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddBolCom.Parameters.AddWithValue("@Kol_dney", Kolichestvo_dney.Text);
                AddBolCom.Parameters.AddWithValue("@Date_vidach", Date_vidachi.Text);
                AddBolCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKBol.SelectedItem as IDComboBoxItem).ID);
                AddBolCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void UpdBolnichny_Click(object sender, RoutedEventArgs e)
        {
            if (ID_SotrFKBol.SelectedItem == null) return;
            if (BolnichnyTbl.SelectedItem == null) return;
            DataRowView Bol = (DataRowView)BolnichnyTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdBolCom = new SqlCommand("UpdateBolnichny", con);
                UpdBolCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdBolCom.Parameters.AddWithValue("@ID_Bol", (int)Bol["ID_Bolnichny"]);
                UpdBolCom.Parameters.AddWithValue("@Kol_dney", Kolichestvo_dney.Text);
                UpdBolCom.Parameters.AddWithValue("@Date_vidach", Date_vidachi.Text);
                UpdBolCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKBol.SelectedItem as IDComboBoxItem).ID);
                UpdBolCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelBolnichny_Click(object sender, RoutedEventArgs e)
        {
            if (BolnichnyTbl.SelectedItem == null) return;
            DataRowView Bol = (DataRowView)BolnichnyTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelBolCom = new SqlCommand("[dbo].[DelBolnichny]", con);
                DelBolCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelBolCom.Parameters.AddWithValue("@ID_Bol", (int)Bol["ID_Bolnichny"]);
                DelBolCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void GetSotrFKOtp()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektSotrFKOtpcmd = new SqlCommand("SELECT [ID_Sotr], [Fam_sotr] FROM [dbo].[Sotr]", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrFKOtp = new DataTable();
                SotrFKOtp.Load(SelektSotrFKOtpcmd.ExecuteReader());
                ID_SotrFKBol.Items.Clear();
                foreach (DataRow Row in SotrFKOtp.Rows)
                {
                    ID_SotrFKOtp.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));
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

        private void ID_SotrFKOtp_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKOtp();
        }

        private void OtpuskTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtpuskTbl.SelectedItem == null) return;
            DataRowView row = (DataRowView)OtpuskTbl.SelectedItem;
            Vid_otpuska.Text = row["Vid_otpuska"].ToString();
            Kolvo_dney_otpuska.Text = row["Kolichestvo_dney"].ToString();

            GetSotrFKOtp();

            foreach (IDComboBoxItem item in ID_SotrFKOtp.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKOtp.SelectedIndex = ID_SotrFKOtp.Items.IndexOf(item);
                }
            }
        }

        private void AddOtpusk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddOtpCom = new SqlCommand("AddOtpusk", con); //создание выборки username и pass из таблицы tbl_log
                AddOtpCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddOtpCom.Parameters.AddWithValue("@Vid_otpuska", Vid_otpuska.Text);
                AddOtpCom.Parameters.AddWithValue("@Kolichestvo_dney", Kolvo_dney_otpuska.Text);
                AddOtpCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKOtp.SelectedItem as IDComboBoxItem).ID);
                AddOtpCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void UpdOtpusk_Click(object sender, RoutedEventArgs e)
        {
            if (ID_SotrFKOtp.SelectedItem == null) return;
            if (OtpuskTbl.SelectedItem == null) return;
            DataRowView Otp = (DataRowView)OtpuskTbl.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdOtpCom = new SqlCommand("UpdateOtpusk", con);
                UpdOtpCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdOtpCom.Parameters.AddWithValue("@ID_Otp", (int)Otp["ID_Otpusk"]);
                UpdOtpCom.Parameters.AddWithValue("@Vid_otpuska", Vid_otpuska.Text);
                UpdOtpCom.Parameters.AddWithValue("@Kolichestvo_dney", Kolvo_dney_otpuska.Text);
                UpdOtpCom.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKOtp.SelectedItem as IDComboBoxItem).ID);
                UpdOtpCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }

        private void DelOtpusk_Click(object sender, RoutedEventArgs e)
        {
            if (OtpuskTbl.SelectedItem == null) return;
            DataRowView Otp = (DataRowView)OtpuskTbl.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
              //  SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelOtpCom = new SqlCommand("DelOtpusk", con);
                DelOtpCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelOtpCom.Parameters.AddWithValue("@ID_Otp", (int)Otp["ID_Otpusk"]);
                DelOtpCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisKdarRab();
        }
    }
}
