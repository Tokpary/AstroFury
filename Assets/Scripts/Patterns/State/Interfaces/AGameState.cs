using Scripts.Patterns.State.Components;

namespace Patterns.State.Interfaces
{
    public abstract class AGameState : IState
    {
        protected IGameState _gameState;
        
        public AGameState(IGameState gameState)
        {
            this._gameState = gameState;
        }

        public abstract void Enter(GameManager gameManager);

        public abstract void Exit(GameManager gameManager);
        
        public abstract void Update();

    }
}