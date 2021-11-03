using Microsoft.Xna.Framework;

namespace StarterProject
{
    
        public interface IUserInterface
        {
            Point Position { get; set; }
            bool IsVisible { get; set; }
            bool IsDirty { get; set; }
            SadConsole.Console Console { get; }
        }
   
}
