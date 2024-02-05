using System;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows;

namespace AlbertoPlayer.ViewModels
{
    public class PiosenkaViewModel
    {
        public BitmapImage Zdjecie { get; set; }
        public string Nazwa { get; set; }
        public ICommand PlayCommand { get; set; }
        // Dodaj inne właściwości, jeśli są potrzebne

        public PiosenkaViewModel(BitmapImage zdjecie, string nazwa, Action playAction)
        {
            Zdjecie = zdjecie;
            Nazwa = nazwa;
            PlayCommand = new RelayCommand(playAction);
        }
    }
}