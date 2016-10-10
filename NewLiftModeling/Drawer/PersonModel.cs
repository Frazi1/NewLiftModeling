using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewLiftModeling
{
    public class PersonModel
    {
        public Person Person { get; set; }
        public double Radius { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public PersonModel(Person person)
        {
            Person = person;
            Radius = Settings.PersonRadius;
        }
    }
}
