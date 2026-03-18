using Gtk;
using GLib;
using System;
using System.Collections.Generic;

namespace DVA222_Projekt
{
    class GameBoardWindow : Window
    {
        private Grid grid;
        private ConnectFourLogic game;
        private Random random;
        private bool humanTurn;
        private bool gameOver;

        private const int rows = 9;
        private const int columns = 9;
        private const int cellSize = 60;

        public GameBoardWindow() : base("Connect Four")
        {
            random = new Random();
            game = new ConnectFourLogic(rows, columns);
            grid = new Grid(rows, columns, cellSize, "white", "black");

            SetDefaultSize(columns * cellSize, rows * cellSize);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Gtk.Application.Quit(); };

            Add(grid);
            ShowAll();

            KeyPressEvent += OnKeyPress;

            humanTurn = random.Next(2) == 0;
            gameOver = false;

            if (!humanTurn)
                ComputerMove();
        }

        private void OnKeyPress(object o, KeyPressEventArgs args)
        {
            if (gameOver)
                return;

            if (args.Event.Key == Gdk.Key.Escape)
            {
                Gtk.Application.Quit();
                return;
            }

            if(!humanTurn)
                return;

            int column = -1;

            switch (args.Event.Key)
            {
                case Gdk.Key.Key_1:
                case Gdk.Key.KP_1:
                column = 0;
                break;
                case Gdk.Key.Key_2:
                case Gdk.Key.KP_2:
                column = 1;
                break;
                case Gdk.Key.Key_3:
                case Gdk.Key.KP_3:
                column = 2;
                break;
                case Gdk.Key.Key_4:
                case Gdk.Key.KP_4:
                column = 3;
                break;
                case Gdk.Key.Key_5:
                case Gdk.Key.KP_5:
                column = 4;
                break;
                case Gdk.Key.Key_6:
                case Gdk.Key.KP_6:
                column = 5;
                break;
                case Gdk.Key.Key_7:
                case Gdk.Key.KP_7:
                column = 6;
                break;
                case Gdk.Key.Key_8:
                case Gdk.Key.KP_8:
                column = 7;
                break;
                case Gdk.Key.Key_9:
                case Gdk.Key.KP_9:
                column = 8;
                break;
            }

            if(column != -1)
                HumanMove(column);
        }

        private void HumanMove(int column)
        {
            int row = game.DropPiece(column, 1);

            if (row == -1)
            {
                ShowMessage("That column is full. choose another column.");
                return;
            }

            grid.SetCell(row, column, "red");

            if (game.CheckWin(row, column, 1))
            {
                gameOver = true;
                ShowMessage("You win!");
                return;
            }

            if (game.IsBoardFull())
            {
                gameOver = true;
                ShowMessage("Draw!");
                return;
            }

            humanTurn = false;
            ComputerMove();
        }

        private void ComputerMove()
        {
            if (gameOver)
                return;

            int column = game.GetRandomValidColumn(random);

            if (column == -1)
            {
                gameOver = true;
                ShowMessage("Draw!");
                return;
            }

            int row = game.DropPiece(column, 2);

            if (row == -1)
                return;

            grid.SetCell(row, column, "blue");

            if (game.CheckWin(row, column, 2))
            {
                gameOver = true;
                ShowMessage("Computer wins!");
                return;
            }

            if (game.IsBoardFull())
            {
                gameOver = true;
                ShowMessage("Draw!");
                return;
            }

            humanTurn = true;
        }

        private void ShowMessage(string message)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, message);
            md.Run();
            md.Destroy();
        }
    }
}