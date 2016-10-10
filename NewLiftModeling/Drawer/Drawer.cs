using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewLiftModeling
{
    public class Drawer
    {
        public Canvas Canvas { get; set; }
        public Building Building { get; set; }

        public LiftModel LiftModel { get; set; }
        public List<LevelModel> LevelsModel { get; set; }
        public List<PersonModel> PeopleModels { get; set; }

        public Drawer(Canvas canvas, Building building)
        {
            Canvas = canvas;
            Building = building;
            PeopleModels = new List<PersonModel>();

            //
            LevelsModel = new List<LevelModel>();
            foreach (Level lvl in Building.Levels)
                LevelsModel.Add(new LevelModel(lvl));
            LiftModel = new LiftModel(Building.Lift);

        }

        //Отрисовка формы объекта
        public void DrawLift()
        {
            SetLiftCoords();

            Rectangle liftRect = new Rectangle();
            liftRect.Width = LiftModel.Width;
            liftRect.Height = LiftModel.Heigth;
            liftRect.Stroke = new SolidColorBrush(Colors.Brown);
            liftRect.StrokeThickness = 1;
            liftRect.Name = "Lift";
            Canvas.Children.Add(liftRect);
            Canvas.SetLeft(liftRect, LiftModel.X);
            Canvas.SetTop(liftRect, LiftModel.Y);

        }
        public void DrawLevels()
        {
            double y = Canvas.ActualHeight - Settings.MARGIN;
            for (int i = 0; i < LevelsModel.Count; i++)
            {
                //foreach (LevelModel lvlm in LevelsModel)
                //{
                //y -= lvlm.Heigth;
                LevelModel lvlm = LevelsModel[i];
                y -= lvlm.Heigth;
                Rectangle r = new Rectangle();
                r.Width = lvlm.Width;
                r.Height = lvlm.Heigth;
                r.Stroke = new SolidColorBrush(Colors.Brown);
                r.StrokeThickness = 1;
                r.Name = "Level" + i;

                Canvas.Children.Add(r);
                Canvas.SetLeft(r, 5);
                Canvas.SetTop(r, y);
                //}
            }

        }
        public void DrawPerson(Person person)
        {
            PersonModel personModel = new PersonModel(person);
            PeopleModels.Add(personModel);
            SetPersonCoords(personModel);

            Ellipse el = new Ellipse();
            el.Width = Settings.PERSON_RADIUS;
            el.Height = Settings.PERSON_RADIUS;
            el.Stroke = new SolidColorBrush(Settings.PERSON_COLOR);
            el.StrokeThickness = 1;
            el.Fill = new SolidColorBrush(Settings.PERSON_COLOR);
            el.Name = "Person" + personModel.Person.ID;
            Canvas.Children.Add(el);
            Canvas.SetLeft(el, personModel.X);
            Canvas.SetTop(el, personModel.Y);
        }

        //Рассчет координат объектов
        public void SetLiftCoords()
        {
            int currentLevel = LiftModel.Lift.CurrentLevel.LevelNumber;
            double x = Settings.LEVEL_WIDTH + Settings.MARGIN / 2;
            double y = Canvas.ActualHeight - (Settings.MARGIN + Settings.LIFT_HEIGTH + Settings.LEVEL_HEIGTH * currentLevel);
            LiftModel.X = x;
            LiftModel.Y = y;
        }
        public void SetPersonCoords(PersonModel personModel)
        {
            int currentLevel = personModel.Person.CurrentLevel.LevelNumber;
            if (!personModel.Person.IsInLift)
            {
                if (personModel.Person.CurrentLevel.LevelNumber == 0)
                {
                    var personCircle = (Ellipse)LogicalTreeHelper.FindLogicalNode(Canvas, "Person" + personModel.Person.ID);
                    Canvas.Children.Remove(personCircle);
                    return;
                }
                personModel.Y = Canvas.ActualHeight - (Settings.MARGIN * 2 + Settings.LEVEL_HEIGTH * (currentLevel));
                //personModel.X = Settings.LEVEL_WIDTH;
                personModel.X = Settings.LEVEL_WIDTH - (Settings.MARGIN * 2 + (Settings.QUEUE_GAP + Settings.PERSON_RADIUS) * personModel.Person.QueueNumber);
            }
            else
            {



                personModel.Y = Canvas.ActualHeight - (Settings.MARGIN * 2 + Settings.LEVEL_HEIGTH * (currentLevel));
                personModel.X = Settings.LEVEL_WIDTH - (Settings.MARGIN * 2 + (Settings.QUEUE_GAP + Settings.PERSON_RADIUS) * personModel.Person.QueueNumber) + Settings.LIFT_WIDTH + personModel.Person.QueueNumber*Settings.QUEUE_GAP;
            }
            List<PersonModel> ToRemove = PeopleModels.FindAll(e => e.Person.CurrentLevel.LevelNumber == 0 && !e.Person.IsInLift);
            foreach(var pm in ToRemove)
            {
                var personCircle = (Ellipse)LogicalTreeHelper.FindLogicalNode(Canvas, "Person" + pm.Person.ID);
                PeopleModels.Remove(pm);

            }

        }

        //Анимация
        public void MoveLift()
        {
            SetLiftCoords();
            var lift = (Rectangle)LogicalTreeHelper.FindLogicalNode(Canvas, "Lift");
            //Rectangle lift = (Rectangle) Canvas.FindName("Lift");
            Canvas.SetTop(lift, LiftModel.Y);
            Canvas.SetLeft(lift, LiftModel.X);
        }
        public void MovePerson(Person p)
        {
            PersonModel pModel = PeopleModels.Find(e => e.Person == p);
            SetPersonCoords(pModel);
            var personCircle = (Ellipse)LogicalTreeHelper.FindLogicalNode(Canvas, "Person" + pModel.Person.ID);
            if (personCircle != null)
            {
                Canvas.SetTop(personCircle, pModel.Y);
                Canvas.SetLeft(personCircle, pModel.X);
            }
        }
    }

}
