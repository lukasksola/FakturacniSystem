using FakturacniSystem.Code;
using FakturacniSystem.EF;
using System.Windows;
using System.Windows.Controls;

namespace FakturacniSystem.Pages.Sklad
{
    /// <summary>
    /// Interakční logika pro HistorieView.xaml
    /// </summary>
    public partial class HistorieView : Page
    {
        public Polozka polozka;
        public List<DodanaPolozka> historieDodani;
        public List<OdebraniPolozka> historieOdberu;

        public HistorieView(Polozka polozka)
        {
            InitializeComponent();
            this.polozka = polozka;
            HistorieKoho.Text = $"Historie: {polozka.Jmeno}";
            ScanSkladCount();
            Dodavatele.ItemsSource = historieDodani;
            Odberatele.ItemsSource = historieOdberu;

        }

        public void ScanSkladCount()
        {
            historieDodani = new List<DodanaPolozka>();
            historieOdberu = new List<OdebraniPolozka>();
            using (var db = new SqliteContext())
            {
                var sklad = db.Polozky.ToList();
                var dodavani = db.Dodavatele.ToList();
                var odebirani = db.Odebiratele.ToList();


                for (int j = 0; j < dodavani.Count; j++)
                {
                    if (polozka.Id == dodavani[j].PolozkaId)
                    {
                        historieDodani.Add(dodavani[j]);

                    }

                }
                for (int j = 0; j < odebirani.Count; j++)
                {
                    if (polozka.Id == odebirani[j].PolozkaId)
                    {
                        historieOdberu.Add(odebirani[j]);


                    }

                }




            }
        }

        private void SmazatZaznam(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Dodavatele.SelectedItems.Count > 0)
            {
                MessageBox.Show(Dodavatele.SelectedItems.Count + "");

                using (SqliteContext db = new SqliteContext())
                {
                    foreach (DodanaPolozka polozka in Dodavatele.SelectedItems)
                    {
                        if (db.Dodavatele.Contains(polozka))
                        {
                            db.Dodavatele.Remove(polozka);
                        }
                    }
                    db.SaveChanges();


                }
            }

            if (Odberatele.SelectedItems.Count > 0)
            {
                MessageBox.Show(Odberatele.SelectedItems.Count + "");

                using (SqliteContext db = new SqliteContext())
                {
                    foreach (OdebraniPolozka polozka in Odberatele.SelectedItems)
                    {
                        if (db.Odebiratele.Contains(polozka))
                        {
                            db.Odebiratele.Remove(polozka);
                        }
                    }
                    db.SaveChanges();

                }
            }

        }
    }
}
