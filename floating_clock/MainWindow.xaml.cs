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

            // create default preference
            preference = new Preference();

            // TODO: start timer and update every ~1
            // set time
            string time = DateTime.Now.ToString("h:mm:ss tt");
            this.timerLabel.Content = time;

            // set window to allows be on top
            this.Topmost = true;

            preferenceDelegate += new PreferenceDelegate(on_SetSavedPreference);

            //  DispatcherTimer setup
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
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
           // Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

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
    }
}
