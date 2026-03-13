using System;
using System.ComponentModel;
using Gtk;

/*
Tänkte att vi kan först ta upp ett fönster för att 
be om alla input till spelet, som sedan används för
att skapa ett nytt GameWindow.
*/

namespace DVA222_Projekt
{
    class InputWindow : Window
    {
       public List<SettingsField> questions = new List<SettingsField>();

        public InputWindow() : base("Connect Four - Settings")
        {
            SetDefaultSize(500,400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };
            VBox vbox = new VBox(false, 10);

            Label title = new Label("Welcome to Connect Four! Please select your settings.");
            title.Xalign = 0;
            vbox.PackStart(title, false, false, 0);

            questions.Add(new SettingsField("Enter number of rows:"));
            questions.Add(new SettingsField("Enter number of columns:"));
            questions.Add(new SettingsField("Enter number of pixels in each cell:"));
            questions.Add(new SettingsField("Enter desired background color:"));
            questions.Add(new SettingsField("Enter the desired color of the lines:"));   

            foreach (SettingsField s in questions)
            {
                s.AskQuestion(vbox);
            }

            Alignment filler = new Alignment(0, 0, 1, 1);
            vbox.PackStart(filler, true, true, 0);

            HBox hbox = new HBox(false, 5);

            Button next = new Button("Start game->");
            next.SetSizeRequest(80, 35);
            next.Clicked += OnNextClicked;
            hbox.PackEnd(next, false, false, 10);
            vbox.PackEnd(hbox, false, false, 10);

            Add(vbox);
            ShowAll();    
        }
        public void OnNextClicked(object sender, EventArgs e)
        {
            int rows = questions[0].GetIntAnswer();
            int columns = questions[1].GetIntAnswer();   
            int cellSize = questions[2].GetIntAnswer();
            string backgroundColor = questions[3].GetAnswer();   
            string lineColor = questions[4].GetAnswer();

            BoardWindow gameBoard = new BoardWindow(rows, columns, cellSize, backgroundColor, lineColor);
            gameBoard.ShowAll();

            Window inputWindow = (Window)((Button)sender).Toplevel;
            inputWindow.Destroy();
        }
    }
}