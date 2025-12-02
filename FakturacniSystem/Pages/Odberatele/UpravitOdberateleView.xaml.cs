using FakturacniSystem.Code;
using FakturacniSystem.Code.Slouceni;
using FakturacniSystem.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interakční logika pro UpravitOdberateleView.xaml
    /// </summary>
    public partial class UpravitOdberateleView : Page, INotifyPropertyChanged
    {
        OdebiraniSlouceno odebiraniSlouceno;
        public UpravitOdberateleView(OdebiraniSlouceno slouceno)
        {
            odebiraniSlouceno = slouceno;
            InitializeComponent();
            SetStartValues();
            this.DataContext = this;
        }

        public void SetStartValues()
        {
            Nazev = odebiraniSlouceno.polozka.Jmeno;
            _odebranyPocet = odebiraniSlouceno.odebirani.pocetPolozekOdebrano;
            _cenaZaKus = odebiraniSlouceno.odebirani.CenaZaKus;
            _id = odebiraniSlouceno.polozka.Id;
            NazevOdberatele = odebiraniSlouceno.odebirani.NazevOdberatele;
        }

        public void SaveChangedValues()
        {
            using (var context = new SqliteContext())
            {
                List<OdebraniPolozka> odebirani = context.Odebiratele.ToList();
                for (int i = 0; i < odebirani.Count; i++) {
                    if(odebirani[i].Id == odebiraniSlouceno.odebirani.Id)
                    {
                        odebirani[i].NazevOdberatele = NazevOdberatele;
                        odebirani[i].pocetPolozekOdebrano = _odebranyPocet;
                        odebirani[i].CenaZaKus = _cenaZaKus;

                        if(odebiraniSlouceno.polozka.Jmeno != Nazev)
                        {
                            foreach (var polozka in context.Polozky.ToList())
                            {
                                if (polozka.Jmeno == Nazev)
                                {
                                    odebirani[i].PolozkaId = polozka.Id;
                                }
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
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
            SaveChangedValues();
            NavigationService.GoBack();


        }
        private void Zrusit(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}
