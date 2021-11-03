using System.Collections.Generic;
using System.Linq;

namespace StarterProject
{
    public static class UserInterfaceManager
    {

        public static void Initialize()
        {
            //// Initialize game window, set's the Global.CurrentScreen
            //var _gamePlay = new GamePlay();
            //Add(_gamePlay);

            //// Initialize map

        }
        private static readonly List<IUserInterface> Interfaces = new List<IUserInterface>();
        public static T Get<T>() where T : IUserInterface
        {
            return Interfaces.OfType<T>().SingleOrDefault();
        }
        public static void Add<T>(T userInterface) where T : IUserInterface
        {
            Interfaces.Add(userInterface);
        }
        public static void Remove<T>(T userInterface) where T : IUserInterface
        {
            Interfaces.Remove(userInterface);
        }
        public static IEnumerable<T> GetAll<T>()
        {
            return Interfaces.OfType<T>().ToArray();
        }
    }
}
