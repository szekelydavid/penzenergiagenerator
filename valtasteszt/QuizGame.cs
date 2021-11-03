using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using Microsoft.Xna.Framework.Input;
using SadConsole.Input;
using SadConsole.Controls;
using SadConsole.Themes;
using Console = SadConsole.Console;
using SadConsole.Instructions;
using System.IO.Compression;
using SadConsole.Readers;
using System.Globalization;

namespace StarterProject
{
    struct Kerdes
    {
        public string Feladvany;
        public string ValaszA;
        public string ValaszB;
        public string ValaszC;
        public string ValaszD;
        public int MegoldasIndex;
    }


    public class QuizGame : SadConsole.ControlsConsole, IUserInterface
    {

        private readonly SadConsole.Timer progressTimer;
        public int timeleft;
        private List<Kerdes> teszt;
        public static int[] penzenergiavektor= { 500, 1000, 2000, 30000, 5000, 7500, 10000, 15000, 25000, 50000, 75000, 150000, 250000, 500000, 1000000 };
    public Console Console => this;

        public Console Surfaceszoveg { get; set; }
        public Console Surfaceszoveg2 { get; set; }
        public Console Surfaceszoveg3 { get; set; }


        public ControlsConsole feladatConsole { get; set; }
        public int kerdesmax;
        public int kerdesszam;

        Button Abutton = new Button(40, 4)
        {
            Name = "0",
            Text = "A",
            Position = new Point(6, 18),
            UseMouse = true,
            UseKeyboard = true,
            TextAlignment = HorizontalAlignment.Left,
            IsDirty = true,
        };

        Button Bbutton = new Button(40, 4)
        {
            Name = "1",
            Text = "B ",
            Position = new Point(54, 18),
            UseMouse = true,
            UseKeyboard = true,
            //Theme = Themebeallit.SetupThemesA(),
            TextAlignment = HorizontalAlignment.Left,
            IsDirty = true,
        };

        Button Cbutton = new Button(40, 4)
        {
            Name = "2",
            Text = "C ",
            Position = new Point(6, 26),
            UseMouse = true,
            UseKeyboard = true,

            TextAlignment = HorizontalAlignment.Left,
        };


        Button Dbutton = new Button(40, 4)
        {
            Name = "3",
            Text = "D",
            Position = new Point(54, 26),
            UseMouse = true,
            UseKeyboard = true,
            TextAlignment = HorizontalAlignment.Left,

        };

        public Button[] buttonlist = new Button[4];

        Button backButton = new Button(25, 4)
        {
            Text = "(V)  Vissza",
            Position = new Point(69, 34),
            UseMouse = true,
            UseKeyboard = false,
            TextAlignment = HorizontalAlignment.Left,
        };

        public QuizGame() : base(100, 40)
        {

            //Timer beállításai
            progressTimer = new Timer(TimeSpan.FromSeconds(1));
            timeleft = BeallitasManager.GetBeallitas("ido");
            progressTimer.TimerElapsed += (timer, e) => { timeleft--; };
            Components.Add(progressTimer);


            //Kérdések száma
            kerdesszam = 0;
            kerdesmax= BeallitasManager.GetBeallitas("kerdes");



            //! Konzolok létrehozása

            var hatterConsole = new ControlsConsole(100, 40);
            hatterConsole.Position = new Point(0, 0);
            Children.Add(hatterConsole);


            //! feladatConsole.FillWithRandomGarbage();

            //! háttérkép beolvasása és beállítása konzolnak
            GZipStream instream = new GZipStream(File.OpenRead(@"..\..\res\hatter1.gz"), CompressionMode.Decompress);


            var rexi = REXPaintImage.Load(instream);
            var rexilay = rexi.ToLayeredConsole();
            hatterConsole.Children.Add(rexilay);

            feladatConsole = new ControlsConsole(100, 40);
            feladatConsole.Position = new Point(0, 0);
            feladatConsole.Theme = Themebeallit.SetupThemesA();
            hatterConsole.Children.Add(feladatConsole);


            // !Kérdéseket megjelenítő konzol létrehozása
            Surfaceszoveg = new Console(40, 4, SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.Two))
            {
                DefaultBackground = Color.Black,
                DefaultForeground = Color.White,
            };
            Surfaceszoveg.Position = new Point(5, 3);
            feladatConsole.Children.Add(Surfaceszoveg);
            Surfaceszoveg.Cursor.UseStringParser = true;
            Surfaceszoveg.Cursor.IsVisible = true;

            //! időt kijelző konzol beálítása
            Surfaceszoveg2 = new Console(13, 2, SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.Two))
            {
                DefaultBackground = Color.Black,
                DefaultForeground = Color.White,
            };
            Surfaceszoveg2.Position = new Point(3, 17);
            feladatConsole.Children.Add(Surfaceszoveg2);
            Surfaceszoveg2.UsePrintProcessor = true;

