using FakturacniSystem.Code;
using FakturacniSystem.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FakturacniSystem.Pages
{
    /// <summary>
    /// Interakční logika pro ZaznamDodavateluView.xaml
    /// </summary>
    public partial class ZaznamDodavateluView : Page
    {
        public List<DodanaPolozka> dodavatele = new List<DodanaPolozka>();
        public List<(DodanaPolozka, Polozka)> Contract = new List<(DodanaPolozka, Polozka)>();

        public ZaznamDodavateluView()
        {
            InitializeComponent();
            LoadDb();
            DodavateleList.ItemsSource = Contract;

        }

        private void LoadDb()
        {
            using (SqliteContext db = new SqliteContext())
            {
                dodavatele = db.Dodavatele.ToList();


                Contract.Add((dodavatele[0], db.Polozky.ToList()[0]));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PridatDodavatele(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PridatDodavatele());
        }

        private void UpravitDodavatele(object sender, RoutedEventArgs e)
        {
            if (DodavateleList.SelectedItem != null)
            {
                NavigationService.Navigate(new UpravitPolozkuView((Polozka)DodavateleList.SelectedItem));
            }
        }

    }
}
