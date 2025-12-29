using Scripts.Patterns.State.Components;

namespace Patterns.State.Interfaces
{
    public interface ISelectableUpgradeState
    {
        void SkipUpgrade();
        void SelectUpgrade(int upgradeIndex); 
        void AcceptUpgrade(GameManager gameManager); 
        void DisplayUpgrades(GameManager gameManager); 
    }
}