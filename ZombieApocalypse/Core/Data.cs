using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse.Core
{

    public static class Data
    {
        public static int ScreenW { get; set; } = 1920;

        public static int ScreenH { get; set; } = 1080;

        public static bool Exist { get; set; } = false;


        public enum Scenes { Menu, Game, Settings }
        public static Scenes CurrentState { get; set; } = Scenes.Menu;
    }
}