            //! pontszám és kérdést jelző konzol

            Surfaceszoveg3 = new Console(19, 4)
            {
                DefaultBackground = Color.Black,
                DefaultForeground = Color.White,
            };
            Surfaceszoveg3.Position = new Point(41,35 );
            feladatConsole.Children.Add(Surfaceszoveg3);
            Surfaceszoveg3.UsePrintProcessor = true;

            //! első kérdés felirat
            Surfaceszoveg3.Print(0, 0, $"{kerdesszam}. kérdés", Color.LightCyan);
            Surfaceszoveg3.Print(0, 1, $"{penzenergiavektor[kerdesszam]}", Color.Yellow);
            Surfaceszoveg3.Print(0, 2, "pénzenergia", Color.Yellow);

            //! Kérdések beolvasása
            Beolvas();

            //Első kérdés kirása
            Surfaceszoveg.Clear();
            Surfaceszoveg.Cursor.Position = new Point(0, 0);
            Surfaceszoveg.Cursor.Print(teszt[kerdesszam].Feladvany);



            //! Gombok szövege
            Abutton.Text = teszt[kerdesszam].ValaszA;
            Bbutton.Text = teszt[kerdesszam].ValaszB;
            Cbutton.Text = teszt[kerdesszam].ValaszC;
            Dbutton.Text = teszt[kerdesszam].ValaszD;


            //! Gombok funkciója és hozzáadása
            Abutton.Click += ValaszCheck;


            feladatConsole.Add(Abutton);

            Bbutton.Click += ValaszCheck;
            feladatConsole.Add(Bbutton);

            Cbutton.Click += ValaszCheck;
            feladatConsole.Add(Cbutton);

            Dbutton.Click += ValaszCheck;
            feladatConsole.Add(Dbutton);



