using Microsoft.Xna.Framework;
using ColorHelper = Microsoft.Xna.Framework.Color;
using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;
using Console = SadConsole.Console;

namespace StarterProject
{
    public static class Themebeallit 
    {
       
        public static Library SetupThemesA()
        {

            var consoleTheme = Library.Default.Clone();
            consoleTheme.Colors.ControlHostBack = Color.Transparent;
            consoleTheme.Colors.Text = Color.White;

            consoleTheme.ButtonTheme = new ButtonTheme
            {


                Colors = new Colors
                {
                    ControlBack = Color.Navy,
                    Text = Color.WhiteSmoke,
                    ControlBackLight= Color.Navy,
                    ControlBackSelected = Color.Yellow,
                    TextDark = Color.AntiqueWhite,
                    TextBright = Color.White,
                    TextFocused = Color.WhiteSmoke,
                    TextLight = Color.WhiteSmoke,
                    TextSelected = Color.Black,
                    TextSelectedDark = Color.Black,
                    TitleText = Color.WhiteSmoke,
                },
                //EndCharacterLeft = '*',
                //EndCharacterRight = '*',
                //ShowEnds = true
            };
            consoleTheme.ButtonTheme.Colors.RebuildAppearances();
            consoleTheme.Colors.RebuildAppearances();
            return consoleTheme;

        }

        public static Library SetupThemesMenu()
        {

            var consoleTheme = Library.Default.Clone();
            consoleTheme.Colors.ControlHostBack = Color.Transparent;
            consoleTheme.Colors.Text = Color.White;
            consoleTheme.Colors.ControlBack = Color.Black;
            consoleTheme.ButtonTheme = new ButtonLinesTheme
            {


                Colors = new Colors
                {
                    ControlBack = Color.Black,
                    Text = Color.WhiteSmoke,
                    ControlBackLight = Color.Black,
                    ControlBackSelected = Color.Blue,
                    TextDark = Color.AntiqueWhite,
                    TextBright = Color.White,
                    TextFocused = Color.WhiteSmoke,
                    TextLight = Color.WhiteSmoke,
                    TextSelected = Color.Yellow,
                    TextSelectedDark = Color.Yellow,
                    TitleText = Color.WhiteSmoke,
                },
                //EndCharacterLeft = '*',
                //EndCharacterRight = '*',
                //ShowEnds = true
            };
            consoleTheme.ButtonTheme.Colors.RebuildAppearances();
            consoleTheme.Colors.RebuildAppearances();
            return consoleTheme;

        }

        public static Library SetupThemesI()
        {

            var consoleTheme = Library.Default.Clone();
            consoleTheme.Colors.ControlHostBack = Color.Black;
            consoleTheme.Colors.Text = Color.White;

            consoleTheme.ButtonTheme = new ButtonTheme
            {


                Colors = new Colors
                {
                    ControlBack = Color.Black,
                    Text = Color.WhiteSmoke,
                    ControlBackLight = Color.Black,
                    ControlBackSelected = Color.Blue,
                    TextDark = Color.AntiqueWhite,
                    TextBright = Color.White,
                    TextFocused = Color.WhiteSmoke,
                    TextLight = Color.WhiteSmoke,
                    TextSelected = Color.Yellow,
                    TextSelectedDark = Color.Yellow,
                    TitleText = Color.WhiteSmoke,
                },
                //EndCharacterLeft = '*',
                //EndCharacterRight = '*',
                //ShowEnds = true
            };
            consoleTheme.ButtonTheme.Colors.RebuildAppearances();
            consoleTheme.Colors.RebuildAppearances();
            return consoleTheme;

        }
    }
}

