using FakturacniSystem.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FakturacniSystem.EF
{
    public class ManageSkladCount
    {
        public static void ReloadSkladCount()
        {
            using (var db = new SqliteContext())
            {
                var sklad = db.Polozky.ToList();
                var dodavani = db.Dodavatele.ToList();
                var odebirani = db.Odebiratele.ToList();

                for(int i = 0; i < sklad.Count; i++)
                {
                    bool necoNalezeno = false;
                    int pocet = 0;

                    for(int j = 0; j < dodavani.Count; j++)
                    {
                        if (sklad[i].Id == dodavani[j].PolozkaId)
                        {
                            pocet += dodavani[j].pocetPolozekPridano;
                            necoNalezeno = true;
                        }
                        

                    }

                    for (int j = 0; j < odebirani.Count; j++)
                    {
                        if (sklad[i].Id == odebirani[j].PolozkaId)
                        {
                            pocet -= odebirani[j].pocetPolozekOdebrano;
                            necoNalezeno = true;
                        }

                    }

                    if (necoNalezeno) {
                        sklad[i].Pocet = pocet;
                    }
                    else
                    {
                        sklad[i].Pocet = 0;

                    }



                }
                db.SaveChanges();

            }
        }
    }
}
