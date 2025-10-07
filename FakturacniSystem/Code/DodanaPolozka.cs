using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturacniSystem.Code
{
    public class DodanaPolozka
    {
        public int Id { get; set; }
        public string NazevDodavatele {  get; set; }

        public DateTime denDodani;
        public int PolozkaId { get; set; }
    }
}
