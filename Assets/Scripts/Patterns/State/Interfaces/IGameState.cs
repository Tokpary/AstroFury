using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Patterns.State.Interfaces
{
    public interface IGameState
    {
        
        public void StartGame();
        public void EndGame();
        public IState GetCurrentState();
        public void SetState(IState state);
        public void DisplayUpgrades();
        public void AcceptUpgrade();
        public void SelectUpgrade(int upgradeIndex);
        public void SkipUpgrade();
    }
}