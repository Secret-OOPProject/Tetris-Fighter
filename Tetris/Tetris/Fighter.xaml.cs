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

namespace Tetris
{
    /// <summary>
    /// Interaction logic for Fighter.xaml
    /// </summary>
    public partial class Fighter : Page
    {
        public Fighter()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(DispatTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        int PAniState = 1;

        private void DispatTick(object sender, EventArgs e)
        {
            if (PAniState == 1)
            {
                Player1.Source = new BitmapImage(new Uri(@"\res\2.png", UriKind.Relative));
                Player2.Source = new BitmapImage(new Uri(@"\res\bot2.png", UriKind.Relative));
                PAniState = 2;
            }

            else if (PAniState == 2)
            {
                Player1.Source = new BitmapImage(new Uri(@"\res\3.png", UriKind.Relative));
                Player2.Source = new BitmapImage(new Uri(@"\res\bot3.png", UriKind.Relative));
                PAniState = 3;
            }

            else if (PAniState == 3)
            {
                Player1.Source = new BitmapImage(new Uri(@"\res\4.png", UriKind.Relative));
                Player2.Source = new BitmapImage(new Uri(@"\res\bot4.png", UriKind.Relative));
                PAniState = 4;
            }

            else if (PAniState == 4)
            {
                Player1.Source = new BitmapImage(new Uri(@"\res\1.png", UriKind.Relative));
                Player2.Source = new BitmapImage(new Uri(@"\res\bot1.png", UriKind.Relative));
                PAniState = 1;
            }
        }
    }
}
