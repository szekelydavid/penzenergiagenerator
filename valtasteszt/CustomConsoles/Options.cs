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
    public class Options : ControlsConsole, IUserInterface
    {
        public int currentIdo;
        public int MinIdo;
        public int currentKerdes;
        public int MinKerdes;
        public ControlsConsole gombosConsole { get; set; }

        public Console Console => this;

        private readonly SadConsole.Controls.RadioButton optionButtonmagyar;
        private readonly SadConsole.Controls.RadioButton optionButtonAngol;

        Button backButton = new Button(20, 3)
        {
            Text = "(V)  Vissza",
            Position = new Point(78, 35),
            UseMouse = true,
            UseKeyboard = false,
        };
        SadConsole.Controls.ScrollBar idoScroll = new SadConsole.Controls.ScrollBar(Orientation.Horizontal, 20) { Position = new Point(2, 26) };
        SadConsole.Controls.ScrollBar kerdesScroll = new SadConsole.Controls.ScrollBar(Orientation.Horizontal, 20) { Position = new Point(2, 28) };
        SadConsole.Controls.CheckBox checkbox = new SadConsole.Controls.CheckBox(20, 1)
        {
            Text = "Vágó-üzemmód",
            Position = new Point(2, 33)
        };


        public Options() : base(Constants.GameWindowWidth, Constants.GameWindowHeight)
        {
            var _hatteroptions = new Console(100, 40);
            Children.Add(_hatteroptions);
            var doc = new SadConsole.Ansi.Document(@"optionsbackground02.ans");
            var ansioptionswr = new AnsiWriter(doc, _hatteroptions);

            ansioptionswr.CharactersPerSecond = 960;

            var ansitimer01 = new Timer(TimeSpan.FromSeconds(1));
            double ansitime = 0;

            ansitimer01.TimerElapsed += (timer, e) =>
            {
                ansitime++;
                ansioptionswr.Process(ansitime);
            };

            Components.Add(ansitimer01);

            gombosConsole = new ControlsConsole(100, 40);
            gombosConsole.Position = new Point(0, 0);
            Children.Add(gombosConsole);





            //ansioptionswr.ReadEntireDocument();

            Theme = Themebeallit.SetupThemesMenu();




         
            backButton.Click += (sender, args) =>
            {
                var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                Program.MoveNextConsole(_mainmenu);
            };
            gombosConsole.Add(backButton);
            gombosConsole.Theme = Themebeallit.SetupThemesMenu();



            MinIdo = 15;
            //var idoScroll = new SadConsole.Controls.ScrollBar(Orientation.Horizontal, 20) { Position = new Point(2, 26) };
            idoScroll.Maximum = 60;

            idoScroll.Step = 1;
            idoScroll.Value = BeallitasManager.GetBeallitas("ido");
            idoScroll.ValueChanged += idoScroll_ValueChanged;
            currentIdo = BeallitasManager.GetBeallitas("ido");
            gombosConsole.Add(idoScroll);

            MinKerdes = 1;
            //var kerdesScroll = new SadConsole.Controls.ScrollBar(Orientation.Horizontal, 20) { Position = new Point(2, 28) };
            kerdesScroll.Maximum = 14;
            kerdesScroll.Step = 1;
            kerdesScroll.Value = BeallitasManager.GetBeallitas("kerdes");
            kerdesScroll.ValueChanged += kerdesScroll_ValueChanged;
            currentKerdes = BeallitasManager.GetBeallitas("kerdes");
            gombosConsole.Add(kerdesScroll);

            gombosConsole.Print(2, 25, "Idö:            (" + (currentIdo) + ")", new Cell(Color.Cyan, Color.Black));
            gombosConsole.Print(2, 27, "Kérdések száma: (" + ((currentKerdes)) + ")", new Cell(Color.Cyan, Color.Black));
            gombosConsole.Print(2, 29, "Nyelv:              ", new Cell(Color.Cyan, Color.Black));
            gombosConsole.Print(2, 32, "Kultúra:            ", new Cell(Color.Cyan, Color.Black));
            gombosConsole.Print(2, 35, "(Q) Idö''-  (W) Idö''+  (A)Kérdések-  (S)Kérdések+  (Y)Nyelv  (X)Vágó", new Cell(Color.Cyan, Color.Black));

            optionButtonAngol = new SadConsole.Controls.RadioButton(20, 1)
            {
                Text = "English",
                Position = new Point(2, 30),
            };
            optionButtonAngol.IsSelectedChanged += OptionButton_IsSelectedChanged;
            gombosConsole.Add(optionButtonAngol);

            optionButtonmagyar = new SadConsole.Controls.RadioButton(20, 1)
            {
                Text = "Magyar",
                Position = new Point(2, 31)
            };
            gombosConsole.Add(optionButtonmagyar);
            optionButtonAngol.IsSelectedChanged += OptionButton_IsSelectedChanged;

            optionButtonAngol.IsSelected = true;

            //SadConsole.Controls.CheckBox checkbox = new SadConsole.Controls.CheckBox(20, 1)
            //{
            //    Text = "Vágó-üzemmód",
            //    Position = new Point(2, 33)
            //};
            gombosConsole.Add(checkbox);
            checkbox.IsSelectedChanged += CB_IsSelectedChanged;

        }
        private void idoScroll_ValueChanged(object sender, EventArgs e)
        {
            gombosConsole.Print(2, 25, "                  ");
            gombosConsole.Print(2, 25, "Idö:            (" + ((sender as SadConsole.Controls.ScrollBar).Value + MinIdo) + ")", new Cell(Color.Cyan, Color.Black));
            currentIdo = ((sender as SadConsole.Controls.ScrollBar).Value + MinIdo);
            BeallitasManager.EditBeallitas("ido", currentIdo);
        }
        private void kerdesScroll_ValueChanged(object sender, EventArgs e)
        {
            currentKerdes = ((sender as SadConsole.Controls.ScrollBar).Value);
            gombosConsole.Print(2, 27, "                  ");
            gombosConsole.Print(2, 27, "Kérdések száma: (" + ((currentKerdes)) + ")", new Cell(Color.Cyan, Color.Black));
            //gombosConsole.Print(2, 16, "Resolution changes will require a restart", new Cell(Color.OrangeRed, Color.Black));

            BeallitasManager.EditBeallitas("kerdes", currentKerdes);
        }

        private void OptionButton_IsSelectedChanged(object sender, EventArgs e)
        {
            if (optionButtonmagyar.IsSelected)
            {
                BeallitasManager.EditBeallitas("nyelv", 1);
            }
            else if (optionButtonAngol.IsSelected)
            {
                BeallitasManager.EditBeallitas("nyelv", 0);
            }
            //gombosConsole.Print(10, 25, $"{BeallitasManager.GetBeallitas("nyelv")}", new Cell(Color.Cyan, Color.Black));
        }
        private void CB_IsSelectedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (SadConsole.Controls.CheckBox)sender;
            if (CB.IsSelected)
            {
                BeallitasManager.EditBeallitas("vmm", 1);
            }
            else
            {
                BeallitasManager.EditBeallitas("vmm", 0);
            }
            //gombosConsole.Print(10, 25, $"{BeallitasManager.GetBeallitas("vmm")}", new Cell(Color.Cyan, Color.Black));
        }
        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyReleased(Keys.V))
            {
                backButton.DoClick();
            }

            if (info.IsKeyReleased(Keys.Q))
            {
                idoScroll.Value -= 1;
            }

            if (info.IsKeyReleased(Keys.W))
            {
                idoScroll.Value += 1;
            }

            if (info.IsKeyReleased(Keys.A))
            {
                kerdesScroll.Value -= 1;
            }

            if (info.IsKeyReleased(Keys.S))
            {
                kerdesScroll.Value += 1;
            }

            if (info.IsKeyReleased(Keys.Y))
            {
                if (optionButtonmagyar.IsSelected)
                {
                    optionButtonAngol.IsSelected = true;
                }
                
                else if (optionButtonAngol.IsSelected)
                {
                    optionButtonmagyar.IsSelected = true;
                }
            }
            if (info.IsKeyReleased(Keys.X))
            {
                if (checkbox.IsSelected == true) { checkbox.IsSelected = false; }
                else { checkbox.IsSelected = true; }
            }

            return true;
        }
    }
}
