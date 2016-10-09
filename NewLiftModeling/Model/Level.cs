using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewLiftModeling
{
    public class Level
    {
        public int LevelNumber { get; set; }
        public Queue<Person> Queue { get; set; }
        public List<Person> JustPeople
        {
            get { return justPeople; }
            set
            {
                justPeople = value;
                if (justPeople.Count >= 4)
                    justPeople.Clear();
            }
        }
        public bool IsLiftPresent
        {
            get { return isLiftPresent; }
            set
            {
                isLiftPresent = value;
                if (value == true)
                    isLiftButtonPushed = false;
            }
        }
        public bool IsLiftSummonButtonPushed
        {
            get { return isLiftButtonPushed; }
            set
            {
                isLiftButtonPushed = value;
                Lift.LevelsToVisit.Enqueue(this);
               
            }
        }
        public Lift Lift { get; set; }
        public Building Building { get; set; }

        private bool isLiftButtonPushed;
        private bool isLiftPresent;

        private List<Person> justPeople;

        public Level(Building building, int levelNumber, bool isLiftPresent)
        {
            LevelNumber = levelNumber;
            Queue = new Queue<Person>();
            isLiftButtonPushed = false;
            Building = building;
            this.isLiftPresent = isLiftPresent;
            JustPeople = new List<Person>();
        }



    }
}
