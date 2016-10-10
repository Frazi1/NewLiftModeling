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
        public static int LevelsNumber=7;

        public static int LIFT_CAPACITY = 10;
        public static double LIFT_SPEED = 0.5;
        public static int LIFT_STARTING_LEVEL = 0;
        public static double LIFT_DOORS_OPEN_TIME = 3;


        public static double PERSON_RADIUS = 10;

        public static double QUEUE_GAP = PERSON_RADIUS / 2;

        //Для рисования
        public static double LIFT_HEIGTH = 75;
        //public static double LIFT_WIDTH = 75;
        public static double LIFT_WIDTH = LIFT_CAPACITY * 3 * QUEUE_GAP;

        public static double LEVEL_WIDTH = 350;
        public static double LEVEL_HEIGTH = 75;

        public static double MARGIN = 10;


        //Цвета
        public static Color PERSON_COLOR = Colors.Blue;

    }
}
