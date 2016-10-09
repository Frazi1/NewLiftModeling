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
                //Move();
            }
        }
        public int Speed { get; set; } //Время, за которое лифт проезжает один(1) этаж 
        public int Capacity { get; set; }
        public int TargetLevelNumber
        {
            get { return targetLevelNumber; }
            set
            {
                targetLevelNumber = value;
                //Move();
            }
        }

        private DispatcherTimer LiftTimer;
        private Queue<Level> levelsToVisit;
        private int targetLevelNumber;
        private int trasnportToLevelNumber;

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
            targetLevelNumber = -1;

            LiftTimer = new DispatcherTimer();
            LiftTimer.Interval = TimeSpan.FromSeconds(Settings.LIFT_SPEED);
            LiftTimer.Tick += LiftTimer_Tick;
            LiftTimer.Start();

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
            targetLevelNumber = -1;

        }
        //public void Move()
        //{
        //    if (LevelsToVisit.Count != 0)
        //    {
        //        if (TargetLevelNumber == -1)
        //            TargetLevelNumber = LevelsToVisit.Dequeue().LevelNumber;
        //        MoveTo();
        //        //LiftTimer.Stop();
        //    }

        //}
        //public void MoveTo()
        //{
        //    if (TargetLevelNumber != -1)
        //    {
        //        if (CurrentLevel.LevelNumber < TargetLevelNumber)
        //        {
        //            this.CurrentLevel = Levels[CurrentLevel.LevelNumber + 1];
        //        }
        //        else if (CurrentLevel.LevelNumber > TargetLevelNumber)
        //        {
        //            this.CurrentLevel = Levels[CurrentLevel.LevelNumber - 1];
        //            if (CurrentLevel.IsLiftSummonButtonPushed && People.Count < Capacity)
        //                TakePeople();
        //        }
        //        else
        //        {
        //            if (CurrentLevel.LevelNumber == 0)
        //                GetPeopleOut();
        //            TargetLevelNumber = -1;
        //        }
        //        if (LiftMoved != null)
        //            LiftMoved(this, new LiftMovedEventArgs(CurrentLevel));
        //        foreach (Person p in People)
        //            PersonMoved(this, new PersonMovedEventArgs(p));
        //    }
        //}

        public void Move1()
        {
            if (TargetLevelNumber == -1)
            {
                if (LevelsToVisit.Count != 0)
                    TargetLevelNumber = LevelsToVisit.Dequeue().LevelNumber;

                
            }
            LiftTimer.Start();
        }

        private void TakePeople()
        {
            while (CurrentLevel.Queue.Count > 0 && People.Count < Capacity)
            {
                Person p = CurrentLevel.Queue.Dequeue();
                p.IsInLift = true;
                p.QueueNumber = People.Count;
                People.Add(p);
                if (PersonMoved != null)
                    PersonMoved(this, new PersonMovedEventArgs(p));
            }
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
            //MoveTo();
            if (TargetLevelNumber != -1)
            {
                if (CurrentLevel.LevelNumber < TargetLevelNumber)
                {
                    CurrentLevel.IsLiftPresent = false;
                    CurrentLevel = Levels[CurrentLevel.LevelNumber + 1];
                    CurrentLevel.IsLiftPresent = true;
                    LiftMoved(this, new LiftMovedEventArgs(CurrentLevel));
                    foreach (Person p in People)
                        PersonMoved(this, new PersonMovedEventArgs(p));
                }
                else if (CurrentLevel.LevelNumber > TargetLevelNumber)
                {
                    CurrentLevel.IsLiftPresent = false;
                    CurrentLevel = Levels[CurrentLevel.LevelNumber - 1];
                    CurrentLevel.IsLiftPresent = true;
                    LiftMoved(this, new LiftMovedEventArgs(CurrentLevel));
                    for (int i = 0; i < People.Count; i++)
                    {
                        Person p = People[i];
                        p.CurrentLevel = CurrentLevel;
                        PersonMoved(this, new PersonMovedEventArgs(p));
                    }
                }
                else if (CurrentLevel.LevelNumber == TargetLevelNumber)
                {
                    TakePeople();
                    if (CurrentLevel.LevelNumber != 0)
                    {
                        TargetLevelNumber = trasnportToLevelNumber;
                        Move1();
                    }
                    if (CurrentLevel.LevelNumber == 0)
                    {
                        GetPeopleOut();
                        TargetLevelNumber = -1;
                    }
                }
            }
        }


        public delegate void LiftMovedEventHandler(object sender, LiftMovedEventArgs e);
        public delegate void PersonMovedEventHandler(object sender, PersonMovedEventArgs e);

        public event LiftMovedEventHandler LiftMoved;
        public event PersonMovedEventHandler PersonMoved;










    }

}
