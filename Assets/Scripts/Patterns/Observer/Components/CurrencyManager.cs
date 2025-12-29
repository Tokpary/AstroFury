using System;
using System.Collections.Generic;
using Components;
using DefaultNamespace.Patterns;
using Patterns.Observer.Interfaces;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using UnityEngine;

namespace Patterns.Observer.Components
{
    public class CurrencyManager : MonoBehaviour, ISubject<float>
    {
        public float Currency { get; set; }

        private List<Interfaces.IObserver<float>> _observers = new List<Interfaces.IObserver<float>>();
        public void AddObserver(Interfaces.IObserver<float> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(Interfaces.IObserver<float> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (Interfaces.IObserver<float> observer in _observers)
            {
                observer?.UpdateObserver(Currency);
            }
        }
        
        private void Start()
        {
            Currency = 0;
            GameManager.Instance.OnGameRestarted.AddListener(ResetCurrency);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameRestarted.RemoveListener(ResetCurrency);
        }

        private void ResetCurrency()
        {
            
            Currency = 0;
            NotifyObservers();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            IPickable pickable = other.GetComponent<IPickable>();
            if (pickable != null)
            {
                SoundManager.Instance.PlaySfx("PickupSFX");
                Currency += pickable.Pick();
                NotifyObservers();
            }
        }

        
        
    }
}