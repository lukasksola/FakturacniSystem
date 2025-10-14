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
        public List<DodavaniZaznam> Contract = new List<DodavaniZaznam>();

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

                foreach (DodanaPolozka dodano in dodavatele) {
                    DodavaniZaznam zaznam = new DodavaniZaznam();
                    zaznam.dodavani = dodano;

                    zaznam.polozka = db.Polozky.ToList().FirstOrDefault(x => x.Id == dodano.PolozkaId); 

                    Contract.Add(zaznam);
                }


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
                NavigationService.Navigate(new UpravitDodavateleView((DodavaniZaznam)DodavateleList.SelectedItem));
            }
        }

    }
}
