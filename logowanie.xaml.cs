using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
using AlbertoPlayer.pages;
using System.Collections.Generic;
using static AlbertoPlayer.MainWindow;

namespace AlbertoPlayer
{
    public partial class logowanie : Window
    {
        private TextBox emailtextBox;
        private PasswordBox haslopasswordBox;
        public event EventHandler<string> UserLoggedIn;
        private Page koszykPage;
        public logowanie()
        {
            InitializeComponent();
            
            emailtextBox = FindName("emailtext") as TextBox;
            haslopasswordBox = FindName("haslopassword") as PasswordBox;
        }

        public class AlbertoUser
        {
            [Required]
            public string EmailAddress { get; set; }
            [Required]
            public string PasswordUser { get; set; }
            public string LoginUser { get; set; }

            public void SetPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    PasswordUser = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }

            public bool VerifyPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    string inputHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    return inputHash == PasswordUser;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (AlbertoDbContext _context = new AlbertoDbContext())
            {
                string enteredPassword = haslopasswordBox.Password;
                AlbertoPlayer.AlbertoUser user = _context.AlbertoUsers.SingleOrDefault(u => u.EmailAddress == emailtextBox.Text);

                if (user != null && user.VerifyPassword(enteredPassword))
                {
                    MessageBox.Show("Poprawne dane logowania! Witaj, " + user.LoginUser, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Przekazanie zalogowanego użytkownika do strony Koszyk
                    Koszyk koszykPage = new Koszyk(user, new List<AlbertoSong>());

                    // Użyj konstruktora z odpowiednimi argumentami
                    MainWindow mainWindow = new MainWindow(user, koszykPage);
                    mainWindow.nazwausers.Text = user.LoginUser;

                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe dane logowania. Spróbuj ponownie.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            rejestracja noweOknoLogowanie = new rejestracja();
            noweOknoLogowanie.ShowDialog();
        }
    }
}