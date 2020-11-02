using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using System.Drawing.Text;


namespace floating_clock
{
    public class Preference
    {
        public Color FontColor { get; set; }
        public FontFamily FontFamily { get; set; }
        public double FontSize { get; set; }
        public bool Is12HrFormat { get; set; }
        public Color BackgroundColor{get; set;}

        public Preference()
        {
            // implement 
            // defaults

            // rgb - 000 - black i think
            FontColor = Color.FromArgb(255,0, 0, 0);
            FontFamily = new FontFamily("Calibri");
            FontSize = 18;
            Is12HrFormat = true;
            BackgroundColor = Color.FromArgb(100, 0, 0, 0);
        }
        public Preference(Color foreground, Color background, FontFamily fontFamily, double fontSize, bool is12HrFormat)
        {
            FontColor = foreground;
            BackgroundColor = background;
            FontFamily = fontFamily;
            FontSize = fontSize;
            Is12HrFormat = is12HrFormat;
        }
    }
}
