using System;
using Microsoft.Xna.Framework;
using System.Globalization;
using SadConsole.StringParser;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Components;
using SadConsole.Input;
using Console = SadConsole.Console;
using SadConsole.Themes;

namespace StarterProject
{

    public class PlazmaConsole : Console
    {
        public Console frame { set; get; }
        public int fee;

        public PlazmaConsole() : base(100, 40)
        {
            frame = new Console(100, 40);
            //szélesség
            //for (int x = 0; x < 30; x++)
            //{
                fee = 0;
                var ProgressTimer = new Timer(TimeSpan.FromSeconds(0.005d));
                ProgressTimer.TimerElapsed += (timer, e) => { fee++; };
                Components.Add(ProgressTimer);
                Children.Add(frame);
            //}

        }
        public double dist(double a, double b, double c, double d)
        {
            double disti = Math.Sqrt((double)((a - c) * (a - c) + (b - d) * (b - d)));
            return disti;
        }

        public override void Update(TimeSpan delta)
        {
            UpdateScreen();

            base.Update(delta);
        }
        public void UpdateScreen()
        {
            for (int x = 0; x < 100; x++)
            {
                //magasság
                for (int y = 0; y < 40; y++)
                {
                    float timex = (float)fee / 5.0f;

                    double value = Math.Sin(dist(x + timex, y, 128.0, 128.0) / 8.0)
                     + Math.Sin(dist(x, y, 64.0, 64.0) / 8.0)
                    + Math.Sin(dist(x, y + timex / 7, 192.0, 64) / 7.0)
                     + Math.Sin(dist(x, y, 192.0, 100.0) / 8.0);
                    int color = (int)((4 + value)) * 32;

                    //Color myRgbColor = new Color(color, color, color);
                    Color myRgbColor = new Color(color, color * 2, 255 - color);

                    int character = 0;
                    float bright = myRgbColor.GetBrightness();
                    if (bright < 0.6) { character = 36; }
                    if (bright >= 0.6 && bright <= 0.7) { character = 42; }
                    if (bright > 0.7 && bright <= 1.0) { character = 9; }

                    frame.SetGlyph(x, y, character);

                    frame.SetForeground(x, y, myRgbColor);
                }

            }
        }
    }
}






