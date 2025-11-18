using FakturacniSystem.Code.Slouceni;
using FakturacniSystem.Migrations;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.UniversalAccessibility.Drawing;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FakturacniSystem.Code
{
    public class FakturyCreator
    {
        XGraphics grafika;
        PdfDocument pdf;
        OdebiraniSlouceno odebrani;

        public FakturyCreator(OdebiraniSlouceno odebrani)
        {
            this.odebrani = odebrani;
        }
        public void CreatePDF()
        {
            pdf = new PdfSharp.Pdf.PdfDocument();
            var page = pdf.AddPage();
            grafika = XGraphics.FromPdfPage(page);

            AddText(new XPoint(90, 50), "FAKTURA", 20);
            AddText(new XPoint(page.Width - 100, 50), $"{DateTime.Today}", 10);

            AddLine(new XPoint(50, 80), new XPoint(page.Width - 50, 80));

            AddText(new XPoint(100, 100), "Dodavatel", 15);
            AddText(new XPoint(130, 130), "Nazev: Lukasuv Sklad s.r.o.", 10);





            AddText(new XPoint(350, 100), "Odberatel", 15);
            AddText(new XPoint(380, 130), $"Nazev: {odebrani.odebirani.NazevOdberatele}", 10);

            AddText(new XPoint(160, 170), $"Nazev Polozky = {odebrani.polozka.Jmeno}, Id = {odebrani.polozka.Id}", 10);
            AddText(new XPoint(160, 190), $"Pocet kusu odebranych = {odebrani.odebirani.pocetPolozekOdebrano}", 10);

            AddText(new XPoint(160, 210), $"Cena za kus: {odebrani.odebirani.CenaZaKus}kc", 10);
            AddText(new XPoint(300, 280), $"Cena za objednavku: {odebrani.odebirani.CenaZaKus * odebrani.odebirani.pocetPolozekOdebrano}kc", 10);


            QRCreate();
            AddText(new XPoint(430, 530), $"QR platba {odebrani.odebirani.CenaZaKus * odebrani.odebirani.pocetPolozekOdebrano}kc", 10);


            SaveFile();
            
        }

        public void SaveFile()
        {
            var fileName = $"{odebrani.odebirani.NazevOdberatele} + {odebrani.polozka.Jmeno} + {odebrani.odebirani.pocetPolozekOdebrano}.pdf";
            pdf.Save(fileName);
            
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true // Required to open with default app
                }
            };
            process.Start();


        }

        public void AddText(XPoint point, string content, int size)
        {
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;

            var font = new XFont("Arial", size, XFontStyleEx.Bold);
            grafika.DrawString(content, font, XBrushes.Black, point, XStringFormats.Center);
        }

        public void AddLine(XPoint from, XPoint to)
        {
            grafika.DrawLine(new XPen(XColors.Black), from, to);
        }

        public void QRCreate()
        {
            string qrText = "0/0000";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using Bitmap qrBitmap = qrCode.GetGraphic(20);

            MemoryStream qrStream = new MemoryStream();
            qrBitmap.Save(qrStream, System.Drawing.Imaging.ImageFormat.Png);
            qrStream.Position = 0;

            XImage qrImage = XImage.FromStream(qrStream);
            grafika.DrawImage(qrImage, 300,300, 250, 250);

        }


    }
}
