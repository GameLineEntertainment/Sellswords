using System.Collections;
using System.Collections.Generic;


namespace Sellswords
{
    public class CircularLinkedList<T> : IEnumerable<T>
    {
        #region PrivateData

        private Node<T> _first;
        private Node<T> _last;
        private int _count;

        #endregion


        #region Properties

        public bool IsEmpty => _count == 0;
        public int Count => _count;
        public bool IsReadOnly => false;

        #endregion


        #region ClassLifeCycles

        public CircularLinkedList()
        {
            
        }

        public CircularLinkedList(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        #endregion
        
        
        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            var current = _first;

            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            } while (current != _first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        #endregion


        #region Methods

        public void Add(T data)
        {
            var node = new Node<T>(data);

            if (_first == null)
            {
                _first = node;
                _last = node;
                _last.Next = _first;
            }
            else
            {
                node.Next = _first;
                _last.Next = node;
                _last = node;
            }

            _count++;
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public bool Remove(T data)
        {
            var result = false;
            var current = _first;
            Node<T> previous = null;

            if (IsEmpty)
            {
                return false;
            }

            do
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current == _last)
                        {
                            _last = previous;
                        }
                    }
                    else
                    {
                        if (_count == 1)
                        {
                            _first = _last = null;
                        }
                        else
                        {
                            _first = current.Next;
                            _last.Next = current.Next;
                        }
                    }

                    _count--;
                    result = true;
                    break;
                }

                previous = current;
                current = current.Next;
            } while (current != _first);

            return result;
        }

        public void Clear()
        {
            _first = null;
            _last = null;
            _count = 0;
        }

        public bool Contains(T data)
        {
            var result = false;
            var current = _first;
            
            if (current == null)
            {
                return result;
            }

            do
            {
                
                if (current.Data.Equals(data))
                {
                    result = true;
                    break;
                }

                current = current.Next;
                
            } while (current != _first);

            return result;
        }

        public T GetNext(T data)
        {
            var result = default(T);
            var current = _first;

            do
            {
                if (current.Data.Equals(data))
                {
                    result = current.Next.Data;
                    break;
                }

                current = current.Next;
                
            } while (current != _first);

            return result;
        }

        #endregion
    }
}