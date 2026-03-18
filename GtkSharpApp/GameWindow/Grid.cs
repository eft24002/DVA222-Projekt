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
        private int rows;
        private int columns;
        private int cellSize;
        private double[] backgroundColor;
        private double[] lineColor;
        private string[,] circles;

        public Grid(int rows, int columns, int cellSize, string backgroundColor, string lineColor)
        {
            this.rows = rows;
            this.columns = columns;
            this.cellSize = cellSize;
            this.backgroundColor = new GetColor(backgroundColor).RGB;
            this.lineColor =  new GetColor(lineColor).RGB;

            circles = new string[rows, columns];

            SetSizeRequest(cellSize*columns, cellSize*rows);
        }

        public void SetCell(int row, int column, string color)
        {
            if (row < 0 || row >= rows || column < 0 || column >= columns)
                return;

            circles[row, column] = color;
            QueueDraw();
        }

        public void ClearCircles()
        {
            circles = new string[rows, columns];
            QueueDraw();
        }

        public bool IsCellEmpty(int row, int column)
        {
            if (row < 0 || row >= rows || column < 0 || column >= columns)
                return false;

            return circles[row, column] == null;
        }

        public bool IsFull()
        {
            for (int r= 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (circles[r, c] == null)
                        return false;
                }
            }
            return true;
        }

        protected override bool OnDrawn(Context cr)
        {
            cr.SetSourceRGB(backgroundColor[0], backgroundColor[1], backgroundColor[2]);
            cr.Rectangle(0, 0, columns * cellSize, rows * cellSize);
            cr.Fill();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (circles[r, c] != null)
                    {
                        double[] circleColor = new GetColor(circles[r, c]).RGB;

                        double centerX = c * cellSize + cellSize /2.0;
                        double centerY = r * cellSize + cellSize /2.0;
                        double radius = cellSize * 0.4;

                        cr.SetSourceRGB(circleColor[0], circleColor[1], circleColor[2]);
                        cr.Arc(centerX, centerY, radius, 0, 2 * System.Math.PI);
                        cr.Fill();
                    }
                }
            }

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