using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;
using SadConsole.Instructions;
using Console = SadConsole.Console;
using System.Collections.Generic;

namespace StarterProject
{
    public class Star
    {
        public float x;
        public float y;
        public float z;
        public float xpos;
        public float ypos;
        public int xszor;
        public int yszor;
        public Star()
        {
            x = 0f;
            y = 0f;
            z = 0f;
            xpos = 0f;
            ypos = 0f;
            xszor = 0;
            yszor = 0;
        }

    };


    public class Intro : Console
    {
        static Random rnd = new Random();
        public Console frameB { set; get; }
        static int NUMOFSTARS = 500;
        public List<Star> stars { get; set; }
        public object FrameB { get; private set; }

        public int tesztint;
        public Console Surfaceszovegintro;
        public Console Surfaceszovegenter;

        public Intro() : base(100, 40)
        {
            //DefaultBackground = Color.Black;

            Surfaceszovegintro = new Console(40, 2, SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.Two));
            //Surfaceszovegintro.Fill(Color.Black);
            Surfaceszovegintro.Position = new Point(7, 9);
           
            Children.Add(Surfaceszovegintro);
            Surfaceszovegintro.UsePrintProcessor = true;
            
            Surfaceszovegenter = new Console(5, 1, SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.Four));
            //Surfaceszovegintro.Fill(Color.Black);
            Surfaceszovegenter.Position = new Point(10, 9);
            Children.Add(Surfaceszovegenter);
            Surfaceszovegenter.UsePrintProcessor = true;
            Surfaceszovegenter.Print(0, 0, "[c:b 5:0.5]ENTER");

            var logoText = new ColorGradient(new[] { Color.Yellow, Color.DarkCyan }, new[] { 0.0f, 1f })
                               .ToColoredString("Pénzenergia Generátor Sci-Fi Edition");
            logoText.SetEffect(new Fade()
            {
                DestinationForeground = Color.Blue,
                FadeForeground = true,
                FadeDuration = 1f,
                Repeat = false,
                RemoveOnFinished = true,
                Permanent = true,
                CloneOnApply = true
            });


            frameB = new Console(100, 40);
            Children.Add(frameB);


            stars = new List<Star>();
            for (var i = 0; i < NUMOFSTARS; i++)
            {
                float xi = rnd.Next(-10, 11);
                float yi = rnd.Next(-5, 6);
                float zi = rnd.Next(1, 9);
                var ssst = new Star();
                ssst.x = xi;
                ssst.y = yi;
                ssst.z = zi;
                stars.Add(ssst);

            };

     


            float x = 0.00f;
            var ProgressTimer2 = new Timer(TimeSpan.FromSeconds(0.15));
            ProgressTimer2.TimerElapsed += (timer, e) =>
            {
                frameB.Clear();
                Szamol();
                x += 0.15f;


            };
            Components.Add(ProgressTimer2);

            InstructionSet animation = new InstructionSet()
                .Wait(TimeSpan.FromSeconds(0.3d))
                .Instruct(new DrawString(Surfaceszovegintro, logoText)
                {
                    Position = new Point(0, 0),
                    TotalTimeToPrint = 3f
                })
                ;



            Components.Add(animation);

            if (x == 8) { }

        }





        public void Szamol()
        {

            for (int i = 0; i < stars.Count; i++)
            {
                Star saa = stars[i];
                saa.z -= 0.2f;
                float k = 12.0f / saa.z;

                saa.xpos = saa.x * k + 50;
                saa.ypos = saa.y * k + 20;
                if (saa.xpos == 50) { saa.x = 1; }
                if (saa.ypos == 20) { saa.y = 1; }


                if (saa.xpos < 0 || saa.ypos < 0)
                {
                    saa.x = rnd.Next(-5, 5);
                    saa.y = rnd.Next(-3, 3);
                    saa.z = rnd.Next(1, 8);
                }


                if (saa.z >= 6)
                {
                    frameB.SetGlyph((int)saa.xpos, (int)saa.ypos, 7);
                }
                if (saa.z >= 5 && saa.z < 6)
                {
                    frameB.SetGlyph((int)saa.xpos, (int)saa.ypos, 9);
                }
                if (saa.z >= 3 && saa.z < 5)
                {
                    frameB.SetGlyph((int)saa.xpos, (int)saa.ypos, 42);
                }
                if (saa.z < 3)
                {
                    frameB.SetGlyph((int)saa.xpos, (int)saa.ypos, 15);
                }

            };
        }
        public override void Update(TimeSpan delta)
        {
            if (IsVisible==false)
            {
                return;
            }

            base.Update(delta);
        }

    }
}
