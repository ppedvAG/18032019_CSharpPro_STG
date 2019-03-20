using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HalloAsync
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

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                pb1.Value = i;
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            b.IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    //pb1.Value = i;
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                }

                b.Dispatcher.Invoke(() => b.IsEnabled = !false);
            });
        }

        private void StartTaskTs(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            b.IsEnabled = false;

            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            cts = new CancellationTokenSource();

            var t = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (cts.IsCancellationRequested)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                    }

                    if (i > 77)
                        throw new EncoderFallbackException();
                    Thread.Sleep(100);
                    int lokalesI = i;
                    Task.Factory.StartNew(() => pb1.Value = i, cts.Token, TaskCreationOptions.None, ts);
                }

                b.Dispatcher.Invoke(() => b.IsEnabled = !false);
            });

            t.ContinueWith(tt =>
                  {
                      if (tt.Exception.InnerExceptions.Any(x => x is OperationCanceledException))
                          MessageBox.Show("Erfolgreich abgebrochen!");
                      else
                          MessageBox.Show($"FEHLER: {tt.Exception.InnerException.Message}");
                  }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);


            t.ContinueWith(tt => MessageBox.Show("Alles OK"), TaskContinuationOptions.OnlyOnRanToCompletion);
            t.ContinueWith(tt => MessageBox.Show("Fertig"));
        }

        CancellationTokenSource cts = null;

        private void Abort(object sender, RoutedEventArgs e)
        {
            //if (cts != null)
            cts?.Cancel();
        }

        private async void StartAsync(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100);
                pb1.Value = i;
            }
        }

        int zzz = 1;
        public long CalcTheWorld(string txt)
        {
            Thread.Sleep(5000);
            return txt.Length * 957 + zzz++;
        }

        public Task<long> CalcTheWorldAsync(string txt, CancellationToken c)
        {
            return Task.Run(() => CalcTheWorld(txt), c);
        }

        private async void StartCalc(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();


            MessageBox.Show((await CalcTheWorldAsync("Hallo", cts.Token)).ToString());

            //Task.Run(() => CalcTheWorld("Hallo")).ContinueWith(t => MessageBox.Show(t.Result.ToString()));
        }

        private void StartStartCalc(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                StartCalc(sender, e);
            }
            MessageBox.Show("!!!!");
        }

        private async void StartNested(object sender, RoutedEventArgs e)
        {

            try
            {
                await Task.Run<Task<long>>(() =>
                {
                    Task.Run(() => { Thread.Sleep(1000); Dispatcher.Invoke(() => Background = Brushes.Red); });
                    Task.Run(() => { Thread.Sleep(1500); Dispatcher.Invoke(() => Background = Brushes.Yellow); });
                    Task.Run(() => { Thread.Sleep(2000); Dispatcher.Invoke(() => Background = Brushes.Green); });

                    //Task.Run(() =>
                    //{
                    //    Task.Run(() => { Thread.Sleep(2500); throw new ArgumentOutOfRangeException(); });
                    //});
                    //return 843874398743;


                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
