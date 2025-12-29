using System;
using DG.Tweening;
using Patterns.ObjectPool.Interfaces;
using TMPro;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class DamageText : MonoBehaviour, IPooleableObject
    {
        [SerializeField] private TMP_Text _damageText;
        private Camera mainCamera;
        private Vector3 worldPosition;

        private void Start()
        {
            _damageText = GetComponent<TMP_Text>();
            mainCamera = Camera.main;
        }

        public IPooleableObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            DamageText damageText = clone.GetComponent<DamageText>();
            return damageText;
        }

        public bool Active
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }

        private void Update()
        {
            if (mainCamera != null)
            {
                transform.position = mainCamera.WorldToScreenPoint(worldPosition);
            }
        }

        public void DisplayDamage(float damage, Vector3 position)
        {
            _damageText.text = damage.ToString("F1");
            worldPosition = position;
            
            transform.DOScale(Vector3.one * 1.5f, 0.5f).OnComplete(() => elementPool.Release(this));
        }

        public IObjectPool elementPool { get; set; }
        public void Reset()
        {
            transform.localScale = Vector3.one;
        }
    }
}