using System.Windows;
using System.Windows.Controls;

namespace AlbertoPlayer.pages
{
    public partial class Rejestracja : Page
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        // Obsługa przycisku powrotu do logowania
        private void Button_Powrot(object sender, RoutedEventArgs e)
        {
            // Utwórz obiekt klasy logowanie
            logowanie logowanieWindow = new logowanie();

            // Sprawdź, czy NavigationService jest dostępny dla strony Rejestracja
            if (NavigationService != null)
            {
                // Ustaw nową stronę do nawigacji
                NavigationService.Navigate(logowanieWindow);
            }
        }

        // Dodaj logikę obsługi zdarzeń, przycisków itp.
    }
}