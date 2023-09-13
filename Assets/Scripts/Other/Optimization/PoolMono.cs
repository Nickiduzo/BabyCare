using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Optimization
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly List<T> _freeElements = new();
        private readonly bool _autoExpand;
        private List<T> _pool;
        private readonly bool _reusable;

        public PoolMono(T prefab, int countInPool, bool autoExpand, bool reusable = true, Transform container = null)
        {
            _reusable = reusable;
            _prefab = prefab;
            _container = container;
            _autoExpand = autoExpand;
            CreatePool(countInPool);
        }

        private bool HasFreeElement(out T element)
        {
            foreach
                (var mono in _pool.Where(mono => !mono.gameObject.activeInHierarchy))
                _freeElements.Add(mono);

            if (_freeElements.Count == 0)
            {
                element = null;
                return false;
            }

            element = _freeElements[Random.Range(0, _freeElements.Count)];
            element.gameObject.SetActive(true);
            _freeElements.Clear();
            return true;
        }

        public bool HasFreeElement()
        {
            foreach
                (var mono in _pool.Where(mono => !mono.gameObject.activeInHierarchy))
                _freeElements.Add(mono);

            if (_freeElements.Count == 0)
                return false;

            _freeElements.Clear();
            return true;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
            {
                if (!_reusable)
                    _pool.Remove(element);

                return element;
            }

            if (_autoExpand)
                CreateObject(true);

            throw new System.Exception($"There is no elements in pool of type {typeof(T)}");
        }

        private void CreatePool(int countInPool)
        {
            _pool = new List<T>();

            for (int i = 0; i < countInPool; i++)
                CreateObject();
        }

        private void CreateObject(bool isActiveByDefault = false)
        {
            var createdObj = Object.Instantiate(_prefab, _container);
            createdObj.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObj);
        }
    }
}