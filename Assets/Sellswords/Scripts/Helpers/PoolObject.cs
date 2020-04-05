using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Sellswords
{
    public class PoolObject<T> : IPoolObject<T> where T : IBaseModel
    {
        #region Fields

        public int Count => _bag.Count;

        #endregion

        #region PrivateData

        private readonly ConcurrentBag<T> _bag;
        private readonly Func<T> _func;
        private readonly Vector3 _position;

        #endregion


        #region ClassLifeCycles

        public PoolObject(Vector3 position)
        {
            _bag = new ConcurrentBag<T>();
            _position = position;
        }

        public PoolObject(Vector3 position, Func<T> func)
        {
            if (func == null)
            {
                return;
            }

            _bag = new ConcurrentBag<T>();
            _position = position;
            _func = func;
        }

        #endregion


        #region Methods

        public T GetObject()
        {
            return _bag.TryTake(out var item) ? item : _func();
        }

        public T GetObject(Vector3 position)
        {
            var result = default(T);

            if (_bag.TryTake(out var item))
            {
                item.Transform.position = position;
                result = item;
            }
            else
            {
                result = _func();
            }

            return result;
        }

        public T GetObject(int id, Func<T> func)
        {
            var result = default(T);
            var itemsMemory = new List<T>();

            if (_bag.Any(q => q.Id == id))
            {
                while (!_bag.IsEmpty)
                {
                    if (_bag.TryTake(out var item))
                    {
                        if (item.Id == id)
                        {
                            result = item;
                            break;
                        }

                        itemsMemory.Add(item);
                    }
                }
            }
            else
            {
                result = func();
            }
            PutObjects(itemsMemory);
            return result;
        }

        public T GetObject(int id, Vector3 position, Func<T> func)
        {
            var result = default(T);
            var itemsMemory = new List<T>();

            if (_bag.Any(q => q.Id == id))
            {
                while (!_bag.IsEmpty)
                {
                    if (_bag.TryTake(out var item))
                    {
                        if (item.Id == id)
                        {
                            item.Transform.position = position;
                            result = item;
                            break;
                        }

                        itemsMemory.Add(item);
                    }
                }
            }
            else
            {
                result = func();
            }
            PutObjects(itemsMemory);
            return result;
        }

        public void PutObject(T item)
        {
            _bag.Add(item);
        }

        public void PutObjects(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                PutObject(item);
            }
        }

        #endregion


        #region IPoolObject

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}