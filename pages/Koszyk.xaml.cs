using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AlbertoPlayer.pages
{
    public partial class Koszyk : Page
    {
        private AlbertoUser zalogowanyUzytkownik;
        private List<AlbertoSong> koszyk1;

        public Koszyk(AlbertoUser user, List<AlbertoSong> koszyk)
        {
            InitializeComponent();
            zalogowanyUzytkownik = user;
            koszyk1 = koszyk;

            listView.ItemsSource = koszyk;

            GridView gridView = new GridView();
            listView.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Wykonawca",
                DisplayMemberBinding = new Binding("Wykonawca")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tytuł",
                DisplayMemberBinding = new Binding("Tytul")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Cena",
                DisplayMemberBinding = new Binding("Cena")
            });
        }

        private void ZaplacButton_Click(object sender, RoutedEventArgs e)
        {
            if (zalogowanyUzytkownik != null && koszyk1.Any())
            {
                decimal sumaDoZaplaty = koszyk1.Sum(song => song.Cena);

                MessageBoxResult result = MessageBox.Show($"Czy na pewno chcesz zapłacić {sumaDoZaplaty} zł?", "Potwierdzenie płatności", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var dbContext = new AlbertoDbContext())
                        {
                            // Utwórz obiekt zakupu dla każdej piosenki w koszyku
                            foreach (var song in koszyk1)
                            {
                                var zakup = new AlbertoZakup
                                {
                                    IDuser = zalogowanyUzytkownik.IDuser,
                                    IDsong = song.IDsong,
                                    CenaZakupu = song.Cena,
                                    DataZakupu = DateTime.Now
                                };

                                // Dodaj zakup do bazy danych
                                dbContext.AlbertoZakupy.Add(zakup);
                            }

                            // Zapisz zmiany w bazie danych
                            dbContext.SaveChanges();
                        }

                        // Wyczyszczenie koszyka po udanej płatności
                        koszyk1.Clear();
                        listView.ItemsSource = null; // Odświeżenie widoku
                        MessageBox.Show("Płatność zakończona pomyślnie!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas zapisywania zakupów: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Koszyk jest pusty lub użytkownik nie jest zalogowany.");
            }
        }
    }
}