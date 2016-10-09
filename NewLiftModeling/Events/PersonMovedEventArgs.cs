using System;

namespace NewLiftModeling
{
    public class PersonMovedEventArgs: EventArgs
    {
        public Person Person { get; set; }
        public PersonMovedEventArgs(Person p)
        {
            Person = p;
        }
    }
}