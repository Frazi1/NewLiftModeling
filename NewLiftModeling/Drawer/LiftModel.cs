using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace NewLiftModeling
{
    public class LiftModel
    {
        public Lift Lift { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public double Width { get; set; }
        public double Heigth { get; set; }

        public SolidColorBrush Brush = new SolidColorBrush(Colors.Black);

        public LiftModel(Lift lift)
        {
            Lift = lift;
            Width = Settings.LIFT_WIDTH;
            Heigth = Settings.LIFT_HEIGTH;
        }

    }
}
