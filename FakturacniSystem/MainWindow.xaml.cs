using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FakturacniSystem.EF;
using FakturacniSystem.Pages;
using Microsoft.EntityFrameworkCore;

namespace FakturacniSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SkladView());

        }
        private void SkladNavigate(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SkladView());
            SelectedButton("sklad");
        }
        private void DodavateleNavigate(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ZaznamDodavateluView());
            SelectedButton("dodavatele");

        }
        private void OdebirateleNavigate(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ZaznamOdebrateluView());
            SelectedButton("odebiratele");

        }

        void SelectedButton(string jakejBut)
        {
            skladBut.Background = Brushes.LightGray;
            dodavateleBut.Background = Brushes.LightGray;
            odebirateleBut.Background = Brushes.LightGray;

            skladBut.Foreground = Brushes.Black;
            dodavateleBut.Foreground = Brushes.Black;
            odebirateleBut.Foreground = Brushes.Black;



            if (jakejBut == "sklad")
            {
                skladBut.Background = Brushes.AntiqueWhite;
                skladBut.Foreground = Brushes.Black;
            } else if (jakejBut == "dodavatele")
            {
                dodavateleBut.Background = Brushes.LightBlue;


            }
            else if(jakejBut == "odebiratele")
            {
                odebirateleBut.Background = Brushes.LightGreen;

            }
        }
    }
}