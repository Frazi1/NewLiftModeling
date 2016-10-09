using System;
namespace NewLiftModeling
{
    public class LiftMovedEventArgs : EventArgs
    {
        Level CurrentLevel { get; set; }
        public LiftMovedEventArgs(Level currentLevel)
        {
            CurrentLevel = currentLevel;
        }
    }
}