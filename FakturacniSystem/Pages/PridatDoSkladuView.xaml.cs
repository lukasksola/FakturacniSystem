using FakturacniSystem.Code;
using FakturacniSystem.EF;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FakturacniSystem.Pages
{
    /// <summary>
    /// Interakční logika pro PridatDoSkladuView.xaml
    /// </summary>
    public partial class PridatDoSkladuView : Page, INotifyPropertyChanged
    {

        string _nazev;
        public string Nazev
        {
            get { return _nazev; }
            set
            {
                if (value != _nazev && value != null)
                {
                    _nazev = value;

                    OnPropertyChanged(nameof(Nazev));
                }
            }
        }
        int _pridanyPocet;
        public string pridanyPocet
        {
            get { return _pridanyPocet + ""; }
            set
            {
                if (value != _pridanyPocet + "" && value != null)
                {
                    _pridanyPocet = int.Parse(value);

                    OnPropertyChanged(nameof(pridanyPocet));
                }
            }
        }

        int _id;
        public string Id
        {
            get { return _id + ""; }
            set
            {
                if (value != _id + "" && value != null)
                {
                    _id = int.Parse(value);

                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public PridatDoSkladuView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Pridej(object sender, RoutedEventArgs e)
        {
            
            if (!AddPocet())
            {
                using (var db = new SqliteContext()) {
                    var polozka = new Polozka();
                    polozka.Jmeno = _nazev;
                    polozka.Pocet = _pridanyPocet;
                    
                    db.Polozky.Add(polozka);
                    db.SaveChanges();
                }
                MessageBox.Show("Polozka pridana");
            }
        }

        bool AddPocet()
        {
            using (var db = new SqliteContext())
            {
                if (Id != null)
                {

                    foreach (var item in db.Polozky)
                    {
                        if (item.Id == _id)
                        {
                            if (_pridanyPocet != 0)
                            {
                                item.Pocet += _pridanyPocet;

                            }

                            return true;
                        }
                    }
                    return false;

                }

                if (_nazev != null)
                {
                    foreach (var item in db.Polozky)
                    {
                        if (item.Jmeno == _nazev)
                        {

                            item.Pocet += _pridanyPocet;

                            return true;
                        }
                    }
                    return false;
                }
                db.SaveChanges();
            }
            return false;

        }
    }
}
