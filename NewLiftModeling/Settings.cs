using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace NewLiftModeling
{
    public static class Settings
    {
        public static int LevelsNumber=6;

        public static int LiftCapacity = 5;
        public static double LiftSpeed = 0.2;
        public static int LiftStartingLevel = 0;
        //public static double LIFT_DOORS_OPEN_TIME = 3;


        public static double PersonRadius = 10;

        public static double QueueGap = PersonRadius / 2;

        //Для рисования
        public static double LiftHeigth = 75;
        //public static double LIFT_WIDTH = 75;
        public static double LiftWidth = LiftCapacity * 3 * QueueGap;

        public static double LevelWidth = 350;
        public static double LevelHeigth = 75;

        public static double Margin = 10;


        //Цвета
        public static Color PersonColor = Colors.Blue;

    }
}
