using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturacniSystem.Code
{
    public class OdebraniZaznam
    {
        public int Id { get; set; }
        public string NazevOdberatele {  get; set; }

        public DateTime denOdebrano;
        public int PolozkaId { get; set; }

        public int pocetPolozekOdebrano { get; set; }
    }
}
