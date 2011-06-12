using System.Collections.Generic;
using MavenThought.Commons;
using MavenThought.Commons.Extensions;
using MavenThought.Commons.Testing;
using SharpTestsEx;

namespace CodeRetreat.Core.Test
{
    public class When_game_board_evolves_a_live_cell_with_three_neighbours : GameBoardSpecification
    {
        private GameBoard _nextGeneration;

        private int _expectedX;

        private int _expectedY;

        private IEnumerable<Pair<int, int>> _startCells;

        protected override void GivenThat()
        {
            var randomGenerator = new RandomGenerator();

            _expectedX = randomGenerator.Generate<int>();
            _expectedY = randomGenerator.Generate<int>();

            _startCells = Enumerable.Create(new Pair<int, int>(_expectedX, _expectedY), 
                new Pair<int, int>(_expectedX - 1, _expectedY), 
                new Pair<int, int>(_expectedX + 1, _expectedY), 
                new Pair<int, int>(_expectedX, _expectedY - 1));
        }

        protected override GameBoard CreateSut()
        {
            return new GameBoard(_startCells);
        }

        protected override void WhenIRun()
        {
            _nextGeneration = Sut.Evolve();
        }

        [It]
        public void Should_have_that_cell_alive()
        {
            _nextGeneration.StatusOfCell(_expectedX, _expectedY).Should().Be.True();
        }
    }
}