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
using System.Windows.Threading;

namespace IS_Restoran
{
    /// <summary>
    /// Логика взаимодействия для LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        public LoadWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        DispatcherTimer ds = new DispatcherTimer();

        private void ds_Tick(object sender, EventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
            ds.Stop();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ds.Tick += new EventHandler(ds_Tick);
            ds.Interval = new TimeSpan(0, 0, 3);
            ds.Start();
        }
    }
}
