using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using System;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework.Input;
using SadConsole.Instructions;


namespace StarterProject
{
   

    public class MainMenu : ControlsConsole, IUserInterface
        {

        bool startFade;
        SadConsole.Instructions.FadeTextSurfaceTint tintFade;
        public SadConsole.Console Console
           {
                get { return this; }
           }

        public InstructionSet animationintro;
        public Intro _introConsole;
        public ControlsConsole menugombok;
        public LogoConsole logoConsole;

        Button playButton = new Button(20, 3)
        {
            Text = "(J)  Játék",
            Position = new Point((Constants.GameWindowWidth / 2) - 10, (Constants.GameWindowHeight / 2) - 4),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };
        Button contributorsButton = new Button(20, 3)
        {
            Text = "(I)  Info",
            Position = new Point((Constants.GameWindowWidth / 2) - 10, (Constants.GameWindowHeight / 2)),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };

        Button optionsButton = new Button(20, 3)
        {
            Text = "(B)  Beállítások",
            Position = new Point((Constants.GameWindowWidth / 2) - 10, (Constants.GameWindowHeight / 2) + 4),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };

        Button exitButton = new Button(20, 3)
        {
            Text = "(K)  Kilép",
            Position = new Point((Constants.GameWindowWidth / 2) - 10, (Constants.GameWindowHeight / 2) + 8),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };




        public MainMenu() : base(100,40)
            {
            var hatterConsoleMenu = new PlazmaConsole();
            hatterConsoleMenu.Position = new Point(0, 0);
            Children.Add(hatterConsoleMenu);

            menugombok = new ControlsConsole(100, 40);
            menugombok.Position = new Point(0, 0);
            menugombok.Theme = Themebeallit.SetupThemesMenu();
            hatterConsoleMenu.Children.Add(menugombok);


            logoConsole = new LogoConsole(61, 7) { Position = new Point(Constants.GameWindowWidth / 2 - 31, 2) };
            menugombok.Children.Add(logoConsole);



            playButton.Click += ButtonPressPlay;
            menugombok.Add(playButton);

            _introConsole = new Intro();
            _introConsole.Position = new Point(-18, -1);
            logoConsole.borderSurface.Children.Add(_introConsole);

            //foregroundConsole.Renderer = new SadConsole.Consoles.CachedTextSurfaceRenderer(foregroundConsole.TextSurface);
            _introConsole.Tint = Color.Black;
            var tintFade1 = new SadConsole.Instructions.FadeTextSurfaceTint(_introConsole, new ColorGradient(Color.Black, Color.Transparent), TimeSpan.FromSeconds(4));


            animationintro = new InstructionSet()
            .Code((console, delta) =>
            {

                _introConsole.frameB.IsVisible = false;
                _introConsole.Surfaceszovegenter.IsVisible = false;
                _introConsole.Surfaceszovegintro.IsVisible = false;

                return true;
            })
            .Instruct(tintFade1)
            
            .Wait(TimeSpan.FromSeconds(0.1d))
            .Code((console, delta) =>
            {
                logoConsole.borderSurface.Children.Clear();
                return true;
            })
            ;



            //_introConsole.DefaultBackground = backColor;
            


            



            BeallitasManager.InitializeDefaultBeallitas();

           contributorsButton.Click += (a, b) =>
           {
             var _options = UserInterfaceManager.Get<InfoConsole>();
             Program.MoveNextConsole(_options);

            }; ;
            menugombok.Add(contributorsButton);

            optionsButton.Click += (a, b) =>
            {
                var _options = UserInterfaceManager.Get<Options>();
                Program.MoveNextConsole(_options);

            };
            menugombok.Add(optionsButton);



       
           exitButton.Click += (a, b) =>
           {
            Environment.Exit(0);
            };
            menugombok.Add(exitButton);
           }

            public void ButtonPressPlay(object sender, EventArgs args)
             {
                var vagoe = BeallitasManager.GetBeallitas("vmm");
            if (vagoe == 1)
            {
                var _vagoScreen= UserInterfaceManager.Get<VagoScreen>();
                _vagoScreen.IsVisible = true;
                _vagoScreen.vagoVillan();
                Program.MoveNextConsole(_vagoScreen);
                

                
            }
            else
            {
                Ujjatekotkezd();
            }

            }

            public void Ujjatekotkezd()
            {
            var _actualgame = new ActualGame();
            }

        

        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyReleased(Keys.J))
            {
                playButton.DoClick();
            }

            if (info.IsKeyReleased(Keys.I))
            {
                contributorsButton.DoClick();
            }

            if (info.IsKeyReleased(Keys.B))
            {
                optionsButton.DoClick();
            }
            if (info.IsKeyReleased(Keys.K))
            {
                exitButton.DoClick();
            }
            if (info.IsKeyReleased(Keys.Enter))
            {
                Components.Add(animationintro);
 
                //menugombok.IsFocused = true;
            }
            return true;
        }
        public override void Update(TimeSpan delta)
        {
            //if (tintFade.IsFinished)
            //{ }
            

            base.Update(delta);
        }
    }
 }


