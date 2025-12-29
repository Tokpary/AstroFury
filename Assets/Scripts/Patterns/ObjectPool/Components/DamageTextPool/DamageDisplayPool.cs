using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class DamageDisplayPool : AObjectPoolManager<DamageText>
    {
        
        private void OnEnable()
        {
            EventManager.Instance.OnDamageDealt += DisplayDamageText;
        }
        
        private void OnDisable()
        {
            EventManager.Instance.OnDamageDealt -= DisplayDamageText;
        }
        
        private void DisplayDamageText(float damage, Vector2 position)
        {
            Debug.Log("damage dealt");
            DamageText damageText = CreatePoolElement(elementPrototype, initialNumberOfElements, allowAddNewElements);
            damageText.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
            damageText.transform.position = screenPosition;
            damageText.DisplayDamage(damage, position);
        }
    }
}