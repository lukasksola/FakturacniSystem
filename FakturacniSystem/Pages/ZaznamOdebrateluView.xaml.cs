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
    /// Interakční logika pro ZaznamOdebrateluView.xaml
    /// </summary>
    public partial class ZaznamOdebrateluView : Page, INotifyPropertyChanged
    {
        public ZaznamOdebrateluView()
        {
            InitializeComponent();
            LoadDb();
            DodavateleList.ItemsSource = Contract;
        }

        public List<OdebraniZaznam> odberatele = new List<OdebraniZaznam>();
        public List<DodanyOdber> Contract = new List<DodanyOdber>();

        private void LoadDb()
        {
            using (SqliteContext db = new SqliteContext())
            {
                odberatele = db.Odebiratele.ToList();

                foreach (OdebraniZaznam odebrano in odberatele)
                {
                    DodanyOdber odberContract = new DodanyOdber();
                    odberContract.odebirani = odebrano;

                    odberContract.polozka = db.Polozky.ToList().FirstOrDefault(x => x.Id == odebrano.PolozkaId);

                    Contract.Add(odberContract);
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
