using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int AniState = 1;
        private Rectangle rec = new Rectangle();
        private List<List<Rectangle>> listOfRectangles = new List<List<Rectangle>>(GameManager.Columns);
        private List<List<Rectangle>> listOfNextRectangles = new List<List<Rectangle>>(4);
        private GameManager gm;

        static Random r = new Random();
        private Player me = new Player(100);
        private Player Dragon = new Player(30);

        public int[] enemyDamageLength = { 1, 10 };
        private double dragonLV = 0;

        public int[] EnemyDamageLength
        {
            get { return enemyDamageLength; }
            set { enemyDamageLength = value; }
        }
        public double DragonLV
        {
            get { return dragonLV; }
            set { dragonLV = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PlayerHPValue.Content = string.Format("{0}/{1}", PlayerHP.Value, PlayerHP.Maximum);
            EnemyHPValue.Content = string.Format("{0}/{1}", EnemyHP.Value, EnemyHP.Maximum);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(DispatTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            CreateGrid();
            NewGame();
        }

        private void DispatTick(object sender, EventArgs e)
        {
            if (AniState == 1)
            {
                Player.Source = new BitmapImage(new Uri(@"\res\2.png", UriKind.Relative));
                Enemy.Source = new BitmapImage(new Uri(@"\res\bot2.png", UriKind.Relative));
                AniState = 2;
            }

            else if (AniState == 2)
            {
                Player.Source = new BitmapImage(new Uri(@"\res\3.png", UriKind.Relative));
                Enemy.Source = new BitmapImage(new Uri(@"\res\bot3.png", UriKind.Relative));
                AniState = 3;
            }

            else if (AniState == 3)
            {
                Player.Source = new BitmapImage(new Uri(@"\res\4.png", UriKind.Relative));
                Enemy.Source = new BitmapImage(new Uri(@"\res\bot4.png", UriKind.Relative));
                AniState = 4;
            }

            else if (AniState == 4)
            {
                Player.Source = new BitmapImage(new Uri(@"\res\1.png", UriKind.Relative));
                Enemy.Source = new BitmapImage(new Uri(@"\res\bot1.png", UriKind.Relative));
                AniState = 1;
            }

            if(!gm.IsPuzzle)
            {
                Deal();
                gm.IsPuzzle = true;
                NewGame();
            }
        }

        private void AllDraw()
        {
            listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush(Colors.White); }));
            listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush(Colors.White); }));
            //  mainGrid.Children.Clear();
            gm.GetAllPoints().ForEach(p => { DrawOne(p.X, p.Y, p.Color); });
            if (!gm.IsEndOfGame) gm.MovingObject.Points.ForEach(p => { DrawOne(p.X, p.Y, p.Color); });
            DrawNext();

        }

        private void DrawOne(int x, int y, Color color)
        {
            listOfRectangles[y][x].Fill = new SolidColorBrush(color);

        }


        /// <summary>
        /// objekt ktery bude priste
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        private void DrawNext()
        {
            List<GameLogic.Point> list = gm.NextMovingObject.Points;

            int minX = list.Min(p => p.X);
            int minY = list.Min(p => p.Y);
            int maxX = list.Max(p => p.X);
            int maxY = list.Max(p => p.Y);
            int coefX = minX;
            int coefY = 0;

            if ((maxX - minX) < 3) coefX -= 1;
            if ((maxY - minY) < 3) coefY += 1;

            //   List<GameLogic.Point> resList = new List<GameLogic.Point>(list.Count);
            //    list.CopyTo(resList.ToArray());
            //   list.cop

            //   resList.ForEach(p => { p.X -= minX; p.Y -= minY; });

            foreach (GameLogic.Point point in list)
            {
                listOfNextRectangles[point.Y + coefY][point.X - coefX].Fill = new SolidColorBrush(point.Color);
            }


        }

        private void NewGame()
        {
            //txtLabel.Text = "";
            txtScore.Text = "0";
            gm = null;
            gm = new GameManager();
            gm.Counter = 5;
            gm.Start();
            AllDraw();
            gm.MoveDownByThread += gm_MoveDownByThread;
        }

        void gm_MoveDownByThread()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                KeyDownMethod(Key.S);
            }));
        }

        private void CreateGrid()
        {
            for (int i = 0; i < GameManager.Columns; i++)
            {
                P1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < GameManager.Rows; i++)
            {
                P1Grid.RowDefinitions.Add(new RowDefinition());
            }


            for (int j = 0; j < GameManager.Rows; j++)
            {
                listOfRectangles.Add(new List<Rectangle>());
                for (int i = 0; i < GameManager.Columns; i++)
                {

                    rec = new Rectangle();
                    rec.Stretch = Stretch.Fill;
                    Thickness ts = new Thickness(1);
                    rec.Margin = ts;
                    rec.Fill = new SolidColorBrush(Colors.White);
                    Grid.SetColumn(rec, i);
                    Grid.SetRow(rec, j);
                    P1Grid.Children.Add(rec);

                    listOfRectangles[j].Add(rec);
                }
            }

            for (int j = 0; j < 4; j++)
            {
                listOfNextRectangles.Add(new List<Rectangle>());
                for (int i = 0; i < 4; i++)
                {
                    rec = new Rectangle();
                    rec.Stretch = Stretch.Fill;
                    Thickness ts = new Thickness(1);
                    rec.Margin = ts;
                    rec.Fill = new SolidColorBrush(Colors.White);
                    Grid.SetColumn(rec, i);
                    Grid.SetRow(rec, j);
                    gridNext.Children.Add(rec);

                    listOfNextRectangles[j].Add(rec);
                }
            }

        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownMethod(e.Key);
        }

        private void KeyDownMethod(Key key)
        {
            if (gm.IsEndOfGame)
            {
                //AllDraw();
                return;
            }

            else if (!gm.IsPuzzle)
            {
                gm.EndRunningGame();
                me.setDamage(gm.Score);
            }

            switch (key)
            {
                case Key.A:
                    {
                        gm.MoveToLeft();
                        break;
                    }
                case Key.D:
                    {
                        gm.MoveToRight();
                        break;
                    }
                case Key.S:
                    {
                        gm.MoveToDown();
                        break;
                    }
                case Key.W:
                    {
                        gm.Rotate();
                        break;
                    }
                case Key.Space:
                    {
                        Deal();
                        break;
                    }

            }
            AllDraw();
            txtScore.Text = gm.Score.ToString();
            txtTimer.Text = gm.Counter.ToString();
        }

        public void Deal()
        {
            Dragon.setDamage(r.Next(enemyDamageLength[0], enemyDamageLength[1]));
            if (me.Damage > Dragon.Damage)
            {
                EnemyHP.Value -= me.Damage;
                EnemyHPValue.Content = string.Format("{0}/{1}", EnemyHP.Value, EnemyHP.Maximum);
                PlayerHP.Value -= Dragon.Damage;
                PlayerHPValue.Content = string.Format("{0}/{1}", PlayerHP.Value, PlayerHP.Maximum);
            }

            else
            {
                PlayerHP.Value -= Dragon.Damage;
                PlayerHPValue.Content = string.Format("{0}/{1}", PlayerHP.Value, PlayerHP.Maximum);
                EnemyHP.Value -= me.Damage;
                EnemyHPValue.Content = string.Format("{0}/{1}", EnemyHP.Value, EnemyHP.Maximum);
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            gm.EndRunningGame();
            NewGame();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gm.EndRunningGame();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            P1Grid.Focus();
            if (gm.IsPaused)
            {
                gm.IsPaused = false;
                btnPause.Content = "Pause";
                gm.timer.Start();
            }
            else
            {
                gm.IsPaused = true;
                btnPause.Content = "Resume";
                gm.timer.Stop();
            }
        }

    }
}
