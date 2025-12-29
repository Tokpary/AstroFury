
using UnityEngine;

namespace ScriptableObjects
{


    [CreateAssetMenu(fileName = "StatUpgrade", menuName = "ScriptableObjects/StatUpgradeSO", order = 0)]
    public class StatUpgradeSO : ScriptableObject
    {
        public StatType StatType;
        public string StatName;
        public Sprite StatIcon;
        public float StatValue;
    }

    public enum StatType
    {
        MaxHealthUp,
        HealthUp,
        Damage,
        Speed,
        ShotSpeed,
        Range,
        Drift,
        Reload,
        CritChance,
        CritDamage
    };
}