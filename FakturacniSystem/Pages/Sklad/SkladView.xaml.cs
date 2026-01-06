using FakturacniSystem.Code;
using FakturacniSystem.EF;
using FakturacniSystem.Pages.Sklad;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace FakturacniSystem.Pages
{
    /// <summary>
    /// Interakční logika pro SkladView.xaml
    /// </summary>
    public partial class SkladView : Page, INotifyPropertyChanged
    {
        public List<Polozka> Sklad = new List<Polozka>();
        public SkladView()
        {
            InitializeComponent();
            ManageSkladCount.ReloadSkladCount();
            LoadDB();
            SkladList.ItemsSource = Sklad;

        }

        private void LoadDB()
        {
            using (var db = new SqliteContext())
            {
                Sklad = db.Polozky.ToList();
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PridatDoSkladu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PridatDoSkladuView());
        }

        private void UpravitPolozku(object sender, RoutedEventArgs e)
        {
            if (SkladList.SelectedItem != null)
            {
                NavigationService.Navigate(new UpravitPolozkuView((Polozka)SkladList.SelectedItem));
            }
        }
        private void ZobrazitHistorii(object sender, RoutedEventArgs e)
        {
            if (SkladList.SelectedItem != null)
            {
                NavigationService.Navigate(new HistorieView((Polozka)SkladList.SelectedItem));

            }
        }

        private void SmazatPolozky(object sender, RoutedEventArgs e)
        {
            if (SkladList.SelectedItems.Count > 0)
            {
                using (SqliteContext db = new SqliteContext())
                {
                    
                    foreach (Polozka polozka in SkladList.SelectedItems)
                    {
                
                        for(int i = 0; i < db.Dodavatele.Count(); i++)
                        {
                            if (db.Dodavatele.ToList()[i].PolozkaId == polozka.Id)
                            {
                                db.Dodavatele.Remove(db.Dodavatele.ElementAt(i));
                            }

                        }

                        for (int i = 0; i < db.Odebiratele.Count(); i++)
                        {
                            if (db.Odebiratele.ToList()[i].PolozkaId == polozka.Id)
                            {
                                db.Odebiratele.Remove(db.Odebiratele.ElementAt(i));
                            }

                        }

                        
                        db.Polozky.Remove(polozka);
                            
                        MessageBox.Show($"smaznuto");

                    }
                    db.SaveChanges();
                }


            }
        }
    }
}
