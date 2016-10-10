using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewLiftModeling
{
    public class LevelModel
    {
        public Level Level { get; set; }

        public double Width { get; set; }
        public double Heigth { get; set; }

        public LevelModel(Level level)
        {
            Level = level;
            Width = Settings.LevelWidth;
            Heigth = Settings.LevelHeigth;
        }
    }
}
