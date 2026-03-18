using System;
using System.ComponentModel;
using Gtk;
using System.Collections.Generic;

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

        public InputWindow() : base("Choose mode")
        {
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };
            Box vbox = new Box(Orientation.Vertical, 10);

            Label title = new Label("Welcome to Connect Four! Please select your settings.");
            title.Xalign = 0;
            vbox.PackStart(title, false, false, 0);

            questions.Add(new SettingsField("Enter number of rows (animation):"));
            questions.Add(new SettingsField("Enter number of columns (animation):"));
            questions.Add(new SettingsField("Enter number of pixels in each cell:"));
            questions.Add(new SettingsField("Enter desired background color:"));
            questions.Add(new SettingsField("Enter the desired color of the lines:"));   

            foreach (SettingsField s in questions)
            {
                s.AskQuestion(vbox);
            }

            Box filler = new Box(Orientation.Vertical, 0);
            vbox.PackStart(filler, true, true, 0);

            Box hbox = new Box(Orientation.Horizontal, 10);

            Button animationButton = new Button("Run animation");
            animationButton.SetSizeRequest(120, 35);
            animationButton.Clicked += OnAnimationClicked;
            hbox.PackEnd(animationButton, true, true, 0);

            Button gameButton = new Button("Run game (9x9)");
            gameButton.SetSizeRequest(120, 35);
            gameButton.Clicked += OnGameClicked;
            hbox.PackEnd(gameButton, true, true, 0);

            vbox.PackEnd(hbox, false, false , 10);

            Add(vbox);
            ShowAll();    
        }
        public void OnAnimationClicked(object sender, EventArgs e)
        {
            int rows = questions[0].GetIntAnswer();
            int columns = questions[1].GetIntAnswer();   
            int cellSize = questions[2].GetIntAnswer();
            string backgroundColor = questions[3].GetAnswer();   
            string lineColor = questions[4].GetAnswer();

            if (rows <= 0 || columns <= 0 || cellSize <= 0)
            {
                ShowError("Rows, columns and cell size must be greater than 0.");
                return;
            }

            BoardWindow animationWindow = new BoardWindow(rows, columns, cellSize, backgroundColor, lineColor);
            animationWindow.ShowAll();
            Destroy();
        }

        public void OnGameClicked(object sender, EventArgs e)
        {
            GameBoardWindow gameWindow = new GameBoardWindow();
            gameWindow.ShowAll();
            Destroy();
        }

        private void ShowError(string message)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
            md.Run();
            md.Destroy();
        }
    }
}