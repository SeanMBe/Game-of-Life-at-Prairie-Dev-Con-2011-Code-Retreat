using System;
using System.Collections.Generic;
using CodeRetreat.Core;
using MavenThought.Commons;
using MavenThought.Commons.Extensions;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace CodeRetreat.Acceptance.Tests.Steps
{
    [Binding]
    public class GameSteps : BaseSteps
    {
        [Binding]
        public class StepDefinitions
        {
            [Given(@"the following setup")]
            public void GivenTheFollowingSetup(Table table)
            {
                var gameBoardInitialState = ParseGameBoard(table);

                CurrentGameBoard = new GameBoard(gameBoardInitialState);
            }

            private IEnumerable<Pair<int, int>> ParseGameBoard(Table table)
            {
                var gameBoardInitialState = new List<Pair<int, int>>();

                table.Rows.ForEach((y, r) => Parse(r, y).ForEach(gameBoardInitialState.Add));
                return gameBoardInitialState;
            }

            [When(@"I evolve the board")]
            public void WhenIEvolveTheBoard()
            {
                CurrentGameBoard = CurrentGameBoard.Evolve();
            }

            [Then(@"the center cell should be dead")]
            public void ThenTheCenterCellShouldBeDead()
            {
                CurrentGameBoard.StatusOfCell(1, 1).Should().Be.False();
            }

            [Then(@"the center cell should be alive")]
            public void ThenTheCenterCellShouldBeAlive()
            {
                CurrentGameBoard.StatusOfCell(1, 1).Should().Be.True();
            }

            [Then(@"I should see the following board")]
            public void ThenIShouldSeeTheFollowingBoard(Table table)
            {
                var liveCells = ParseGameBoard(table);

                var width = table.Header.Count() - 1;
                var height = table.Rows.Count() - 1;

                var expectedStateOfCell = new Func<int, int, bool>((x,y) => 
                    liveCells.Find(c => c.First == x && c.Second == y) == null ? false : true);

                width.Times(x => height.Times(y => 
                    CurrentGameBoard.StatusOfCell(x, y).Should().Be.EqualTo(expectedStateOfCell(x,y))));
            }

            private static IEnumerable<Pair<int, int>> Parse(TableRow tableRow, int y)
            {
                var cells = new List<Pair<int, int>>();

                tableRow.ForEach((x, cell) =>
                                     {
                                         if (cell.Value != ".")
                                         {
                                             cells.Add(new Pair<int, int>(x, y));
                                         }
                                     });


                return cells;
            }
        }
    }
}