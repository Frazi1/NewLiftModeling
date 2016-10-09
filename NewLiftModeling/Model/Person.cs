using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewLiftModeling
{
    public class Person
    {
        public int ID { get; set; }
        public int TargetLevelNumber { get; set; }
        public Level CurrentLevel { get; set; }
        public Building Building { get; set; }
        public bool IsInLift { get; set; }
        public int QueueNumber { get; set; }



        public Person(int id, Building building, Level currentLevel, int targetLevelNumber)
        {
            ID = id;
            TargetLevelNumber = targetLevelNumber;
            CurrentLevel = currentLevel;
            IsInLift = false;
            Building = building;
        }
        public Person(int id, Building building, Level currentLevel, int targetLevelNumber, bool isInLift)
        {
            ID = id;
            TargetLevelNumber = targetLevelNumber;
            CurrentLevel = currentLevel;
            IsInLift = false;
            Building = building;
            IsInLift = isInLift;
        }

    }
}
