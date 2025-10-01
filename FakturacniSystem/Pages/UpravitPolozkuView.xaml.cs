using FakturacniSystem.Code;
using FakturacniSystem.EF;
using System;
using System.Collections.Generic;
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
    /// Interakční logika pro UpravitPolozkuView.xaml
    /// </summary>
    public partial class UpravitPolozkuView : Page
    {
        public Polozka polozka;
        public UpravitPolozkuView(Polozka polozka)
        {
            InitializeComponent();
            if(polozka == null)
            {
                MessageBox.Show("vybrana polozka je null");
            } else
            {
                this.polozka = polozka;
                DataContext = polozka;

            }
        }

        private void PotrvditUpravu(object sender, RoutedEventArgs e)
        {
            using(var db = new SqliteContext())
            {
                var DbPolozky = db.Polozky.ToList();
                for (int i = 0; i < DbPolozky.Count; i++) {
                    if (DbPolozky[i].Id == polozka.Id)
                    {
                        DbPolozky[i].Jmeno = polozka.Jmeno;

                        DbPolozky[i].Pocet = polozka.Pocet;



                    }
                }
                db.SaveChanges();
            }
            
            MessageBox.Show("uprava uspesna");
            NavigationService.Navigate(new SkladView());

        }
        private void ZrusitUpravu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("uprava zrusena");
            NavigationService.GoBack();

        }
    }
}
