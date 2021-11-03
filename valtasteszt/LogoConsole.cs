
using StarterProject.Art;
using Microsoft.Xna.Framework;
using SadConsole;

namespace StarterProject
{
    public class LogoConsole : Console
    {
        public Console borderSurface { get; set; }
        public LogoConsole(int width, int height) : base(width, height)
        {

            UsePrintProcessor = true;
            const string footer = "PéNZENERGIA GENERáTOR";
            Cursor.Position = new Point(0, 0);

            borderSurface = new Console(width + 2, height + 2, Font);
            borderSurface.DrawBox(new Rectangle(0, 0, borderSurface.Width, borderSurface.Height),
                                  new Cell(Color.DarkCyan, Color.Black), null, ConnectedLineThick);
            borderSurface.Position = new Point(-1, -1);
            borderSurface.Print(width / 2 - footer.Length - 2, height + 1, footer, Color.DarkCyan, Color.Black);

            Children.Add(borderSurface);
            string x = (Ascii.Logo[1].Length.ToString());
            for (var i = 0; i < Ascii.Logo.Length; i++)
            {
                var str = Ascii.Logo[i];
                Print(0, i, $"[c:g b:0,0,0:DarkCyan:LightSkyBlue:White:LightSkyBlue:DarkCyan:0,0,0:{str.Length}][c:g f:White:Black:Red:Black:White:{str.Length}]" + str, Color.Cyan, Color.Black);
            }
            
        }
    }
}


