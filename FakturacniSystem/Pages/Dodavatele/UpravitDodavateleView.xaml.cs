using FakturacniSystem.Code;
using FakturacniSystem.Code.Slouceni;
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
    /// Interakční logika pro UpravitDodavateleView.xaml
    /// </summary>
    public partial class UpravitDodavateleView : Page, INotifyPropertyChanged
    {
        public DodavaniSlouceno Zaznam { get; set; }
        public Polozka polozkaNaView;

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
                    int parsnutyInt = InputHandler.ParseInputtedTextToInt(value);
                    if (parsnutyInt != -1)
                    {
                        _pridanyPocet = parsnutyInt;
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





        public UpravitDodavateleView(DodavaniSlouceno zaznam)
        {
            InitializeComponent();
            if (zaznam == null)
            {
                MessageBox.Show("vybrana polozka je null");
            }
            else
            {
                Zaznam = zaznam;


                Nazev = Zaznam.polozka.Jmeno;
                Id = Zaznam.dodavani.Id + "";
                NazevDodavatele = Zaznam.dodavani.NazevDodavatele;
                pridanyPocet = Zaznam.dodavani.pocetPolozekPridano + "";

                this.DataContext = this;

            }

            
        }

        private void PotrvditUpravu(object sender, RoutedEventArgs e)
        {
            using (var db = new SqliteContext())
            {
                var DbPolozky = db.Polozky.ToList();
                for (int i = 0; i < DbPolozky.Count; i++)
                {
                    if (DbPolozky[i].Id == Zaznam.polozka.Id)
                    {
                        DbPolozky[i].Jmeno = Nazev;


                    }
                }
                db.SaveChanges();


                var DbDodani = db.Dodavatele.ToList();
                for(int i = 0;i < DbDodani.Count; i++)
                {
                    if (DbDodani[i].Id == int.Parse(Id))
                    {
                        DbDodani[i].NazevDodavatele = NazevDodavatele;
                        DbDodani[i].pocetPolozekPridano = _pridanyPocet;
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
