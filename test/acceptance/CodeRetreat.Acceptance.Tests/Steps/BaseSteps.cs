using CodeRetreat.Core;
using TechTalk.SpecFlow;

namespace CodeRetreat.Acceptance.Tests.Steps
{
    public class BaseSteps
    {

        public static GameBoard  CurrentGameBoard
        {
            get { return ScenarioContext.Current.Get<GameBoard>(); }
            set { ScenarioContext.Current.Set(value); }
        }
    }
}