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
using System.Windows.Shapes;

using System.Drawing.Text;

namespace floating_clock
{
    /// <summary>
    /// Interaction logic for PreferenceDialog.xaml
    /// </summary>
    public partial class PreferenceDialog : Window
    {
        PreferenceDelegate preferenceDelegate;

        Preference preference;
        public PreferenceDialog(PreferenceDelegate del, Preference pref)
        {
            InitializeComponent();

            // set delegate
            preferenceDelegate = del;
            preference = pref;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            
            // get font names
            IList<string> fontNames = installedFontCollection.Families.Select(f => f.Name).ToList();
            PreferenceFontFamilyCombo.ItemsSource = fontNames;

            // set to defaults
            setUIPreference();
        }

        private void setUIPreference()
        {
            colorPickerControl.SelectedColor = preference.FontColor;
            PreferenceFontFamilyCombo.SelectedValue = preference.FontFamily.ToString();
            PreferenceFontSizeSlider.Value = preference.FontSize;
            if (preference.Is12HrFormat)
            {
                PreferenceTimeFormat12RadioBtn.IsChecked = true;
            }else{
                PreferenceTimeFormat24RadioBtn.IsChecked = true;
            }
        }

        private void PreferenceSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // get all save preferences
            Color selectedColor = (Color)colorPickerControl.SelectedColor;

            // create new prefeence obj
            Preference pref = new Preference();
            pref.FontColor = selectedColor;
            pref.FontFamily = new FontFamily(PreferenceFontFamilyCombo.SelectedValue.ToString());
            pref.FontSize = Math.Round(PreferenceFontSizeSlider.Value,2);

            

            // get time format
            bool is12TimeFormat = PreferenceTimeFormat12RadioBtn.IsChecked == true ? true : false;
            pref.Is12HrFormat = is12TimeFormat;

            // send data back to main window
            // invoke delegate
            preferenceDelegate.Invoke(pref);

            // close this window
            this.Close();
        }

        private void PreferenceFontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            // set font size text label
            this.PreferenceFontSizeLabel.Content = Math.Round(slider.Value,2);
        }
    }
}
