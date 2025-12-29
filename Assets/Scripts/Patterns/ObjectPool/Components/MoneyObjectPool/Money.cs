using System;
using Components;
using DG.Tweening;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;

namespace Patterns.ObjectPool.Components.MoneyObjectPool
{
    public class Money : MonoBehaviour, IPooleableObject, IPickable
    {
        protected float _value = 2f;
        private bool isPicked;

        public IPooleableObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            Money moneyPickable = clone.GetComponent<Money>();
            return moneyPickable;
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
        public IObjectPool elementPool { get; set; }
        public void Reset()
        {
            transform.localScale = Vector3.one * 0.25f;
        }


        public float Pick()
        {
            float value = _value;
            transform.DOScale(Vector3.one * 0.35f, 0.1f).SetEase(Ease.OutQuart).OnComplete(() =>
                transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack).OnComplete(() => elementPool?.Release(this)));
            
            return value;
        }
    }
}