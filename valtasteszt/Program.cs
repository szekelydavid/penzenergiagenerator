using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SadConsole.Themes;

namespace StarterProject
{
    class Program
    {
        public static Console selectedConsole;
        public static Options _options { get; set; }
        public static MainMenu _mainmenu;
        public static MContainer _mainConsole;
        public static VagoScreen _vagoScreen;
        public static InfoConsole _infoConsole;
        //public static Font normalSizedFont;



        static void Main(string[] args)
        {
           
            //var normalSizedFont = SadConsole.Global.LoadFont("Fonts/Cheepicus12.font").GetFont(Font.FontSizes.One);
            // Setup the engine and create the main window.
            SadConsole.Game.Create(100, 40);
            //fsfdsdss


            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            SadConsole.Game.OnDraw = DrawFrame;
            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }
        private static void FontDef()
        {
            SadConsole.Global.FontDefault = SadConsole.Global.LoadFont("Fonts/IBM.font").GetFont(Font.FontSizes.One);
            
        }
        private static void Init()
        {
            // Any setup
            if (Settings.UnlimitedFPS)
                SadConsole.Game.Instance.Components.Add(new SadConsole.Game.FPSCounterComponent(SadConsole.Game.Instance));

            // Setup our custom theme.
            //Theme.SetupThemes();
            //var normalSizedFont = SadConsole.Global.LoadFont("Fonts/cp437.font").GetFont(Font.FontSizes.One);
           
            FontDef();
            SadConsole.Game.Instance.Window.Title = "DemoProject OpenGL";

            // By default SadConsole adds a blank ready-to-go console to the rendering system. 
            // We don't want to use that for the sample project so we'll remove it.

            //Global.MouseState.ProcessMouseWhenOffScreen = true;
            
            _mainmenu = new MainMenu();
            _mainmenu.Theme = Themebeallit.SetupThemesMenu();
            _options = new Options();
            _mainConsole = new MContainer();
            _vagoScreen = new VagoScreen()
            { SplashCompleted = _mainmenu.Ujjatekotkezd };
            _infoConsole = new InfoConsole();
            _infoConsole.Theme = Themebeallit.SetupThemesI();
            //_gameplay = new GamePlay();
            UserInterfaceManager.Add(_mainmenu);
            UserInterfaceManager.Add(_options);
            UserInterfaceManager.Add(_vagoScreen);
            UserInterfaceManager.Add(_infoConsole);
            //UserInterfaceManager.Add(gameplay);
            // We'll instead use our demo consoles that show various features of SadConsole.






            MoveNextConsole(_mainmenu);

            // Initialize the windows


        }
        public static void MoveNextConsole(Console beadottconsole)
        {


            selectedConsole = beadottconsole;

            Global.CurrentScreen.Children.Clear();
            Global.CurrentScreen.Children.Add((SadConsole.Console)selectedConsole);

            selectedConsole.IsVisible = true;
            selectedConsole.IsFocused = true;
            selectedConsole.Position = new Point(0, 0);

            //Global.FocusedConsoles.Set(selectedConsole);
        }
        private static void Update(GameTime time)
        {
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
        }

        private static void DrawFrame(GameTime time)
        {
            // Custom drawing. You don't usually have to do this.
        }

    
           public static void Reset()
            {
                //UserInterfaceManager.IsInitialized = false;

                var skipInterfaces = new[]
                {
                UserInterfaceManager.Get<MainMenu>() as IUserInterface,
                UserInterfaceManager.Get<Options>() as IUserInterface,
                UserInterfaceManager.Get<VagoScreen>() as IUserInterface,
                };

                foreach (var inf in UserInterfaceManager.GetAll<IUserInterface>())
                {
                    if (skipInterfaces.Contains(inf)) continue;
                    UserInterfaceManager.Remove(inf);
                }

               
            }
        
    }
}