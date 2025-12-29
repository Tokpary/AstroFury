using System.Collections;
using UnityEngine;

namespace Patterns.Observer.Components.Components.Misc
{
    public class DamageFlash :MonoBehaviour
    {
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashDuration = 0.1f;
        
        private SpriteRenderer _spriteRenderer;
        private Material _material;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
        }
        
        public void StartFlash()
        {
            StartCoroutine(DamageEffect());
        }
        
        IEnumerator DamageEffect()
        {
            SetFlashColor();
            float currentFlashAmount = 0;
            float elapsedTime = 0;

            while (elapsedTime < _flashDuration)
            {
                elapsedTime += Time.deltaTime;
                 
                currentFlashAmount = Mathf.Lerp(1, 0, elapsedTime / _flashDuration);
                SetFlashAmount(currentFlashAmount);
                yield return null;
            }
        }
        
        private void SetFlashColor()
        {
            _material.SetColor("_FlashColor", _flashColor);
        }

        private void SetFlashAmount(float amount)
        {
            _material.SetFloat("_FlashAmount", amount);
        }
    }
}