using Gtk;
using GLib;
using System;
using System.Collections.Generic;

/*
Gör spelbrädet
*/

namespace DVA222_Projekt
{
    class BoardWindow : Window
    {

        private Grid grid;
        private int rows;
        private int columns;
        private Random random;

        public BoardWindow(int rows, int columns, int cellSize, string backgroundColor, string lineColor) : base("Connect Four")
        {
            this.rows = rows;
            this.columns = columns;
            this.random = new Random();

            SetDefaultSize(columns * cellSize , rows * cellSize + 100);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Gtk.Application.Quit(); };

            grid = new Grid(rows, columns, cellSize, backgroundColor, lineColor);

            Add(grid);
            ShowAll();

            Timeout.Add(500, OnTimerTick);
        }

        private bool OnTimerTick()
        {
            if (grid.IsFull())
            {
                grid.ClearCircles();
                return true;
            }

            List<(int row, int col)> emptyCells = new List<(int, int)>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (grid.IsCellEmpty(r, c))
                    {
                        emptyCells.Add((r, c));
                    }
                }
            }
            if (emptyCells.Count > 0)
            {
                int index = random.Next(emptyCells.Count);
                var cell = emptyCells[index];
                grid.DrawCircleInCell(cell.row, cell.col, "red");
            }
            return true;
        }
    }
}