using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewLiftModeling
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        Building building;
        Drawer drawer;
        public MainWindow()
        {
            InitializeComponent();
            building = new Building(5);
            drawer = new Drawer(canvas1, building);


            building.Lift.LiftMoved += Lift_LiftMoved;
            building.Lift.PersonMoved += Lift_PersonMoved;
            building.PersonSpawned += Building_PersonSpawned;
        }

        private void Building_PersonSpawned(object sender, PersonSpawnedEventArgs e)
        {
            drawer.DrawPerson(e.Person);
        }

        private void Lift_PersonMoved(object sender, PersonMovedEventArgs e)
        {
            drawer.MovePerson(e.Person);
        }

        private void Lift_LiftMoved(object sender, LiftMovedEventArgs e)
        {
            drawer.MoveLift();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Person p = building.SpawnPerson();
            //building.Lift.Move();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drawer.DrawLevels();
            drawer.DrawLift();



        }
    }
}
