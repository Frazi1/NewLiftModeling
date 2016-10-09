using System;

namespace NewLiftModeling
{
    public class PersonSpawnedEventArgs : EventArgs
    {
        public Person Person { get; set; }
        public PersonSpawnedEventArgs(Person p)
        {
            Person = p;
        }
    }
}