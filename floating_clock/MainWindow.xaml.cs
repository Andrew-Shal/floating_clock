using System;
using System.Collections.Generic;
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

using System.Windows.Threading;

namespace floating_clock
{
    // create delegate to pass data
    public delegate void PreferenceDelegate(Preference preference);



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // timer
        private DispatcherTimer dispatcherTimer;

        private Preference preference;

        // delegate to be used
        public PreferenceDelegate preferenceDelegate;
        
        public MainWindow()
        {
            InitializeComponent();

            // move in separate func
            try
            {
                // add in try/catch bcz we cant gurantee setting will load correct from file
                // get preferences from settings

                string fColor = Properties.Settings.Default.fontColor;
                string fFamily = Properties.Settings.Default.fontFamily;
                double fSize = Properties.Settings.Default.fontSize;
                bool is12Format = Properties.Settings.Default.Is12HrFormat;


                // parse color string format "0,0,0" to Color obj
                var colorArrRGB = fColor.Split(',');

                // TODO rethink this
                // rgb
                byte r = Convert.ToByte(colorArrRGB[0]);
                byte g = Convert.ToByte(colorArrRGB[1]);
                byte b = Convert.ToByte(colorArrRGB[2]);

                // create color from rgb
                var color = Color.FromRgb(r, g, b);

                // create default preference
                // should load with defaults
                preference = new Preference(color, new FontFamily(fFamily), fSize, is12Format);
            }
            catch (Exception e) {
                // fallback to default
                preference = new Preference();
            }

            // TODO: start timer and update every ~1
            // set time
            string time = DateTime.Now.ToString("h:mm:ss tt");
            this.timerLabel.Content = time;

            // set window to allows be on top
            this.Topmost = true;
            this.ResizeMode = ResizeMode.NoResize;

            preferenceDelegate += new PreferenceDelegate(on_SetSavedPreference);

            //  DispatcherTimer setup
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            // set ui to saved preference
            updateUI();
        }

        private void on_SetSavedPreference(Preference preferenceData)
        {
            // update preference obj
            preference = preferenceData;
            updateUI();
        }

        private void updateUI()
        {
            // set ui data
            this.timerLabel.Foreground = new SolidColorBrush(preference.FontColor);
            this.timerLabel.FontFamily = preference.FontFamily;
            this.timerLabel.FontSize = preference.FontSize;
            
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // TODO : move over to separate class/file
            // update ui text element that displays the time
            this.timerLabel.Content = preference.Is12HrFormat ? DateTime.Now.ToString("h:mm:ss tt") : DateTime.Now.ToString("HH:mm:ss");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // allow window to be draggable
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void PreferenceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // pass delegate here

            // open preference window dialog
            PreferenceDialog preferenceDialog = new PreferenceDialog(preferenceDelegate, preference);
            preferenceDialog.Owner = this;
            preferenceDialog.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // convert font color to string r,g,b
            string color = $"{preference.FontColor.R},{preference.FontColor.G},{preference.FontColor.B}";

            // convert font family to string
            string family = preference.FontFamily.ToString();

            // save settings
            Properties.Settings.Default.Is12HrFormat = preference.Is12HrFormat;
            Properties.Settings.Default.fontColor = color;
            Properties.Settings.Default.fontSize = preference.FontSize;
            Properties.Settings.Default.fontFamily = family;

            // persist data
            Properties.Settings.Default.Save();

        }
    }
}
