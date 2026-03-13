using Gtk;
using Cairo;

/*
Gör spelbrädet
*/

namespace DVA222_Projekt
{
    class BoardWindow : Window
    {
        public BoardWindow(int rows, int columns, int cellSize, string backgroundColor, string lineColor) : base("Connect Four")
        {
            SetDefaultSize(columns * cellSize , rows * cellSize + 100);
        
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };

            Grid grid = new Grid(rows, columns, cellSize, backgroundColor, lineColor);

            Add(grid);
            ShowAll();
        }
    }
}