using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SadConsole;
using SadConsole.DrawCalls;
using SadConsole.Effects;
using SadConsole.Instructions;
using Console = SadConsole.Console;

namespace StarterProject
{
    class VagoScreen : ScrollingConsole, IUserInterface
    {
        public Console Console => this;

        public Action SplashCompleted { get; set; }

        private readonly Console _consoleImage;
        private readonly Point _consoleImagePosition;


        public VagoScreen()
            : base(100, 40)
        {
            Cursor.IsEnabled = false;
            IsVisible = true;


            using (System.IO.Stream imageStream = Microsoft.Xna.Framework.TitleContainer.OpenStream("asciivagopng.png"))
            {
                using (var image = Texture2D.FromStream(Global.GraphicsDevice, imageStream))
                {
                    CellSurface logo = image.ToSurface(Global.FontDefault, false);

                    _consoleImage = Console.FromSurface(logo, Global.FontDefault);
                    _consoleImagePosition = new Point(Width / 2 - _consoleImage.Width / 2, -1);
                    _consoleImage.Tint = Color.Black;
                }
            }



        }

        public override void Update(TimeSpan delta)
        {
            if (!IsVisible)
            {
                return;
            }

            base.Update(delta);
        }

        public override void Draw(TimeSpan delta)
        {
            // Draw the logo console...
            if (IsVisible)
            {
                Renderer.Render(_consoleImage);
                Global.DrawCalls.Add(new DrawCallScreenObject(_consoleImage, _consoleImagePosition, false));

                base.Draw(delta);
            }
        }

        public void vagoVillan()
        {
            InstructionSet animation = new InstructionSet()



                               // Fade in the logo
                               .Instruct(new FadeTextSurfaceTint(_consoleImage,
                                                                 new ColorGradient(Color.Black, Color.Transparent),
                                                                 TimeSpan.FromSeconds(2)))

                               // Delay so blinking effect is seen
                               .Wait(TimeSpan.FromSeconds(2.5d))

                               // Fade out main console and logo console.
                               .InstructConcurrent(new FadeTextSurfaceTint(_consoleImage,
                                                                 new ColorGradient(Color.Transparent, Color.Black),
                                                                 TimeSpan.FromSeconds(2)),

                                                   new FadeTextSurfaceTint(this,
                                                                 new ColorGradient(Color.Transparent, Color.Black),
                                                                 TimeSpan.FromSeconds(1.0d)))

                               // Animation has completed, call the callback this console uses to indicate it's complete
                               .Code((con, delta) => { SplashCompleted?.Invoke(); return true; })
                           ;

            Components.Add(animation);
        }



    }

}

