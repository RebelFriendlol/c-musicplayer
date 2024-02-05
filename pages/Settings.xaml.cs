using Microsoft.Win32;
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

namespace AlbertoPlayer.pages
{
    /// <summary>
    /// Logika interakcji dla klasy Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        public string[] names {  get; set; }

        private MainWindow mainWindow;

        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            names = new string[] { "Poslki", "Angielski", "Niggerzynski" };
            DataContext = this;
        }

        private void FileSelect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.png;*.jpg;*.bmp";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog() == true)
            {
                mainWindow.Logo.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }

        private void Backup(object sender, RoutedEventArgs e)
        {

        }
    }
}
