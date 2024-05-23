using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Haal de huidige dag van de week op
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos/"))
            {
                // Split de mapnaam om de dag van de week te krijgen
                string dirName = new DirectoryInfo(dir).Name;
                string[] dirParts = dirName.Split('_');

                // Check of de eerste deel van de mapnaam overeenkomt met de huidige dag van de week
                if (int.TryParse(dirParts[0], out int dirDay) && dirDay == day)
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        // Voeg de foto toe aan de lijst
                        PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Herlaad de foto's van vandaag
            PicturesToDisplay.Clear();
            Start();
        }
    }
}
