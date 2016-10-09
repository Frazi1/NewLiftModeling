using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace NewLiftModeling
{
    public class Lift
    {
        //vars
        public Level CurrentLevel { get; set; }
        public List<Person> People { get; set; }
        public List<Level> Levels { get; set; }
        public Queue<Level> LevelsToVisit
        {
            get { return levelsToVisit; }
            set
            {
                levelsToVisit = value;
 
            }
        }
        public int Speed { get; set; } //Время, за которое лифт проезжает один(1) этаж 
        public int Capacity { get; set; }
        public int TargetLevelNumber { get; set; }

        private DispatcherTimer LiftTimer;
        private Queue<Level> levelsToVisit;
        //
        //Constructors
        public Lift(Level startingLevel, List<Level> levels)
        {
            CurrentLevel = startingLevel;
            Capacity = Settings.LIFT_CAPACITY;
            People = new List<Person>();
            Levels = levels;
            Speed = Settings.LIFT_SPEED;
            LevelsToVisit = new Queue<Level>();
        }
        public Lift()
        {
            Capacity = Settings.LIFT_CAPACITY;
            People = new List<Person>();
            Speed = Settings.LIFT_SPEED;
            LevelsToVisit = new Queue<Level>();

            LiftTimer = new DispatcherTimer();
            LiftTimer.Interval = TimeSpan.FromSeconds(Settings.LIFT_SPEED);
            LiftTimer.Tick += LiftTimer_Tick;
            LiftTimer.Start();
        }


        public void Move()
        {

            if (LevelsToVisit.Count != 0)
            {
                TargetLevelNumber = LevelsToVisit.Dequeue().LevelNumber;
                
                //while (CurrentLevel.LevelNumber != TargetLevelNumber)
                //{
                    MoveTo(TargetLevelNumber);


                //}
            }

        }

        public void MoveTo(int targetLevelNumber)
        {

            if (CurrentLevel.LevelNumber < targetLevelNumber)
            {
                while (CurrentLevel.LevelNumber != targetLevelNumber)
                {
                    this.CurrentLevel = Levels[CurrentLevel.LevelNumber + 1];
                    if (LiftMoved != null)
                        LiftMoved(this, new LiftMovedEventArgs(CurrentLevel));
                }
            }
            else if (CurrentLevel.LevelNumber > targetLevelNumber)
            {
                while (CurrentLevel.LevelNumber != targetLevelNumber)
                {
                    this.CurrentLevel = Levels[CurrentLevel.LevelNumber - 1];
                    if (LiftMoved != null)
                        LiftMoved(this, new LiftMovedEventArgs(CurrentLevel));
                    if (CurrentLevel.IsLiftSummonButtonPushed && People.Count < Capacity)
                        TakePeople();
                }
            }


        }

        private void TakePeople()
        {
            Person p = CurrentLevel.Queue.Dequeue();
            p.IsInLift = true;
            p.QueueNumber = People.Count;
            People.Add(p);
            if (PersonMoved != null)
                PersonMoved(this, new PersonMovedEventArgs(p));
        }
        private void GetPeopleOut()
        {
            List<Person> PeopleToRemove = People.FindAll(e => e.CurrentLevel.LevelNumber == CurrentLevel.LevelNumber);
            People.RemoveAll(e => e.CurrentLevel.LevelNumber == CurrentLevel.LevelNumber);
            CurrentLevel.JustPeople.AddRange(PeopleToRemove);
            foreach (var p in PeopleToRemove)
                if (PersonMoved != null)
                    PersonMoved(this, new PersonMovedEventArgs(p));

        }



        private void LiftTimer_Tick(object sender, EventArgs e)
        {
            Move();
        }


        public delegate void LiftMovedEventHandler(object sender, LiftMovedEventArgs e);
        public delegate void PersonMovedEventHandler(object sender, PersonMovedEventArgs e);

        public event LiftMovedEventHandler LiftMoved;
        public event PersonMovedEventHandler PersonMoved;










    }

}
