using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewLiftModeling
{
    public class Building
    {
        public List<Level> Levels { get; set; }
        public Lift Lift { get; set; }
        public List<Person> People { get; set; }
        public int PersonCounter { get; set; }

        Random rnd = new Random();


        public Building(int levelsNumber)
        {
            Levels = new List<Level>();
            Lift = new Lift();

            for (int i = 0; i < levelsNumber; i++)
                Levels.Add(new Level(this, i, false));
            Levels.ElementAt<Level>(Settings.LIFT_STARTING_LEVEL).IsLiftPresent = true;
            for (int i = 0; i < levelsNumber; i++)
                Levels.ElementAt<Level>(i).Lift = Lift;

            Lift.CurrentLevel = Levels.ElementAt<Level>(Settings.LIFT_STARTING_LEVEL);
            Lift.Levels = Levels;

            People = new List<Person>();


        }



        public Person SpawnPerson()
        {
            int currentLevelNumber =  rnd.Next(0, Levels.Count);
            int targetLevelNumber = 0;
            Level currentLevel = Levels.ElementAt<Level>(currentLevelNumber);
            Person p = new Person(PersonCounter++, this, currentLevel, targetLevelNumber);
            Levels.ElementAt<Level>(currentLevelNumber).Queue.Enqueue(p);
            p.QueueNumber = Levels.ElementAt<Level>(currentLevelNumber).Queue.Count;
            People.Add(p);
            if (!p.CurrentLevel.IsLiftSummonButtonPushed)
            {
                p.CurrentLevel.IsLiftSummonButtonPushed = true;
            }
            if (PersonSpawned != null)
                PersonSpawned(this, new PersonSpawnedEventArgs(p));
            Lift.Move1();
            return p;
        }


        public delegate void PersonSpawnedEventHandler(object sender, PersonSpawnedEventArgs e);

        public event PersonSpawnedEventHandler PersonSpawned;



    }
}
