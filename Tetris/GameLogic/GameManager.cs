using GameLogic.TetrisObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GameLogic
{
    public class GameManager
    {

        private List<Point> listOfAllPoints = new List<Point>();
        private IObject movingObject;
        private IObject nextMovingObject;
        private bool isEndOfGame = false;
        private bool isPuzzle = true;
        private Thread th;
        private Random ran = new Random();
        private int score = 0;
        private int timeOut = 1000;
        private int counter;


        public static int Columns { get { return 10; } }
        public static int Rows { get { return 20; } }
        public BaseShapeObject MovingObject { get { return (BaseShapeObject)movingObject; } }
        public BaseShapeObject NextMovingObject { get { return (BaseShapeObject)nextMovingObject; } }
        public int Score { get { return score; } }
        public bool IsEndOfGame { get { return isEndOfGame; } }
        public bool IsPuzzle
        {
            get { return isPuzzle; }
            set { isPuzzle = value; }
        }
        public bool IsPaused { get; set; }
        public int Counter {
            get { return counter; }
            set { counter = value; }
        }
        public DispatcherTimer timer = new DispatcherTimer();

        public delegate void MoveDownByThHandler();
        public event MoveDownByThHandler MoveDownByThread;


        /// <summary>
        /// spusti hru
        /// </summary>
        public void Start()
        {
            AddNewObject();
            th = new Thread(MoveDownByTh);
            th.IsBackground = false;
            th.Start();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1); // 1 second
            timer.Start();
        }

        

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Counter != 0) Counter--;
            else
            {
                timer.Stop();
                TurnEnd();
            }
            
        }

        public void TurnEnd()
        {
            isPuzzle = false;
        }

        private void MoveDownByTh()
        {
            while (true)
            {
                if (isEndOfGame) th.Abort();
                if (!IsPaused) if (MoveDownByThread != null) MoveDownByThread();
                Thread.Sleep(timeOut);
            }
        }


        /// <summary>
        /// prida novy objekt do hry
        /// </summary>
        public void AddNewObject()
        {
            if (movingObject != null) listOfAllPoints.AddRange(((BaseShapeObject)movingObject).Points);
            CheckRows();
            if (nextMovingObject == null)
            {
                CreateNewNextObject();
            }
            movingObject = nextMovingObject;
            if (((BaseShapeObject)movingObject).Collision(GetAllPoints()))
            {
                isEndOfGame = true;
                ShowGameOver();
            }
            CreateNewNextObject();
        }



        /// <summary>
        /// vytvori novy objekt ktery bude nasledovat po aktualnim
        /// </summary>
        private void CreateNewNextObject()
        {
            nextMovingObject = GetRandomObject();
            nextMovingObject.Create(Columns / 2, 0);
        }


        private IObject GetRandomObject()
        {
            switch (ran.Next(0, 7))
            {
                case 0:
                    {
                        return new ObjectO();
                    }
                case 1:
                    {
                        return new ObjectI();
                    }
                case 2:
                    {
                        return new ObjectL();
                    }
                case 3:
                    {
                        return new ObjectZ();
                    }
                case 4:
                    {
                        return new ObjectT();
                    }
                case 5:
                    {
                        return new ObjectJ();
                    }
                case 6:
                    {
                        return new ObjectS();
                    }
            }
            return new ObjectO();
        }


        /// <summary>
        /// zkontroluji se radky, jestli se nejake nemaji zrusit + bodove ohodnoceni
        /// </summary>
        private void CheckRows()
        {
            int delRows = 0;
            List<Point> pointsToDel = new List<Point>();
            for (int i = 0; i < GameManager.Rows; i++)
            {
                pointsToDel = listOfAllPoints.Where(p => p.Y == i).ToList();
                if (pointsToDel.Count() >= GameManager.Columns)
                {
                    pointsToDel.ForEach(p => listOfAllPoints.Remove(p));
                    listOfAllPoints.ForEach(p => { if (p.Y < i) p.Y++; });
                    delRows++;
                }
            }
            if (delRows > 0)
            {
                switch (delRows)
                {
                    case 1:
                        score += delRows;
                        break;
                    case 2:
                        score += delRows * 3/2;
                        break;
                    case 3:
                        score += delRows * 2;
                        break;
                    case 4:
                        score += delRows * 5/2;
                        break;
                }

            }

            
        }


        /// <summary>
        /// všechny nepohybující se body
        /// </summary>
        /// <returns></returns>
        public List<Point> GetAllPoints()
        {
            return listOfAllPoints;
        }

        #region pohyby


        /// <summary>
        /// posunutí objektu doprava
        /// </summary>
        public void MoveToRight()
        {
            if (IsPaused) return;
            MovingObject.MoveToRight(GetAllPoints());

        }
        /// <summary>
        /// posunutí objektu doleva
        /// </summary>
        public void MoveToLeft()
        {
            if (IsPaused) return;
            MovingObject.MoveToLeft(GetAllPoints());
        }

        /// <summary>
        /// posunutí objektu dolu
        /// </summary>
        public void MoveToDown()
        {
            if (IsPaused) return;
            if (!MovingObject.MoveDown(GetAllPoints()))
            {
                AddNewObject();
            }
        }

        /// <summary>
        /// rotace objektu
        /// </summary>
        public void Rotate()
        {
            if (IsPaused) return;
            MovingObject.Rotate(GetAllPoints());
        }

        #endregion

        public void EndRunningGame()
        {
            th.Abort();
        }

        /// <summary>
        /// vytvori napis game over v hraci plose
        /// </summary>
        public void ShowGameOver()
        {
            ObjectGO ogo = new ObjectGO();
            ogo.Create(Columns / 2, 0);
            //   listOfAllPoints.AddRange();
            List<int> indexes = new List<int>();
            foreach (Point point in ((BaseShapeObject)(ogo)).Points)
            {
                Point pp = listOfAllPoints.FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
                if (pp != null) listOfAllPoints.Remove(pp);
                listOfAllPoints.Add(point);

                // indexes.AddRange((listOfAllPoints.IndexOf(p => p.X == point.X && p.Y == point.Y)));
            }
            //   listOfAllPoints.ForEach(p=>p.);
        }


    }
}
