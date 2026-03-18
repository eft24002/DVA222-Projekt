using System;
using System.Collections.Generic;

namespace DVA222_Projekt
{
    class ConnectFourLogic
    {
        private int[,] board;
        private int rows;
        private int columns;

        public int Rows
        {
            get {return rows;}
        }

        public int Columns
        {
            get { return columns;}
        }

        public ConnectFourLogic(int rows = 9, int columns = 9)
        {
            this.rows = rows;
            this.columns = columns;
            board = new int[rows, columns];
        }

        public int GetCell(int row, int column)
        {
            return board[row, column];
        }

        public void ClearBoard()
        {
            board = new int[rows, columns];
        }

        public bool IsBoardFull()
        {
            for (int c= 0; c < columns; c++)
            {
                if (!IsColumnFull(c))
                    return false;
            }
            return true;
        }

        public bool IsColumnFull(int column)
        {
            if ( column < 0 || column >= columns)
                return true;
            
            return board[0, column] != 0;
        }

        public int DropPiece(int column, int player)
        {
            if (column < 0 || column >= columns)
                return -1;

            for (int row = rows -1; row >= 0; row--)
            {
                if (board[row, column] == 0)
                {
                    board[row, column] = player;
                    return row;
                }
            }
            return -1;
        }

        public List<int> GetValidColumns()
        {
            List<int> validColumns = new List<int>();

            for (int c = 0; c < columns; c++)
            {
                if (!IsColumnFull(c))
                    validColumns.Add(c);    
            }

            return validColumns;
        }

        public int GetRandomValidColumn(Random random)
        {
            List<int> validColumns = GetValidColumns();

            if ( validColumns.Count == 0)
                return -1;

            int index = random.Next(validColumns.Count);
            return validColumns[index];
        }

        public bool CheckWin(int row, int column, int player)
        {
            if (CountInDirection(row, column, 0, 1, player) + CountInDirection(row, column, 0, -1, player) - 1 >= 4)
                return true;

            if (CountInDirection(row, column, 1, 0, player) + CountInDirection(row, column, -1, 0, player) - 1 >= 4)
                return true;

            if (CountInDirection(row, column, 1, 1, player) + CountInDirection(row, column, -1, -1, player) - 1 >= 4)
                return true;

            if (CountInDirection(row, column, 1, -1, player) + CountInDirection(row, column, -1, 1, player) - 1 >= 4)
                return true;
            
            return false;
        }

        private int CountInDirection(int startRow, int startColumn, int rowStep, int columnStep, int player)
        {
            int count = 0;
            int row = startRow;
            int column = startColumn;

            while ( row >= 0 && row < rows && column >= 0 && column < columns && board[row, column] == player)
            {
                count++;
                row += rowStep;
                column += columnStep;
            }

            return count;
        }
    }
    
}