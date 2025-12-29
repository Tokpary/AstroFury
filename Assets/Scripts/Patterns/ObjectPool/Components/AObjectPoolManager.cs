using System;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using Patterns.ObjectPool;
using Patterns.ObjectPool.Interfaces;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.ObjectPool.Components
{
    public abstract class AObjectPoolManager<T> : MonoBehaviour where T : MonoBehaviour, IPooleableObject
    {
     
        public T elementPrototype;
        public int initialNumberOfElements = 1;
        public bool allowAddNewElements = false;
        public bool spawning = false;
        
        protected ObjectPool _elementsPool;
        
        protected void Start()
        {
            Assert.IsTrue(elementPrototype is IPooleableObject);
            
            _elementsPool = new ObjectPool(elementPrototype, initialNumberOfElements, allowAddNewElements);
            
            if(GameManager.Instance)
                GameManager.Instance.OnGameRestarted.AddListener(ReleaseAllElements);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameRestarted.RemoveListener(ReleaseAllElements);
        }

        protected void ReleaseAllElements()
        {
            _elementsPool.ReleaseAll();
        }
        
        protected virtual T CreatePoolElement(T prototype, int initialNumberOfElements, bool allowAddNew) 
        {
            T element = (T) _elementsPool.Get();
            if (element)
            {
                element.elementPool = _elementsPool;
            }
            
            return element;
        }
    }
}