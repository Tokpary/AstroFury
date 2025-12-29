
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/EntitySO", order = 0)]
    public class EntitySO : ScriptableObject
    {
        public string Name;
        public int Health;
        public int Damage;
        public float Speed;
        public Sprite Sprite;
        public RuntimeAnimatorController Animator;
    }
}