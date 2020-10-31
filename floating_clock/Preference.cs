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
        public Preference()
        {
            // implement 
            // defaults

            // rgb - 000 - black i think
            FontColor = Color.FromRgb(0, 0, 0);

            FontFamily = new FontFamily("Calibri");

            FontSize = 18;
            Is12HrFormat = true;
        }
        public Preference(Color colorName, FontFamily fontFamily, double fontSize, bool is12HrFormat)
        {
            FontColor = colorName;
            FontFamily = fontFamily;
            FontSize = fontSize;
            Is12HrFormat = is12HrFormat;
        }
    }
}
