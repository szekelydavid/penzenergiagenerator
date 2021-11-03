using Microsoft.Xna.Framework;
using ScrollingConsole = SadConsole.ScrollingConsole;

namespace StarterProject
{
    internal class HeaderConsole : ScrollingConsole
    {
        public HeaderConsole() : base(Constants.GameWindowWidth, 2)
        {
            DefaultBackground = Color.Transparent;
            DefaultForeground = Color.Yellow;
        }

        public void SetConsole(string title, string summary)
        {
            Fill(Color.Yellow, Color.DarkBlue, 0);
            Print(1, 0, title.ToUpper(), Color.Yellow);
            Print(1, 1, summary, Color.Gray);
        }
    }
}