            backButton.Click += (sender, args) =>
            {
                var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                Program.MoveNextConsole(_mainmenu);
            };
            backButton.Theme=Themebeallit.SetupThemesMenu().ButtonTheme;
            feladatConsole.Add(backButton);


        }


        private void Beolvas()
        {
            teszt = new List<Kerdes>();

            int nyelv= BeallitasManager.GetBeallitas("nyelv");
            var srk = new StreamReader(@"..\..\res\teszt.txt", Encoding.UTF8);
            if (nyelv == 1)
            {
               srk = new StreamReader(@"..\..\res\teszt02.txt", Encoding.UTF8);
            }


            while (!srk.EndOfStream)
            {
                teszt.Add(new Kerdes()
                {
                    Feladvany = srk.ReadLine(),
                    ValaszA = srk.ReadLine(),
                    ValaszB = srk.ReadLine(),
                    ValaszC = srk.ReadLine(),
                    ValaszD = srk.ReadLine(),

                    MegoldasIndex = int.Parse(srk.ReadLine()),
                });
            }
            srk.Close();
        }




        private Button _buttonPressed;

        public void ValaszCheck(object sender, EventArgs args)
        {
            //if (kerdezesallapot == false) { return; }


            //kerdezesallapot = false;
            feladatConsole.UseMouse = false;
            feladatConsole.UseKeyboard = false;
            _buttonPressed = (Button)sender;


            //ButtonVillodzik((Button)sender);


            Button[] buttonlist = { Abutton, Bbutton, Cbutton, Dbutton };
            Button helyesButton = buttonlist[teszt[kerdesszam].MegoldasIndex];
            int number = int.Parse(_buttonPressed.Name);


            InstructionSet animation = new InstructionSet()
           .Wait(TimeSpan.FromSeconds(0.3d))

          .Code((console, delta) =>
          {

              var consoleTheme = Library.Default.Clone();
              _buttonPressed.Theme = consoleTheme.ButtonTheme;
              consoleTheme.ButtonTheme.SetBackground(Color.Orange);

              return true;
          })
          .Wait(TimeSpan.FromSeconds(1d))
          .Code((console, delta) =>
          {

              var consoleTheme = Library.Default.Clone();
              helyesButton.Theme = consoleTheme.ButtonTheme;
              consoleTheme.ButtonTheme.SetBackground(Color.Yellow);

              return true;
          })

            .Wait(TimeSpan.FromSeconds(0.2d))
            .Code((console, delta) =>
            {

                var consoleTheme = Library.Default.Clone();
                helyesButton.Theme = consoleTheme.ButtonTheme;
                consoleTheme.ButtonTheme.SetBackground(Color.Green);

                return true;
            })

            .Wait(TimeSpan.FromSeconds(0.3d))
            .Code((console, delta) =>
            {

                var consoleTheme = Themebeallit.SetupThemesA();
                helyesButton.Theme = consoleTheme.ButtonTheme;
                _buttonPressed.Theme = consoleTheme.ButtonTheme;
                _buttonPressed.IsFocused = false;
                helyesButton.IsFocused = false;



                return true;
            })


            .Code((console, delta) =>
            {

                if (number == teszt[kerdesszam].MegoldasIndex)
                {
                    Surfaceszoveg.Clear();
                    Surfaceszoveg.Cursor.Position = new Point(0, 0);
                    Surfaceszoveg.Cursor.Print("Jó válasz!");
                    if (kerdesszam == kerdesmax - 1) { Nyertel(); }
                }
                else
                {
                    Rosszvalasz();

                }
                return true;
            })
            .Wait(TimeSpan.FromSeconds(2.0d))
            .Code((console, delta) =>
            {
                if (number == teszt[kerdesszam].MegoldasIndex)
                {
                    UjKerdes();
                }
                return true;
            });
          animation.RemoveOnFinished = true;
          Components.Add(animation);




        }

        public void UjKerdes()
        {

            kerdesszam++;

            Surfaceszoveg.Clear();
            Surfaceszoveg.Cursor.Position = new Point(0, 0);
            Surfaceszoveg.Cursor.Print(teszt[kerdesszam].Feladvany);
            Abutton.Text = teszt[kerdesszam].ValaszA;
            Bbutton.Text = teszt[kerdesszam].ValaszB;
            Cbutton.Text = teszt[kerdesszam].ValaszC;
            Dbutton.Text = teszt[kerdesszam].ValaszD;
            feladatConsole.UseMouse = true;
            feladatConsole.UseKeyboard = true;
            timeleft = BeallitasManager.GetBeallitas("ido");
            Surfaceszoveg3.Print(0, 0, $"{kerdesszam}. kérdés", Color.LightCyan);
            Surfaceszoveg3.Print(0, 1, $"{penzenergiavektor[kerdesszam]}", Color.Yellow);
            Surfaceszoveg3.Print(0, 2, "pénzenergia", Color.Yellow);

        }

        public void Rosszvalasz()
        {
            feladatConsole.UseMouse = false;
            feladatConsole.UseKeyboard = false;
            InstructionSet animationrossz = new InstructionSet()
             .Code((console, delta) =>
              {
                  Surfaceszoveg.Clear();
                  Surfaceszoveg.Cursor.Position = new Point(0, 0);
                  Surfaceszoveg.Cursor.Print("Rossz válasz!");
                  return true;
              })
              .Wait(TimeSpan.FromSeconds(2.0d))
              .Code((console, delta) =>
              {
                  Surfaceszoveg.Clear();
                  Surfaceszoveg.Cursor.Position = new Point(0, 0);
                  Surfaceszoveg.Cursor.Print("Vissza a menübe: 3...");
                  return true;
              })
              .Wait(TimeSpan.FromSeconds(1.0d))
               .Code((console, delta) =>
               {
                   Surfaceszoveg.Cursor.Position = new Point(0, 0);
                   Surfaceszoveg.Cursor.Print("Vissza a menübe: 2...");
                   return true;
               })
               .Wait(TimeSpan.FromSeconds(1.0d))
               .Code((console, delta) =>
               {
                   Surfaceszoveg.Cursor.Position = new Point(0, 0);
                   Surfaceszoveg.Cursor.Print("Vissza a menübe: 1...");
                   return true;
               })
               .Wait(TimeSpan.FromSeconds(1.0d))
               .Code((console, delta) =>
               {
                   var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                   Program.MoveNextConsole(_mainmenu);
                   return true;
               });


            animationrossz.RemoveOnFinished = true;

            Components.Add(animationrossz);

        }


        public void Nyertel()
        {
            feladatConsole.UseMouse = false;
            feladatConsole.UseKeyboard = false;
            InstructionSet animationnyert = new InstructionSet()
               .Code((console, delta) =>
               {
                   Surfaceszoveg.Clear();
                   Surfaceszoveg.Cursor.Position = new Point(0, 0);
                   Surfaceszoveg.Cursor.Print("Gratulálunk! Nyertél!");
                   return true;
               })
                .Wait(TimeSpan.FromSeconds(2.0d))
                .Code((console, delta) =>
                {
                    Surfaceszoveg.Clear();
                    Surfaceszoveg.Cursor.Position = new Point(0, 0);
                    Surfaceszoveg.Cursor.Print($"Nyereményed: {penzenergiavektor[kerdesszam-1]} egységnyi pénzenergia!");
                    return true;
                })
                .Wait(TimeSpan.FromSeconds(2.0d))
                .Code((console, delta) =>
                {
                    Surfaceszoveg.Clear();
                    Surfaceszoveg.Cursor.Position = new Point(0, 0);
                    Surfaceszoveg.Cursor.Print("Vissza a menübe: 3...");
                    return true;
                })
                .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
                 {
                     Surfaceszoveg.Cursor.Position = new Point(0, 0);
                     Surfaceszoveg.Cursor.Print("Vissza a menübe: 2...");
                     return true;
                 })
                 .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
                 {
                     Surfaceszoveg.Cursor.Position = new Point(0, 0);
                     Surfaceszoveg.Cursor.Print("Vissza a menübe: 1...");
                     return true;
                 })
                 .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
               {
                   var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                   Program.MoveNextConsole(_mainmenu);
                   return true;
               });


            animationnyert.RemoveOnFinished = true;

            Components.Add(animationnyert);

        }




        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyReleased(Keys.A))
            {
                Abutton.DoClick();
            }

            if (info.IsKeyReleased(Keys.B))
            {
                Bbutton.DoClick();
            }

            if (info.IsKeyReleased(Keys.C))
            {
                Cbutton.DoClick();
            }
            if (info.IsKeyReleased(Keys.D))
            {
                Dbutton.DoClick();
            }

            if (info.IsKeyReleased(Keys.V))
            {
                backButton.DoClick();
            }


            return true;

        }
        public override void Update(TimeSpan delta)
        {
            if (IsVisible)
            {
                UpdateScreen();


                base.Update(delta);
            }
        }

        public void UpdateScreen()
        {
            //var nameStr = $"Joska .:Geze:.";
            //nameStr = nameStr.PadLeft(22 + nameStr.Length / 2).PadRight(44);
            if (timeleft == 0)
            {
                progressTimer.IsPaused = true;
                Lejartazido();
            }
            //Surfaceszoveg.Print(0, 0, $"[c:g b:red:black:black:blue:{nameStr.Length}]" + timeleft.ToString(), Color.Orange);
            //Surfaceszoveg.Cursor.Print(timeleft.ToString());
            //Print(10, 2, x.ToString());
            int hbmax= BeallitasManager.GetBeallitas("ido");
            CreateHealthBar(0, 0, Color.DarkGreen, Color.LimeGreen, timeleft, hbmax);


        }


        public void Lejartazido()
        {
            feladatConsole.UseMouse = false;
            feladatConsole.UseKeyboard = false;
            InstructionSet animationlejart = new InstructionSet()
               .Code((console, delta) =>
               {
                   Surfaceszoveg.Clear();
                   Surfaceszoveg.Cursor.Position = new Point(0, 0);
                   Surfaceszoveg.Cursor.Print("Lejárt a idö''d!");
                   return true;
               })
                .Wait(TimeSpan.FromSeconds(2.0d))
                .Code((console, delta) =>
                {
                    Surfaceszoveg.Clear();
                    Surfaceszoveg.Cursor.Position = new Point(0, 0);
                    Surfaceszoveg.Cursor.Print("Vissza a menübe: 3...");
                    return true;
                })
                .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
                 {
                     Surfaceszoveg.Cursor.Position = new Point(0, 0);
                     Surfaceszoveg.Cursor.Print("Vissza a menübe: 2...");
                     return true;
                 })
                 .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
                 {
                     Surfaceszoveg.Cursor.Position = new Point(0, 0);
                     Surfaceszoveg.Cursor.Print("Vissza a menübe: 1...");
                     return true;
                 })
                 .Wait(TimeSpan.FromSeconds(1.0d))
                 .Code((console, delta) =>
                 {
                     var _mainmenu = UserInterfaceManager.Get<MainMenu>();
                     Program.MoveNextConsole(_mainmenu);
                     return true;
                 });


            animationlejart.RemoveOnFinished = true;

            Components.Add(animationlejart);
        }

        public void CreateHealthBar(int x, int y, Color low, Color full, int current, int max)
        {
            Surfaceszoveg2.Print(x, y + 1, current.ToString(CultureInfo.InvariantCulture).PadLeft(9), Color.Orange);
            Surfaceszoveg2.Print(x + 10, y + 1, "mp", Color.Gainsboro);
            CreateBar(x, y, low, full, (float)current / max);

        }
        public void CreateBar(int x, int y, Color low, Color full, float perc)
        {
            Surfaceszoveg2.Print(x, y, "[c:sg 176:12]" + "".PadRight(13), low);
            var p = perc * 36;
            var z = (int)(p / 3);
            var t = (int)(p % 3);
            var g = 176 + t;
            Surfaceszoveg2.Print(x, y, $"[c:sg 219:{z}]" + "".PadRight(z), full);
            if (t != 0)
            {
                Surfaceszoveg2.SetGlyph(x + z, y, g, full);
            }
        }



    }

 }

