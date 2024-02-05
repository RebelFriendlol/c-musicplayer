using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AlbertoPlayer.pages
{
    public partial class Shop : Page
    {


        private List<AlbertoSong> songs;
        private List<AlbertoSong> koszyk;
        private AlbertoUser zalogowanyUzytkownik;


        private Image mainImage;
        private Image secondImage;
        private DispatcherTimer timer;
        private bool isFirstImageVisible = true;

        private string _firstImageText;
        public string FirstImageText
        {
            get { return _firstImageText; }
            set
            {
                if (_firstImageText != value)
                {
                    _firstImageText = value;
                    OnPropertyChanged(nameof(FirstImageText));
                }
            }
        }

        public string[] sort { get; set; }

        private string _selectedSort;

        public string SelectedSort

        {
            get { return _selectedSort; }
            set
            {
                if (_selectedSort != value)
                {
                    _selectedSort = value;
                    // Handle the selection change here if needed
                }
            }
        }



        private string _secondImageText;
        public string SecondImageText
        {
            get { return _secondImageText; }
            set
            {
                if (_secondImageText != value)
                {
                    _secondImageText = value;
                    OnPropertyChanged(nameof(SecondImageText));
                }
            }
        }



        public List<AlbertoSong> Songs
        {
            get { return songs; }
            set
            {
                songs = value;
                OnPropertyChanged(nameof(Songs));
            }
        }

        public Shop(AlbertoUser loggedInUser)
        {
            InitializeComponent();
            DataContext = this;
            zalogowanyUzytkownik = loggedInUser;
            koszyk = new List<AlbertoSong>();


            //pobierz dane z bazy danych i przypisz do Songs
            using (var dbContext = new AlbertoDbContext())
            {
                Songs = dbContext.AlbertoSongs.ToList();
            }

            sort = new string[] { "cena-rosnąco", "cena-malejąco", "alfabetycznie" };

            DataContext = this;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // Adjust the interval as needed
            timer.Tick += Timer_Tick;
            timer.Start();

            FirstImageText = "Alberto";



        }

        private void DodajDoKoszyka_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is AlbertoSong selectedSong)
            {
                koszyk.Add(selectedSong); // Dodaj wybraną piosenkę do koszyka
                MessageBox.Show("Dodano do koszyka!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                if (zalogowanyUzytkownik != null)
                {
                    Koszyk koszykPage = new Koszyk(zalogowanyUzytkownik, koszyk); // Przekazanie koszyka do nowej strony
                    NavigationService.Navigate(koszykPage);
                }
                else
                {
                    MessageBox.Show("Musisz być zalogowany, aby zobaczyć koszyk.", "Brak autoryzacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FadeIn(UIElement element)
        {
            element.Visibility = Visibility.Visible;
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }
       

        private void FadeOut(UIElement element)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            fadeOutAnimation.Completed += (s, ev) => { element.Visibility = Visibility.Hidden; };
            element.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            // Toggle visibility and trigger fade animation
            if (isFirstImageVisible)
            {
                FadeOut(firstRectangle);
                FadeIn(newImage);
            }
            else
            {
                FadeOut(newImage);
                FadeIn(firstRectangle);
            }

            isFirstImageVisible = !isFirstImageVisible;
        }

        private void imageCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double canvasWidth = imageCanvas.ActualWidth;
            double canvasHeight = imageCanvas.ActualHeight;

            // Recalculate positioning for firstRectangle
            double firstRectangleX = (canvasWidth - firstRectangle.Width) / 2;
            double firstRectangleY = (canvasHeight - firstRectangle.Height) / 3;

            // Recalculate positioning for newImage
            double newImageX = (canvasWidth - newImage.Width) / 2;
            double newImageY = (canvasHeight - newImage.Height) / 2;

            // Update the position of firstRectangle
            firstRectangle.RenderTransform = new TranslateTransform(firstRectangleX, firstRectangleY);

            // Update the position of newImage
            newImage.RenderTransform = new TranslateTransform(newImageX, newImageY);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}