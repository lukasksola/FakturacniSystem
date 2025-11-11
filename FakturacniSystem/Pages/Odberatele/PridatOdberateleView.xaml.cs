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

namespace FakturacniSystem.Pages.Odberatele
{
    /// <summary>
    /// Interakční logika pro PridatOdberateleView.xaml
    /// </summary>
    public partial class PridatOdberateleView : Page, INotifyPropertyChanged
    {
        public PridatOdberateleView()
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
        int _odebranyPocet;
        public string OdebranyPocet
        {
            get { return _odebranyPocet + ""; }
            set
            {
                if (value != _odebranyPocet + "" && value != null)
                {
                    int parsnutyInput = InputHandler.ParseInputtedTextToInt(value);
                    if (parsnutyInput != -1)
                    {
                        _odebranyPocet = parsnutyInput;
                        celkovaCena.Text = _cenaZaKus * _odebranyPocet + "";
                        OnPropertyChanged(nameof(OdebranyPocet));
                    }
                }
            }
        }

        int _cenaZaKus;
        public string CenaZaKus
        {
            get { return _cenaZaKus + ""; }
            set
            {
                if (value != _cenaZaKus + "" && value != null)
                {
                    int parsnutyInput = InputHandler.ParseInputtedTextToInt(value);
                    if (parsnutyInput != -1)
                    {
                        _cenaZaKus = parsnutyInput;
                        celkovaCena.Text = _cenaZaKus * _odebranyPocet + "";
                        OnPropertyChanged(nameof(CenaZaKus));
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

        string _nazevOdberatele;
        public string NazevOdberatele
        {
            get { return _nazevOdberatele; }
            set
            {
                if (value != _nazevOdberatele && value != null)
                {
                    _nazevOdberatele = value;

                    OnPropertyChanged(nameof(NazevOdberatele));
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
            var odberatel = new OdebraniPolozka();
            odberatel.pocetPolozekOdebrano = _odebranyPocet;
            odberatel.NazevOdberatele = _nazevOdberatele;
            odberatel.CenaZaKus = _cenaZaKus;

            using (var db = new SqliteContext()) {
                var polozky = db.Polozky.ToList();
                bool polozkaNalezena = false;
                foreach (Polozka polozka in polozky) {
                    if(polozka.Jmeno == Nazev)
                    {
                        odberatel.PolozkaId = polozka.Id;

                        // jestli je ndost polozek na rozdavani
                        if(polozka.Pocet < odberatel.pocetPolozekOdebrano)
                        {
                            MessageBox.Show($"snazite se odberat {odberatel.pocetPolozekOdebrano} ale ve skladu je jen {polozka.Pocet}, " +
                                $"Zmente pocet nebo pridejte vice polozek do skladu. ");
                            return;
                        }


                        polozkaNalezena = true;
                        break;
                    }
                }

                if (!polozkaNalezena)
                {
                    MessageBox.Show("Polozka nebyla nalezena");
                    return;
                }

                db.Odebiratele.Add(odberatel);
                db.SaveChanges();
            }

            NavigationService.Navigate(new ZaznamOdebrateluView());

        }
    }
}
