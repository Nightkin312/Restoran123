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
    /// Логика взаимодействия для SklRabForm.xaml
    /// </summary>
    public partial class SklRabForm : Window
    {
        string DataSource;
        string UserID;
        string Password;
        string InitialCatalog;

        //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");

        public SklRabForm()
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

        private void otrisSklad()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand PostavshikCom = new SqlCommand("SELECT * FROM  Postavshik", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand TovarCom = new SqlCommand("SELECT * FROM  Tovar", con);
                SqlCommand Tov_na_sklCom = new SqlCommand("SELECT * FROM  Tovar_na_sklade", con);
                SqlCommand Sklad_producktCom = new SqlCommand("SELECT * FROM  Sklad_produckt", con);
                SqlCommand Dog_s_postCom = new SqlCommand("SELECT * FROM  Dogovor_s_postavshikom", con);
                SqlCommand Zakaz_tovCom = new SqlCommand("SELECT * FROM  Zakaz_tovara", con);
                SqlCommand Sost_zak_prodCom = new SqlCommand("SELECT * FROM  Sost_zakaza_productov", con);
                con.Open(); //открытие подключения
                DataTable PostDT = new DataTable();
                DataTable TovDT = new DataTable();
                DataTable TovNSklDT = new DataTable();
                DataTable SklDT = new DataTable();
                DataTable DogDT = new DataTable();
                DataTable ZakTovDT = new DataTable();
                DataTable SostZakProdDT = new DataTable();
                PostDT.Load(PostavshikCom.ExecuteReader());
                TovDT.Load(TovarCom.ExecuteReader());
                TovNSklDT.Load(Tov_na_sklCom.ExecuteReader());
                SklDT.Load(Sklad_producktCom.ExecuteReader());
                DogDT.Load(Dog_s_postCom.ExecuteReader());
                ZakTovDT.Load(Zakaz_tovCom.ExecuteReader());
                SostZakProdDT.Load(Sost_zak_prodCom.ExecuteReader());
                PostavshikTblSklRab.ItemsSource = PostDT.DefaultView;
                TovarTblSklRab.ItemsSource = TovDT.DefaultView;
                Tovar_na_skaldeTblSklRab.ItemsSource = TovNSklDT.DefaultView;
                Sklad_producktTblSklRab.ItemsSource = SklDT.DefaultView;
                Dog_s_postTblSklRab.ItemsSource = DogDT.DefaultView;
                Zakaz_tovaraTblSklRab.ItemsSource = ZakTovDT.DefaultView;
                Sost_zakaza_productovTblSklRab.ItemsSource = SostZakProdDT.DefaultView;

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
                SqlCommand PostavshikCom = new SqlCommand("SELECT * FROM  Postavshik", con); //создание выборки username и pass из таблицы tbl_log
                SqlCommand TovarCom = new SqlCommand("SELECT * FROM  Tovar", con);
                SqlCommand Tov_na_sklCom = new SqlCommand("SELECT * FROM  Tovar_na_sklade", con);
                SqlCommand Sklad_producktCom = new SqlCommand("SELECT * FROM  Sklad_produckt", con);
                SqlCommand Dog_s_postCom = new SqlCommand("SELECT * FROM  Dogovor_s_postavshikom", con);
                SqlCommand Zakaz_tovCom = new SqlCommand("SELECT * FROM  Zakaz_tovara", con);
                SqlCommand Sost_zak_prodCom = new SqlCommand("SELECT * FROM  Sost_zakaza_productov", con);
                con.Open(); //открытие подключения
                DataTable PostDT = new DataTable();
                DataTable TovDT = new DataTable();
                DataTable TovNSklDT = new DataTable();
                DataTable SklDT = new DataTable();
                DataTable DogDT = new DataTable();
                DataTable ZakTovDT = new DataTable();
                DataTable SostZakProdDT = new DataTable();
                PostDT.Load(PostavshikCom.ExecuteReader());
                TovDT.Load(TovarCom.ExecuteReader());
                TovNSklDT.Load(Tov_na_sklCom.ExecuteReader());
                SklDT.Load(Sklad_producktCom.ExecuteReader());
                DogDT.Load(Dog_s_postCom.ExecuteReader());
                ZakTovDT.Load(Zakaz_tovCom.ExecuteReader());
                SostZakProdDT.Load(Sost_zak_prodCom.ExecuteReader());
                PostavshikTblSklRab.ItemsSource = PostDT.DefaultView;
                TovarTblSklRab.ItemsSource = TovDT.DefaultView;
                Tovar_na_skaldeTblSklRab.ItemsSource = TovNSklDT.DefaultView;
                Sklad_producktTblSklRab.ItemsSource = SklDT.DefaultView;
                Dog_s_postTblSklRab.ItemsSource = DogDT.DefaultView;
                Zakaz_tovaraTblSklRab.ItemsSource = ZakTovDT.DefaultView;
                Sost_zakaza_productovTblSklRab.ItemsSource = SostZakProdDT.DefaultView;

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

        private void AddPostSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddPostCom = new SqlCommand("AddPostavshik", con); //создание выборки username и pass из таблицы tbl_log
                AddPostCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddPostCom.Parameters.AddWithValue("@Naimenovanie", Naim_PostSklRab.Text);
                AddPostCom.Parameters.AddWithValue("@Adres", AdresSklRab.Text);
                AddPostCom.Parameters.AddWithValue("@Telefon", TelefonSklRab.Text);
                AddPostCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisSklad();
        }

        private void UpdPostSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (PostavshikTblSklRab.SelectedItem == null) return;
            DataRowView Postavshik = (DataRowView)PostavshikTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdPostCom = new SqlCommand("UpdatePostavshik", con); //создание выборки username и pass из таблицы tbl_log
                UpdPostCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdPostCom.Parameters.AddWithValue("@ID_Post", (int)Postavshik["ID_Postavshik"]);
                UpdPostCom.Parameters.AddWithValue("@Naimenovanie", Naim_PostSklRab.Text);
                UpdPostCom.Parameters.AddWithValue("@Adres", AdresSklRab.Text);
                UpdPostCom.Parameters.AddWithValue("@Telefon", TelefonSklRab.Text);
                UpdPostCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisSklad();
        }

        private void DelPostSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (PostavshikTblSklRab.SelectedItem == null) return;
            DataRowView Postavshik = (DataRowView)PostavshikTblSklRab.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
           {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelPostCom = new SqlCommand("DelPostavshik", con); //создание выборки username и pass из таблицы tbl_log
                DelPostCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelPostCom.Parameters.AddWithValue("@ID_Post", (int)Postavshik["ID_Postavshik"]);
                DelPostCom.ExecuteNonQuery();
            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisSklad();
        }

        private void PostavshikTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostavshikTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)PostavshikTblSklRab.SelectedItem;
            Naim_PostSklRab.Text = row["Naimenovanie"].ToString();
            AdresSklRab.Text = row["Adres"].ToString();
            TelefonSklRab.Text = row["Telefon"].ToString();
        }

        private void GetPost()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektPostscmd = new SqlCommand("SELECT ID_Postavshik, Naimenovanie FROM Postavshik", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Post = new DataTable();
                Post.Load(SelektPostscmd.ExecuteReader());
                ID_PostavshikFKTovarSklRab.Items.Clear();
                foreach (DataRow Row in Post.Rows)
                {
                    ID_PostavshikFKTovarSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Postavshik"], Row["Naimenovanie"].ToString()));

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

        private void GetPostFKDog()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektPostscmd = new SqlCommand("SELECT ID_Postavshik, Naimenovanie FROM Postavshik", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Post = new DataTable();
                Post.Load(SelektPostscmd.ExecuteReader());
                ID_PostavshikFKDog_s_postSklRab.Items.Clear();
                foreach (DataRow Row in Post.Rows)
                {
                    ID_PostavshikFKDog_s_postSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Postavshik"], Row["Naimenovanie"].ToString()));

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

        private void ID_PostavshikFKTovarSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetPost();
        }

        private void TovarTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TovarTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)TovarTblSklRab.SelectedItem;
            Kolvo_na_skladSklRab.Text = row["Kolvo_na_sklade"].ToString();
            Name_tovaraSklRab.Text = row["Name_tovara"].ToString();
            Cena_tovaraSklRab.Text = row["Cena_tovar"].ToString();
            HaracterSklRab.Text = row["Harakter"].ToString();

            GetPost();

            foreach (IDComboBoxItem item in ID_PostavshikFKTovarSklRab.Items)
            {
                if (item.ID == (int)row["ID_Postavshik"])
                {
                    ID_PostavshikFKTovarSklRab.SelectedIndex = ID_PostavshikFKTovarSklRab.Items.IndexOf(item);
                }
            }
        }
        private void GetTovar()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektTovarscmd = new SqlCommand("SELECT ID_Tovar, Name_tovara FROM Tovar", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Tov = new DataTable();
                Tov.Load(SelektTovarscmd.ExecuteReader());
                ID_TovarFKTov_na_sklSklRab.Items.Clear();
                foreach (DataRow Row in Tov.Rows)
                {
                    ID_TovarFKTov_na_sklSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Tovar"], Row["Name_tovara"].ToString()));

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

        private void GetProd()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektPordscmd = new SqlCommand("SELECT ID_Produckt, Name_produckt FROM Sklad_produckt", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Prod = new DataTable();
                Prod.Load(SelektPordscmd.ExecuteReader());
                ID_ProductFKTov_na_sklSklRab.Items.Clear();
                foreach (DataRow Row in Prod.Rows)
                {
                    ID_ProductFKTov_na_sklSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Produckt"], Row["Name_produckt"].ToString()));

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

        private void ID_TovarFKTov_na_sklSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetTovar();
        }

        private void ID_ProductFKTov_na_sklSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetProd();
        }

        private void Tovar_na_skaldeTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tovar_na_skaldeTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)Tovar_na_skaldeTblSklRab.SelectedItem;
            KolvoSklRab.Text = row["Kolvo"].ToString();

            GetProd();
            GetTovar();

            foreach (IDComboBoxItem item in ID_TovarFKTov_na_sklSklRab.Items)
            {
                if (item.ID == (int)row["ID_Tovar"])
                {
                    ID_TovarFKTov_na_sklSklRab.SelectedIndex = ID_TovarFKTov_na_sklSklRab.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_ProductFKTov_na_sklSklRab.Items)
            {
                if (item.ID == (int)row["ID_Produckt"])
                {
                    ID_ProductFKTov_na_sklSklRab.SelectedIndex = ID_ProductFKTov_na_sklSklRab.Items.IndexOf(item);
                }
            }
        }

        private void AddTovarSklRab_Click(object sender, RoutedEventArgs e)
        {    
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddTovarCom = new SqlCommand("AddTovar", con); //создание выборки username и pass из таблицы tbl_log
                AddTovarCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                AddTovarCom.Parameters.AddWithValue("@Kolvo_na_sklade", Kolvo_na_skladSklRab.Text);
                AddTovarCom.Parameters.AddWithValue("@Name_tovara", Name_tovaraSklRab.Text);
                AddTovarCom.Parameters.AddWithValue("@Cena_tovar", Cena_tovaraSklRab.Text);
                AddTovarCom.Parameters.AddWithValue("@Harakter", HaracterSklRab.Text);
                AddTovarCom.Parameters.AddWithValue("@ID_Postavshik", (int)(ID_PostavshikFKTovarSklRab.SelectedItem as IDComboBoxItem).ID);
                AddTovarCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisSklad();
        }

        private void UpdTovarSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (ID_PostavshikFKTovarSklRab.SelectedItem == null) return;
            if (TovarTblSklRab.SelectedItem == null) return;
            DataRowView Tov = (DataRowView)TovarTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand UpdTovarCom = new SqlCommand("UpdateTovar", con);
                UpdTovarCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                UpdTovarCom.Parameters.AddWithValue("@ID_Tov", (int)Tov["ID_Tovar"]);
                UpdTovarCom.Parameters.AddWithValue("@Kolvo_na_sklade", Kolvo_na_skladSklRab.Text);
                UpdTovarCom.Parameters.AddWithValue("@Name_tovara", Name_tovaraSklRab.Text);
                UpdTovarCom.Parameters.AddWithValue("@Cena_tovar", Cena_tovaraSklRab.Text);
                UpdTovarCom.Parameters.AddWithValue("@Harakter", HaracterSklRab.Text);
                UpdTovarCom.Parameters.AddWithValue("@ID_Postavshik", (int)(ID_PostavshikFKTovarSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdTovarCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
//                con.Close();
            }
            otrisSklad();
        }

        private void DelTovarSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (TovarTblSklRab.SelectedItem == null) return;
            DataRowView Tov = (DataRowView)TovarTblSklRab.SelectedItem;
            if (!(MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;");
                SqlCommand DelTovarCom = new SqlCommand("DelTovar", con);
                DelTovarCom.CommandType = CommandType.StoredProcedure;
                //MessageBox.Show(con.ConnectionString.ToString());
                con.Open(); //открытие подключения
                DelTovarCom.Parameters.AddWithValue("@ID_Tov", (int)Tov["ID_Tovar"]);
                DelTovarCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
//                con.Close();
            }
            otrisSklad();
        }

        private void AddTovar_na_skladeSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //              SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddTovarNaSklCom = new SqlCommand("AddTovar_na_sklade", con); //создание выборки username и pass из таблицы tbl_log
                AddTovarNaSklCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения

                AddTovarNaSklCom.Parameters.AddWithValue("@Kolvo", KolvoSklRab.Text);
                AddTovarNaSklCom.Parameters.AddWithValue("@ID_Produckt", (int)(ID_ProductFKTov_na_sklSklRab.SelectedItem as IDComboBoxItem).ID);
                AddTovarNaSklCom.Parameters.AddWithValue("@ID_Tovar", (int)(ID_TovarFKTov_na_sklSklRab.SelectedItem as IDComboBoxItem).ID);
                AddTovarNaSklCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
                //con.Close();
            }
            otrisSklad();
        }

        private void UpdTovar_na_skladeSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (ID_ProductFKTov_na_sklSklRab.SelectedItem == null) return;
            if (ID_TovarFKTov_na_sklSklRab.SelectedItem == null) return;
            if (Tovar_na_skaldeTblSklRab.SelectedItem == null) return;
            DataRowView TovNaSkl = (DataRowView)Tovar_na_skaldeTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdTovarNaSklCom = new SqlCommand("UpdateTovar_na_sklade", con); //создание выборки username и pass из таблицы tbl_log
                UpdTovarNaSklCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdTovarNaSklCom.Parameters.AddWithValue("@ID_TovSkl", (int)TovNaSkl["ID_Tovar_na_sklade"]);
                UpdTovarNaSklCom.Parameters.AddWithValue("@Kolvo", KolvoSklRab.Text);
                UpdTovarNaSklCom.Parameters.AddWithValue("@ID_Produckt", (int)(ID_ProductFKTov_na_sklSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdTovarNaSklCom.Parameters.AddWithValue("@ID_Tovar", (int)(ID_TovarFKTov_na_sklSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdTovarNaSklCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisSklad();
        }

        private void DelTovar_na_skladeSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Tovar_na_skaldeTblSklRab.SelectedItem == null) return;
            DataRowView TovNaSkl = (DataRowView)Tovar_na_skaldeTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelTovarNaSklCom = new SqlCommand("DelTovar_na_sklade", con); //создание выборки username и pass из таблицы tbl_log
                DelTovarNaSklCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelTovarNaSklCom.Parameters.AddWithValue("@ID_TovSkl", (int)TovNaSkl["ID_Tovar_na_sklade"]);
                DelTovarNaSklCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisSklad();
        }

        private void Sklad_producktTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sklad_producktTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)Sklad_producktTblSklRab.SelectedItem;
            Name_productSklRab.Text = row["Name_produckt"].ToString();
        }

        private void AddSklad_producktSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddAddSklad_producktCom = new SqlCommand("AddSklad_produckt", con); //создание выборки username и pass из таблицы tbl_log
                AddAddSklad_producktCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения

                AddAddSklad_producktCom.Parameters.AddWithValue("@Name_produckt", Name_productSklRab.Text);
                AddAddSklad_producktCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisSklad();
        }

        private void UpdSklad_producktSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Sklad_producktTblSklRab.SelectedItem == null) return;
            DataRowView Sklad_produckt = (DataRowView)Sklad_producktTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdSklad_producktCom = new SqlCommand("UpdateSklad_produckt", con); //создание выборки username и pass из таблицы tbl_log
                UpdSklad_producktCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdSklad_producktCom.Parameters.AddWithValue("@ID_Prod", (int)Sklad_produckt["ID_Produckt"]);
                UpdSklad_producktCom.Parameters.AddWithValue("@Name_produckt", Name_productSklRab.Text);
                UpdSklad_producktCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void DelSklad_producktSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Sklad_producktTblSklRab.SelectedItem == null) return;
            DataRowView Sklad_produckt = (DataRowView)Sklad_producktTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DellSklad_producktCom = new SqlCommand("DelSklad_produckt", con); //создание выборки username и pass из таблицы tbl_log
                DellSklad_producktCom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DellSklad_producktCom.Parameters.AddWithValue("@ID_SkladProd", (int)Sklad_produckt["ID_Produckt"]);
                DellSklad_producktCom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisSklad();
        }

        private void Dog_s_postTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dog_s_postTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)Dog_s_postTblSklRab.SelectedItem;
            Nomer_dogovoraSklRab.Text = row["Nomer_dogovora"].ToString();
            Nazvanie_dogovoraSklRab.Text = row["Nazvanie_dogovora"].ToString();
            Date_zakluchSklRab.Text = row["Date_zakluch"].ToString();

            GetPostFKDog();

            foreach (IDComboBoxItem item in ID_PostavshikFKDog_s_postSklRab.Items)
            {
                if (item.ID == (int)row["ID_Postavshik"])
                {
                    ID_PostavshikFKDog_s_postSklRab.SelectedIndex = ID_PostavshikFKDog_s_postSklRab.Items.IndexOf(item);
                }
            }
        }

        private void ID_PostavshikFKDog_s_postSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetPostFKDog();
        }

        private void AddDog_s_postSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddDogovor_s_postavshikom = new SqlCommand("AddDogovor_s_postavshikom", con); //создание выборки username и pass из таблицы tbl_log
                AddDogovor_s_postavshikom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения

                AddDogovor_s_postavshikom.Parameters.AddWithValue("@Nomer_dogovora", Nomer_dogovoraSklRab.Text);
                AddDogovor_s_postavshikom.Parameters.AddWithValue("@Nazvanie_dogovora", Nazvanie_dogovoraSklRab.Text);
                AddDogovor_s_postavshikom.Parameters.AddWithValue("@Date_zakluch", Date_zakluchSklRab.Text);
                AddDogovor_s_postavshikom.Parameters.AddWithValue("@ID_Postavshik", (int)(ID_PostavshikFKDog_s_postSklRab.SelectedItem as IDComboBoxItem).ID);
                AddDogovor_s_postavshikom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void UpdDog_s_postSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Dog_s_postTblSklRab.SelectedItem == null) return;
            DataRowView Dogovor_s_postavshikom = (DataRowView)Dog_s_postTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdDogovor_s_postavshikom = new SqlCommand("UpdateDogovor_s_postavshikom", con); //создание выборки username и pass из таблицы tbl_log
                UpdDogovor_s_postavshikom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdDogovor_s_postavshikom.Parameters.AddWithValue("@ID_Dog", (int)Dogovor_s_postavshikom["ID_Dogovor_s_postavshikom"]);
                UpdDogovor_s_postavshikom.Parameters.AddWithValue("@Nomer_dogovora", Nomer_dogovoraSklRab.Text);
                UpdDogovor_s_postavshikom.Parameters.AddWithValue("@Nazvanie_dogovora", Nazvanie_dogovoraSklRab.Text);
                UpdDogovor_s_postavshikom.Parameters.AddWithValue("@Date_zakluch", Date_zakluchSklRab.Text);
                UpdDogovor_s_postavshikom.Parameters.AddWithValue("@ID_Postavshik", (int)(ID_PostavshikFKDog_s_postSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdDogovor_s_postavshikom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
               // con.Close();
            }
            otrisSklad();
        }

        private void DelDog_s_postSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Dog_s_postTblSklRab.SelectedItem == null) return;
            DataRowView Dogovor_s_postavshikom = (DataRowView)Dog_s_postTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelDogovor_s_postavshikom = new SqlCommand("DelDogovor_s_postavshikom", con); //создание выборки username и pass из таблицы tbl_log
                DelDogovor_s_postavshikom.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelDogovor_s_postavshikom.Parameters.AddWithValue("@ID_Dog", (int)Dogovor_s_postavshikom["ID_Dogovor_s_postavshikom"]);
                DelDogovor_s_postavshikom.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void GetDogPostFKZAkTov()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektDogPostscmd = new SqlCommand("SELECT ID_Dogovor_s_postavshikom, Nomer_dogovora FROM Dogovor_s_postavshikom", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable DogPost = new DataTable();
                DogPost.Load(SelektDogPostscmd.ExecuteReader());
                ID_Dog_s_postFKZak_tovSklRab.Items.Clear();
                foreach (DataRow Row in DogPost.Rows)
                {
                    ID_Dog_s_postFKZak_tovSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Dogovor_s_postavshikom"], Row["Nomer_dogovora"].ToString()));

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

        private void GetSotrFKZAkTov()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektSotrcmd = new SqlCommand("SELECT ID_Sotr, Fam_sotr FROM Sotr", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable SotrPost = new DataTable();
                SotrPost.Load(SelektSotrcmd.ExecuteReader());
                ID_SotrFKZak_tovSklRab.Items.Clear();
                foreach (DataRow Row in SotrPost.Rows)
                {
                    ID_SotrFKZak_tovSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Sotr"], Row["Fam_sotr"].ToString()));

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

        private void Zakaz_tovaraTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Zakaz_tovaraTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)Zakaz_tovaraTblSklRab.SelectedItem;
            Nomer_zakaza_prodSklRab.Text = row["Nomer_zakaza"].ToString();
            Date_zakazaSklRab.Text = row["Date_zakaza"].ToString();
            Summa_zakazaSklRab.Text = row["Summa_zakaza"].ToString();

            GetDogPostFKZAkTov();
            GetSotrFKZAkTov();

            foreach (IDComboBoxItem item in ID_Dog_s_postFKZak_tovSklRab.Items)
            {
                if (item.ID == (int)row["ID_Dogovor_s_postavshikom"])
                {
                    ID_Dog_s_postFKZak_tovSklRab.SelectedIndex = ID_Dog_s_postFKZak_tovSklRab.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in ID_SotrFKZak_tovSklRab.Items)
            {
                if (item.ID == (int)row["ID_Sotr"])
                {
                    ID_SotrFKZak_tovSklRab.SelectedIndex = ID_SotrFKZak_tovSklRab.Items.IndexOf(item);
                }
            }
        }

        private void AddZakaz_tovaraSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
                //SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddZakaz_tovara = new SqlCommand("[dbo].[AddZakaz_tovara]", con); //создание выборки username и pass из таблицы tbl_log
                AddZakaz_tovara.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения

                AddZakaz_tovara.Parameters.AddWithValue("@Nomer_zakaza", Nomer_zakaza_prodSklRab.Text);
                AddZakaz_tovara.Parameters.AddWithValue("@Date_zakaza", Date_zakazaSklRab.Text);
                AddZakaz_tovara.Parameters.AddWithValue("@Summa_zakaza", Summa_zakazaSklRab.Text);
                AddZakaz_tovara.Parameters.AddWithValue("@ID_Dogovor_s_postavshikom", (int)(ID_Dog_s_postFKZak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                AddZakaz_tovara.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                AddZakaz_tovara.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void UpdZakaz_tovaraSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Zakaz_tovaraTblSklRab.SelectedItem == null) return;
            DataRowView Zakaz_tovara = (DataRowView)Zakaz_tovaraTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdZakaz_tovara = new SqlCommand("[dbo].[UpdateZakaz_tovara]", con); //создание выборки username и pass из таблицы tbl_log
                UpdZakaz_tovara.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdZakaz_tovara.Parameters.AddWithValue("@ID_ZakTov", (int)Zakaz_tovara["ID_Zakaz_tovara"]);
                UpdZakaz_tovara.Parameters.AddWithValue("@Nomer_zakaza", Nomer_zakaza_prodSklRab.Text);
                UpdZakaz_tovara.Parameters.AddWithValue("@Date_zakaza", Date_zakazaSklRab.Text);
                UpdZakaz_tovara.Parameters.AddWithValue("@Summa_zakaza", Summa_zakazaSklRab.Text);
                UpdZakaz_tovara.Parameters.AddWithValue("@ID_Dogovor_s_postavshikom", (int)(ID_Dog_s_postFKZak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdZakaz_tovara.Parameters.AddWithValue("@ID_Sotr", (int)(ID_SotrFKZak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdZakaz_tovara.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void DelZakaz_tovaraSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Zakaz_tovaraTblSklRab.SelectedItem == null) return;
            DataRowView Zakaz_tovara = (DataRowView)Zakaz_tovaraTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelZakaz_tovara = new SqlCommand("[dbo].[DelZakaz_tovara]", con); //создание выборки username и pass из таблицы tbl_log
                DelZakaz_tovara.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelZakaz_tovara.Parameters.AddWithValue("@ID_ZakTov", (int)Zakaz_tovara["ID_Zakaz_tovara"]);
                DelZakaz_tovara.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void GetTovNaSklFKSostZAkTov()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektTovNaSklcmd = new SqlCommand("SELECT ID_Tovar_na_sklade, Name_tovara FROM Tovar_na_sklade, Tovar WHERE Tovar_na_sklade.ID_Tovar = Tovar.ID_Tovar", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable TovNaSkl = new DataTable();
                TovNaSkl.Load(SelektTovNaSklcmd.ExecuteReader());
                Tovar_na_skaldeFKSost_zak_tovSklRab.Items.Clear();
                foreach (DataRow Row in TovNaSkl.Rows)
                {
                    Tovar_na_skaldeFKSost_zak_tovSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Tovar_na_sklade"], Row["Name_tovara"].ToString()));
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

        private void GetZakazFKSostZAkTov()
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand SelektZakazcmd = new SqlCommand("SELECT ID_Zakaz_tovara, Nomer_zakaza FROM Zakaz_tovara", con); //создание выборки username и pass из таблицы tbl_log
                con.Open(); //открытие подключения
                DataTable Zakaz = new DataTable();
                Zakaz.Load(SelektZakazcmd.ExecuteReader());
                Zakaz_tovaraFKSost_zak_tovSklRab.Items.Clear();
                foreach (DataRow Row in Zakaz.Rows)
                {
                    Zakaz_tovaraFKSost_zak_tovSklRab.Items.Add(new IDComboBoxItem((int)Row["ID_Zakaz_tovara"], Row["Nomer_zakaza"].ToString()));

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

        private void Sost_zakaza_productovTblSklRab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sost_zakaza_productovTblSklRab.SelectedItem == null) return;
            DataRowView row = (DataRowView)Sost_zakaza_productovTblSklRab.SelectedItem;
            KolvoTovaraSklRab.Text = row["Kolvo"].ToString();
            Name_tovarSklRab.Text = row["Name_tovar"].ToString();

            GetTovNaSklFKSostZAkTov();
            GetZakazFKSostZAkTov();

            foreach (IDComboBoxItem item in Zakaz_tovaraFKSost_zak_tovSklRab.Items)
            {
                if (item.ID == (int)row["ID_Zakaz_tovara"])
                {
                    Zakaz_tovaraFKSost_zak_tovSklRab.SelectedIndex = Zakaz_tovaraFKSost_zak_tovSklRab.Items.IndexOf(item);
                }
            }

            foreach (IDComboBoxItem item in Tovar_na_skaldeFKSost_zak_tovSklRab.Items)
            {
                if (item.ID == (int)row["ID_Tovar_na_sklade"])
                {
                    Tovar_na_skaldeFKSost_zak_tovSklRab.SelectedIndex = Tovar_na_skaldeFKSost_zak_tovSklRab.Items.IndexOf(item);
                }
            }
        }

        private void AddSost_zakaza_productovSklRab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand AddSost_zakaza_productov = new SqlCommand("AddSost_zakaza_productov", con); //создание выборки username и pass из таблицы tbl_log
                AddSost_zakaza_productov.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения

                AddSost_zakaza_productov.Parameters.AddWithValue("@Kolvo", KolvoTovaraSklRab.Text);
                AddSost_zakaza_productov.Parameters.AddWithValue("@Name_tovar", Name_tovarSklRab.Text);
                AddSost_zakaza_productov.Parameters.AddWithValue("@ID_Zakaz_tovara", (int)(Zakaz_tovaraFKSost_zak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                AddSost_zakaza_productov.Parameters.AddWithValue("@ID_Tovar_na_sklade", (int)(Tovar_na_skaldeFKSost_zak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                AddSost_zakaza_productov.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void UpdSost_zakaza_productovSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Sost_zakaza_productovTblSklRab.SelectedItem == null) return;
            DataRowView Sost_zakaza_productov = (DataRowView)Sost_zakaza_productovTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand UpdSost_zakaza_productov = new SqlCommand("[dbo].[UpdateSost_zakaza_productov]", con); //создание выборки username и pass из таблицы tbl_log
                UpdSost_zakaza_productov.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                UpdSost_zakaza_productov.Parameters.AddWithValue("@ID_SostZakProd", (int)Sost_zakaza_productov["ID_Sost_produkt"]);
                UpdSost_zakaza_productov.Parameters.AddWithValue("@Kolvo", KolvoTovaraSklRab.Text);
                UpdSost_zakaza_productov.Parameters.AddWithValue("@Name_tovar", Name_tovarSklRab.Text);
                UpdSost_zakaza_productov.Parameters.AddWithValue("@ID_Zakaz_tovara", (int)(Zakaz_tovaraFKSost_zak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdSost_zakaza_productov.Parameters.AddWithValue("@ID_Tovar_na_sklade", (int)(Tovar_na_skaldeFKSost_zak_tovSklRab.SelectedItem as IDComboBoxItem).ID);
                UpdSost_zakaza_productov.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void DelSost_zakaza_productovSklRab_Click(object sender, RoutedEventArgs e)
        {
            if (Sost_zakaza_productovTblSklRab.SelectedItem == null) return;
            DataRowView Sost_zakaza_productov = (DataRowView)Sost_zakaza_productovTblSklRab.SelectedItem;
            try
            {
                SqlConnection con = Connector.Connection(DataSource, UserID, Password, InitialCatalog);
               // SqlConnection con = new SqlConnection(@"server=KINPC\MSSQLSERVER1; database=Restoran; Integrated Security = true;"); //создание подключения к БД
                SqlCommand DelSost_zakaza_productov = new SqlCommand("[dbo].[DelSost_zakaza_productov]", con); //создание выборки username и pass из таблицы tbl_log
                DelSost_zakaza_productov.CommandType = CommandType.StoredProcedure;
                con.Open(); //открытие подключения
                DelSost_zakaza_productov.Parameters.AddWithValue("@ID_SostZakProd", (int)Sost_zakaza_productov["ID_Sost_produkt"]);
                DelSost_zakaza_productov.ExecuteNonQuery();

            }
            catch (Exception ex) //показываем ошибку
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK); //выводим сообщение с ошибкой
            }
            finally
            {
              //  con.Close();
            }
            otrisSklad();
        }

        private void ID_Dog_s_postFKZak_tovSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetDogPostFKZAkTov();
        }

        private void ID_SotrFKZak_tovSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetSotrFKZAkTov();
        }

        private void Zakaz_tovaraFKSost_zak_tovSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetZakazFKSostZAkTov();
        }

        private void Tovar_na_skaldeFKSost_zak_tovSklRab_DropDownOpened(object sender, EventArgs e)
        {
            GetTovNaSklFKSostZAkTov();
        }
    }
}