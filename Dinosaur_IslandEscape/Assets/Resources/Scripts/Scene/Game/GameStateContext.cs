
namespace JongJin
{
    // TODO<이종진> - 돌발 미션 이름 및 통일성 수정 필요 - 20241110
    public enum EGameState
    {
        CUTSCENE,
        RUNNING,
        TAILMISSION,
        FIRSTMISSION,
        SECONDMISSION,
        THIRDMISSION,

        END
    }

    public class GameStateContext
    {
        public IGameState CurrentState { get; set; }
        private readonly GameSceneController controller;
        public GameStateContext(GameSceneController controller)
        {
            this.controller = controller;
        }

        public void Transition(IGameState gameState)
        {
            if (CurrentState != null)
                CurrentState.ExitState();
            CurrentState = gameState;

            CurrentState.EnterState();
        }
    }
}