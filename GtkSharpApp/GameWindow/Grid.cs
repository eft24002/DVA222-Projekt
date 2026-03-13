using Gtk;
using Cairo;

/*
Bestämmer vart linjer ska dras för att 
skapa ett rutnät från inputs i GameBoardSettings.cs
*/

namespace DVA222_Projekt
{
    class Grid : DrawingArea
    {
        int rows;
        int columns;
        int cellSize;
        double[] backgroundColor;
        double[] lineColor;

        public Grid(int rows, int columns, int cellSize, string backgroundColor, string lineColor)
        {
            this.rows = rows;
            this.columns = columns;
            this.cellSize = cellSize;
            this.backgroundColor = new GetColor(backgroundColor).RGB;
            this.lineColor =  new GetColor(lineColor).RGB;

            SetSizeRequest(cellSize*columns, cellSize*rows);
        }
        protected override bool OnDrawn(Context cr)
        {
            cr.SetSourceRGB(backgroundColor[0], backgroundColor[1], backgroundColor[2]);
            cr.Rectangle(0, 0, columns * cellSize, rows * cellSize);
            cr.Fill();

            cr.SetSourceRGB(lineColor[0], lineColor[1], lineColor[2]);
            cr.LineWidth = 2;

            for (int r = 0; r <= rows; r++)
            {
                cr.MoveTo(0, r * cellSize);
                cr.LineTo(columns * cellSize, r * cellSize);
            }

            for (int c = 0; c <= columns; c++)
            {
                cr.MoveTo(c * cellSize, 0);
                cr.LineTo(c * cellSize, rows * cellSize);
            }
            cr.Stroke();
            return true;
        }
    }
}