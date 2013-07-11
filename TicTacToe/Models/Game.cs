using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
    public class Game
    {
        public string Name { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Symbols CurrentPlayer { get; set; }

        public Game()
        {
            CurrentPlayer = Symbols.X;
        }

        public List<List<Player>> Board { get; set; }

        public bool IsFull()
        {
            return Board.TrueForAll(l => l.TrueForAll(p => p != null));
        }

        public Symbols GetWinner()
        {
            if (SymbolWinsRow(Symbols.X))
                return Symbols.X;
            if (SymbolWinsRow(Symbols.O))
                return Symbols.O;

            if (SymbolWinsColumn(Symbols.X))
                return Symbols.X;
            if (SymbolWinsColumn(Symbols.O))
                return Symbols.O;

            if (WinsMainDiagonal(Symbols.X))
                return Symbols.X;
            if (WinsMainDiagonal(Symbols.O))
                return Symbols.O;

            if (WinsSecondaryDiagonal(Symbols.X))
                return Symbols.X;
            if (WinsSecondaryDiagonal(Symbols.O))
                return Symbols.O;

            return Symbols.None;
        }

        private bool SymbolWinsRow(Symbols symbol)
        {
            return Board.Exists(row => row.TrueForAll(p => p != null && p.Symbol == symbol));
        }

        private bool SymbolWinsColumn(Symbols symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board.TrueForAll(row => row[i] != null && row[i].Symbol == symbol))
                {
                    return true;
                }
            }
            return false;
        }

        private bool WinsMainDiagonal(Symbols symbol)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Board[i][i] == null || Board[i][i].Symbol != symbol)
                {
                    return false;
                }
            }

            return true;
        }

        private bool WinsSecondaryDiagonal(Symbols symbol)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Board[i][2 - i] == null || Board[i][2 - i].Symbol != symbol)
                {
                    return false;
                }
            }

            return true;
        }

        public static
            List<List<Player>> CreateDashboard()
        {
            return new List<List<Player>>
                {
                    new List<Player>{null, null, null},
                    new List<Player>{null, null, null},
                    new List<Player>{null, null, null},
                };
        }
    }
}