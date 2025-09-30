using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FakturacniSystem.Code;
using FakturacniSystem.EF;

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
    }
}
