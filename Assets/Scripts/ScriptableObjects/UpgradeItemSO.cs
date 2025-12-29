
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "ScriptableObjects/UpgradeItemSOs", order = 0)]
    public class UpgradeItemSO : ScriptableObject
    {
        public string ItemName;
        public string ItemDesc;
        public Sprite ItemSprite;
        public int Price;

        public Sprite playerEquipedSprite;
        public Sprite ammoSprite;

        public StatUpgradeSO[] StatUpgrades;
    }
}