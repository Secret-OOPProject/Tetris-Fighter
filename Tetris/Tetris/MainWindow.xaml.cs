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
        private Rectangle rec = new Rectangle();
        private List<List<Rectangle>> listOfRectangles = new List<List<Rectangle>>(GameManager.Columns);
        private List<List<Rectangle>> listOfNextRectangles = new List<List<Rectangle>>(4);
        private GameManager gm;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            CreateGrid();
            NewGame();
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
            gm.Counter = 60;
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

        #region tlacitka

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
                        
                        break;
                    }
            }
            AllDraw();
            txtScore.Text = gm.Score.ToString();
            txtTimer.Text = gm.Counter.ToString();
            if (gm.IsEndOfGame) MessageBox.Show("Turn End!!!");
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

            #endregion
        }

    }
}
