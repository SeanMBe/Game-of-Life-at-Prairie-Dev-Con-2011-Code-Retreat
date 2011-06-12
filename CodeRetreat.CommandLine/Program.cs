using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CodeRetreat.Core;
using MavenThought.Commons;

namespace CodeRetreat.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Pair<int,int>> liveCells;
            var gameBoard = new GameBoard(liveCells);
            while(true)
            {
                PrintBoard(gameBoard);
                Thread.Sleep(500);
                Console.Clear();
            }
        
        }

        private static void PrintBoard(GameBoard gameBoard)
        {
            
        }
    }
}
