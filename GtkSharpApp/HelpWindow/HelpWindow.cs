using Gtk;
using Cairo;

/*
Visar bara vilka färger som är ok, allt annat blir svart...
*/

namespace DVA222_Projekt
{
    class HelpWindow : Window
    {
        public HelpWindow() : base("Help")
        {
            SetDefaultSize(50, 250);
            DeleteEvent += delegate { Application.Quit(); };

            Label helpText = new Label("Supported colors:\n Red\n Green\n Blue\n Yellow\n Cyan\n Magenta\n Black\n White");
            helpText.LineWrap = true;
            helpText.Xalign = 0;
            Add(helpText);

            ShowAll();
        }
    }
}