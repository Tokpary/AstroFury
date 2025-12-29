using System.Collections.Generic;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;

namespace Patterns.ObjectPool
{
    public class ObjectPool : IObjectPool
    {
        private IPooleableObject _objectPrototype;
        private readonly bool _allowAddNew;
        
        private List<IPooleableObject> _objects;
        
        private int _activeObjects;
        
        public ObjectPool(IPooleableObject objectPrototype, int initialNumberOfElements, bool allowAddNew)
        {
            _objectPrototype = objectPrototype;     // El tipo de objeto que se va a poolear
            _allowAddNew = allowAddNew;             // Si se permite agregar nuevos objetos al pool
            _objects = new List<IPooleableObject>(initialNumberOfElements); // Lista de objetos pooleados
            _activeObjects = 0;                    // Número de objetos activos
            
            for (int i = 0; i < initialNumberOfElements; i++)
            {
                _objects.Add(CreateObject());
            }
        }

        private IPooleableObject CreateObject()
        {
            IPooleableObject newObj = _objectPrototype.Clone() as IPooleableObject;
            return newObj;
        }

        public IPooleableObject Get()
        {
            for (int i = 0; i< _objects.Count; i++)
            {
                if (!_objects[i].Active)
                {
                    _objects[i].Active = true;
                    _activeObjects += 1;
                    return _objects[i];
                }
            }

            if (_allowAddNew)
            {
                IPooleableObject newObj = CreateObject();
                newObj.Active = true;
                _objects.Add(newObj);
                _activeObjects += 1;
                return newObj;
            }

            return null;
        }

        public void Release(IPooleableObject obj)
        {
            obj.Active = false;
            _activeObjects -= 1;
            obj.Reset();
        }
        
        public void ReleaseAll()
        {
            if(_objects == null)
                return;
            if (_objects.Count > 0)
            {
                foreach (var obj in _objects)
                {
                    if (obj.Active)
                    {
                        obj.Active = false;
                        _activeObjects -= 1;
                        obj.Reset();
                    }
                }
            }
            
        }
        
        public int GetCount()
        {
            return _objects.Count;
        }
        
        public int GetActive()
        {
            return _activeObjects;
        }
    }
}