using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using System;
using System.Linq;
using SadConsole.Themes;
using SadConsole.StringParser;
using System.Threading;

namespace StarterProject
{


    public class ActualGame : ContainerConsole
    {
        

        //public string Abutton.Text;
        public SadConsole.Console Console
        {
            get { return this; }
        }

     


        public ActualGame() : base()
        {

            Program.Reset();
            //UserInterfaceManager.Initialize();
            // Initialize game window, set's the Global.CurrentScreen
            QuizGame quizGame = new QuizGame();
            UserInterfaceManager.Add(quizGame);
            

            // Initialize map

            Program.MoveNextConsole(quizGame);

            
           

        }



    }
}



