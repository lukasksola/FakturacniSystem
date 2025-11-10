using FakturacniSystem.Code;
using FakturacniSystem.EF;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FakturacniSystem.Pages
{
    /// <summary>
    /// Interakční logika pro PridatDodavatele.xaml
    /// </summary>
    public partial class PridatDodavatele : Page
    {
        public PridatDodavatele()
        {
            InitializeComponent();
            this.DataContext = this;
        }

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
                    int parsnutyInput = InputHandler.ParseInputtedTextToInt(value);
                    if(parsnutyInput != -1)
                    {
                        _pridanyPocet = parsnutyInput;
                        OnPropertyChanged(nameof(pridanyPocet));
                    }
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

        string _nazevDodavatele;
        public string NazevDodavatele
        {
            get { return _nazevDodavatele; }
            set
            {
                if (value != _nazevDodavatele && value != null)
                {
                    _nazevDodavatele = value;

                    OnPropertyChanged(nameof(NazevDodavatele));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Pridej(object sender, RoutedEventArgs e)
        {
            var polozka = new Polozka();
            polozka.Jmeno = _nazev;
            polozka.Pocet = _pridanyPocet;
            if (!AddPocet())
            {
                //vytvari novou polozku
                using (var db = new SqliteContext())
                {
                    db.Polozky.Add(polozka);
                    db.SaveChanges();
                }

                MessageBox.Show("Polozka pridana");
            }
            else
            {
                MessageBox.Show("Pocet pridan");
            }

            using (var db = new SqliteContext())
            {
                var dodavatel = new DodanaPolozka();
                dodavatel.NazevDodavatele = _nazevDodavatele;
                int id = 0;

                foreach(Polozka pol in db.Polozky.ToList())
                {
                    if(pol.Jmeno == polozka.Jmeno)
                    {
                        id = pol.Id;
                    }
                }
                dodavatel.PolozkaId = id;
                dodavatel.denDodani = DateTime.Today;
                dodavatel.pocetPolozekPridano = _pridanyPocet;
                db.Dodavatele.Add(dodavatel);
                db.SaveChanges();

            }


            NavigationService.Navigate(new SkladView());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> vraci true pokud najde polozku, false pokud vytvari novou </returns>
        bool AddPocet()
        {
            using (var db = new SqliteContext())
            {
                if (_id != 0)
                {

                    foreach (var item in db.Polozky)
                    {
                        if (item.Id == _id)
                        {
                            if (_pridanyPocet != 0)
                            {
                                item.Pocet += _pridanyPocet;
                                db.SaveChanges();
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
                            db.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }

            }
            return false;

        }

        
    }
}
