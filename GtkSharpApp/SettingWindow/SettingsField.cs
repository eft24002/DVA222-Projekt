using Gtk;

/*
Grund för specifikationer till brädet, används i InputWindows.cs
*/

namespace DVA222_Projekt
{
    class SettingsField 
    {
        public string Question { get; set; }
        public Entry Choice { get; set; }
        
        public SettingsField(string question)
        {
            Question = question;
            Choice = new Entry();
        }
        public void AskQuestion(VBox vbox)
        {
            Label qLabel = new Label(Question);
            qLabel.Xalign = 0;
            vbox.PackStart(qLabel, false, false, 0);
            vbox.PackStart(Choice, false, false, 0);
        }
        public string GetAnswer()
        {
            return Choice.Text;
        } 
        public int GetIntAnswer()
        {
            return int.Parse(Choice.Text);
        }
    }
}   