using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;
using Console = SadConsole.Console;
using SadConsole.Ansi;
using System.IO;

namespace StarterProject
{
    public class InfoConsole : ControlsConsole, IUserInterface
    {

        public ControlsConsole gombinfoConsole { get; set; }

        public Console Console => this;

        Button backButton = new Button(20, 3)
        {
            Text = "(V)  Vissza",
            Position = new Point(78, 35),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };

        public InfoConsole() : base(Constants.GameWindowWidth, Constants.GameWindowHeight)
        {
            var _hatterinfo = new Console(100, 40);
            Children.Add(_hatterinfo);
            var docinf = new SadConsole.Ansi.Document(@"info2.ans");
            var ansiinfoswr = new AnsiWriter(docinf, _hatterinfo);

            ansiinfoswr.CharactersPerSecond = 720;

            var ansitimer02 = new Timer(TimeSpan.FromSeconds(1));
            double ansitime = 0;

            ansitimer02.TimerElapsed += (timer, e) =>
            {
                ansitime++;
                ansiinfoswr.Process(ansitime);
            };

            Components.Add(ansitimer02);

            gombinfoConsole = new ControlsConsole(100, 40);
            gombinfoConsole.Position = new Point(0, 0);
            Children.Add(gombinfoConsole);

            gombinfoConsole.Theme = Themebeallit.SetupThemesMenu();




           
            backButton.Click += (sender, args) =>
            {
                var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                Program.MoveNextConsole(_mainmenu);

            };
            gombinfoConsole.Add(backButton);


        }
        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyReleased(Keys.V))
            {
                backButton.DoClick();
            }
            return true;
        }
    }
}
