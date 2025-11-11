using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FakturacniSystem.Code.Slouceni;

namespace FakturacniSystem.Code
{
    public class FakturyCreator
    {
        public FakturyCreator() {
            
        }

        public void CreatePDF(OdebiraniSlouceno odebrani)
        {

            var pdf = new PdfSharp.Pdf.PdfDocument();
            var page = pdf.AddPage();

            var grafika = XGraphics.FromPdfPage(page);
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            var font = new XFont("Arial", 10, XFontStyleEx.Bold);
            string content = $"POLOZKA NAZEV = {odebrani.polozka.Jmeno}, Odebiratel = {odebrani.odebirani.NazevOdberatele}, Pocet kusu odebranych = {odebrani.odebirani.pocetPolozekOdebrano}";


            grafika.DrawString(content, font, XBrushes.Black, new XPoint(200, 200), XStringFormats.Center);

            var fileName = $"{odebrani.odebirani.NazevOdberatele} + {odebrani.polozka.Jmeno} + {odebrani.odebirani.pocetPolozekOdebrano}.pdf";
            pdf.Save(fileName);
        }
    }
}
