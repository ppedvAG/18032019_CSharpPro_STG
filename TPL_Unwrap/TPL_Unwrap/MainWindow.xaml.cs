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

namespace TPL_Unwrap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click_Blue(object sender, RoutedEventArgs e)
        {
            laserZielControl.BringBlueToCenterAsync(10);

        }

        private void Button_Click_Green(object sender, RoutedEventArgs e)
        {
            laserZielControl.BringGreenToCenterAsync(10);

        }

        private async void Button_Click_Both(object sender, RoutedEventArgs e)
        {

            try
            {
                //keine Exceptions!
                //await laserZielControl.BringBlueToCenterAsync(10).ContinueWith(async t => { if (t.Result) await laserZielControl.BringGreenToCenterAsync(10); });

                //mit exception!
                await laserZielControl.BringBlueToCenterAsync(10).ContinueWith(async t => { if (t.Result) await laserZielControl.BringGreenToCenterAsync(10); }).Unwrap();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} {ex.InnerException?.Message}");
            }

        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            laserZielControl.ResetPos();
        }
    }
}
