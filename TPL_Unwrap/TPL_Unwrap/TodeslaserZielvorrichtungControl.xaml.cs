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
    /// Interaction logic for TodeslaserZielvorrichtungControl.xaml
    /// </summary>
    public partial class TodeslaserZielvorrichtungControl : UserControl
    {
        public TodeslaserZielvorrichtungControl()
        {
            InitializeComponent();
            r1.MouseUp += R_MouseUp;
            r2.MouseUp += R_MouseUp;

        }
        bool throwEx = false;
        private void R_MouseUp(object sender, MouseButtonEventArgs e)
        {
            throwEx = true;
        }

        public async Task<bool> BringBlueToCenterAsync(int delay)
        {
            return await BringToCenterAsync(delay, r1);
        }

        public async Task<bool> BringGreenToCenterAsync(int delay)
        {
            return await BringToCenterAsync(delay, r2);
        }

        private async Task<bool> BringToCenterAsync(int delay, FrameworkElement fe)
        {
            int speedX = 1;
            int speedY = 1;

            throwEx = false;


            if (GetXCenterOfElement(fe) > GetXCenterOfWin())
                speedX *= -1;

            while (GetXCenterOfWin() != GetXCenterOfElement(fe))
            {
                Dispatcher.Invoke(() =>
                {
                    var pos = Canvas.GetLeft(fe);
                    Canvas.SetLeft(fe, pos + speedX);

                    if (throwEx)
                        throw new InvalidTimeZoneException($"Do not touch this! {((Shape)fe).Stroke}");
                });
                await Task.Delay(delay);

            }



            if (GetYCenterOfElement(fe) > GetYCenterOfWin())
                speedY *= -1;
            while (GetYCenterOfWin() != GetYCenterOfElement(fe))
            {
                Dispatcher.Invoke(() =>
                {
                    var pos = Canvas.GetTop(fe);
                    Canvas.SetTop(fe, pos + speedY);

                    if (throwEx)
                        throw new InvalidTimeZoneException($"Do not touch this! {((Shape)fe).Stroke}");
                });
                await Task.Delay(delay);


            }
            return true;
        }


        internal void ResetPos()
        {
            var ran = new Random();
            Canvas.SetTop(r1, ran.NextDouble() * (ActualHeight - r1.ActualHeight));
            Canvas.SetLeft(r1, ran.NextDouble() * (ActualWidth - r1.ActualWidth));
            Canvas.SetTop(r2, ran.NextDouble() * (ActualHeight - r1.ActualHeight));
            Canvas.SetLeft(r2, ran.NextDouble() * (ActualWidth - r1.ActualWidth));
        }

        private int GetXCenterOfWin()
        {
            return (int)(this.ActualWidth / 2);
        }

        private int GetYCenterOfWin()
        {
            return (int)(this.ActualHeight / 2);
        }



        private static int GetXCenterOfElement(FrameworkElement fe)
        {
            return fe.Dispatcher.Invoke(() => (int)(Canvas.GetLeft(fe) + ((fe.ActualWidth / 2))));
        }


        private static int GetYCenterOfElement(FrameworkElement fe)
        {
            return fe.Dispatcher.Invoke(() => (int)(Canvas.GetTop(fe) + ((fe.ActualHeight / 2))));
        }
    }
}
