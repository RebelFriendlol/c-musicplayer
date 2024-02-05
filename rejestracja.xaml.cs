using Microsoft.Data.SqlClient;
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

namespace AlbertoPlayer
{

    public partial class rejestracja : Window
    {
        public rejestracja()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            using (var context = new AlbertoDbContext())//tworzenie obiektu(uzytkownika) na podstawie zmiennych ktore sa odpowiednie kolumnom w tabeli
            {
                var nowyUser = new AlbertoUser
                {
                    LoginUser = textlogin.Text,
                    EmailAddress = textemial.Text,
                    PasswordUser = texthaslo.Text,
                    CountryUser = textkraj.Text,
                    GenderUser = men.IsChecked == true ? "Mezczyzna" : "Kobieta",
                    DateOfBirthUser = textdata1.SelectedDate,
                    DateOfCreateUser = DateTime.Now,
                };

                nowyUser.SetPassword(texthaslo.Text);

                //pozniej bede musial zamienic te if'y ale to narazie dziala
                if (string.IsNullOrWhiteSpace(textlogin.Text))
                {
                    MessageBox.Show("Wypełnij pole Login!");
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textemial.Text))
                {
                    MessageBox.Show("Wypełnij pole E-mail!");
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(texthaslo.Text))
                {
                    MessageBox.Show("Wypełnij pole Haslo!");
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textkraj.Text))
                {
                    MessageBox.Show("Wypełnij pole KRAJ!");
                    this.Close();
                    return;
                }




                context.AlbertoUsers.Add(nowyUser);
                try
                {
                    context.SaveChanges();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Konto z takim E-mailem już istnieje!");
                }
            }
            this.Close();
        }
    }

}
