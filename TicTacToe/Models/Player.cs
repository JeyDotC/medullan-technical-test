using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Models
{
    public enum Symbols
    {
        None,
        X = 'X',
        O = 'O'
    }
    public class Player
    {
        public string Name { get; set; }
        public Symbols Symbol { get; set; }
    }
}
