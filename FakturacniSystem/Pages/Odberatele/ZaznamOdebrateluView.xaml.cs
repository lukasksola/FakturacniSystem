using FakturacniSystem.Code;
using FakturacniSystem.Code.Slouceni;
using FakturacniSystem.EF;
using FakturacniSystem.Pages.Odberatele;
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
        //faktura ma byt
        public ZaznamOdebrateluView()
        {
            InitializeComponent();
            LoadDb();
            OdberateleList.ItemsSource = Contract;
        }

        public List<OdebraniPolozka> odberatele = new List<OdebraniPolozka>();
        public List<OdebiraniSlouceno> Contract = new List<OdebiraniSlouceno>();

        private void LoadDb()
        {
            using (SqliteContext db = new SqliteContext())
            {
                odberatele = db.Odebiratele.ToList();
                var polozky = db.Polozky.ToList();

                foreach (OdebraniPolozka odebrano in odberatele)
                {
                    OdebiraniSlouceno odberContract = new OdebiraniSlouceno();
                    odberContract.odebirani = odebrano;

                    odberContract.polozka = polozky.FirstOrDefault(x => x.Id == odebrano.PolozkaId);

                    Contract.Add(odberContract);
                }


            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PridatOdberatele(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PridatOdberateleView());
        }
        private void VystavitFakturu(object sender, RoutedEventArgs e)
        {
            if (OdberateleList.SelectedItem != null)
            {
                new FakturyCreator().CreatePDF((OdebiraniSlouceno)OdberateleList.SelectedItem);
            }
        }

        private void UpravitOdberatele(object sender, RoutedEventArgs e)
        {
            if (OdberateleList.SelectedItem != null)
            {
                NavigationService.Navigate(new UpravitDodavateleView((DodavaniSlouceno)OdberateleList.SelectedItem));
            }
        }
    }
}
