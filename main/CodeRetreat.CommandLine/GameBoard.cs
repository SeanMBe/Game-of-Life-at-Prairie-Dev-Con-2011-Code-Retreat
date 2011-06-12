using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons;
using MavenThought.Commons.Extensions;

namespace CodeRetreat.Core
{
    public class GameBoard
    {
        private readonly IEnumerable<Pair<int, int>> _liveCells;

        public GameBoard(IEnumerable<Pair<int, int>> liveCells)
        {
            _liveCells = liveCells;
        }

        public GameBoard Evolve()
        {
            var nextGenerationOfCells = EvolveCells(_liveCells);
            var nextGenerationOfGameBoard = new GameBoard(nextGenerationOfCells);

            return nextGenerationOfGameBoard;
        }

        public bool StatusOfCell(int x, int y)
        {
            return _liveCells.Where(c => c.First == x && c.Second == y).FirstOrDefault() != null;
        }

        

        private IEnumerable<Pair<int, int>> EvolveCells(IEnumerable<Pair<int, int>> liveCells)
        {
            var nextGeneration = new List<Pair<int, int>>();

            liveCells.ForEach(c =>
                                  {
                                      if (CellLives(c))
                                      {
                                        nextGeneration.Add(c);  
                                      }
                                  });

            var reproducedCells = Reproduce();

            return nextGeneration.Concat(reproducedCells);
        }

        private IEnumerable<Pair<int, int>> Reproduce()
        {
            var offspring = new List<Pair<int, int>>();

            _liveCells.ForEach(c => FindDeadNeighbours(c).ForEach(dn =>
                                            {
                                                if (FindLiveNeighbours(dn).Count() == 3 && offspring.Where(o => o.First == dn.First && o.Second == dn.Second).Count() == 0)
                                                {
                                                    offspring.Add(dn);
                                                }
                                            }
                                        ));
            return offspring;
        }


        private bool CellLives(Pair<int, int> cell)
        {
            var neighbours = FindLiveNeighbours(cell);
            
            return StatusOfCell(cell.First, cell.Second) && (neighbours.Count() == 2 || neighbours.Count() == 3);
        }

        private IEnumerable<Pair<int, int>> FindLiveNeighbours(Pair<int,int> cell)
        {
            return _liveCells.Where(c => (cell.First - 1 == c.First && cell.Second == c.Second)
                                  || (cell.First + 1 == c.First && cell.Second == c.Second)
                                  || (cell.First == c.First && cell.Second - 1 == c.Second)
                                  || (cell.First == c.First && cell.Second + 1 == c.Second)
                                  || (cell.First + 1 == c.First && cell.Second - 1 == c.Second)
                                  || (cell.First + 1 == c.First && cell.Second + 1 == c.Second)
                                  || (cell.First - 1 == c.First && cell.Second - 1 == c.Second)
                                  || (cell.First - 1 == c.First && cell.Second + 1 == c.Second));
        }

        private IEnumerable<Pair<int, int>> FindDeadNeighbours(Pair<int, int> cell)
        {
            var liveNeighbours = FindLiveNeighbours(cell);

            IEnumerable<Pair<int, int>> adjacentNeighbours = new List<Pair<int, int>>() {
                new Pair<int, int>(cell.First - 1, cell.Second),
                new Pair<int, int>(cell.First + 1, cell.Second),
                new Pair<int, int>(cell.First, cell.Second - 1),
                new Pair<int, int>(cell.First, cell.Second + 1),
                new Pair<int, int>(cell.First + 1, cell.Second - 1),
                new Pair<int, int>(cell.First + 1, cell.Second + 1),
                new Pair<int, int>(cell.First - 1, cell.Second - 1),
                new Pair<int, int>(cell.First - 1, cell.Second + 1)
            };

            
            var deadNeighbours = adjacentNeighbours.Where(an => !liveNeighbours.Contains(an) && (an.First != cell.First && an.Second != cell.Second));

            return deadNeighbours;
        }
    }
}