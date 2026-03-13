using System;
using Gtk;

namespace DVA222_Projekt
{
    class Program 
    {
        public static void Main()
        {
            Application.Init();
            new InputWindow();
            new HelpWindow();
            Application.Run();
        }
    }
}