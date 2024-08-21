using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts._helpers
{
    /// <summary>
    /// A generic object pool for MonoBehaviour types, which manages the reuse of objects to avoid unnecessary instantiation and destruction.
    /// </summary>
    /// <typeparam name="T">The type of MonoBehaviour object to pool.</typeparam>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Func<T> _objectFactory;
        private readonly Queue<T> _objectPool;
        private readonly int _initialSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="objectFactory">A function that creates a new instance of the object when needed.</param>
        /// <param name="initialSize">The initial number of objects to be created in the pool.</param>
        public ObjectPool(Func<T> objectFactory, int initialSize)
        {
            _objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory));
            _initialSize = initialSize > 0 ? initialSize : throw new ArgumentOutOfRangeException(nameof(initialSize), "Initial size must be greater than zero.");
            _objectPool = new Queue<T>(initialSize);

            InitializePool();
        }

        /// <summary>
        /// Initializes the object pool by creating the initial set of objects.
        /// </summary>
        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                T obj = CreateObject();
                _objectPool.Enqueue(obj);
            }
        }

        /// <summary>
        /// Retrieves an object from the pool. If the pool is empty, a new object is created.
        /// </summary>
        /// <returns>An object of type <typeparamref name="T"/> from the pool.</returns>
        public T GetObject()
        {
            if (_objectPool.Count == 0)
            {
                return CreateObject();
            }

            T obj = _objectPool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Returns an object to the pool, deactivating it in the process.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        public void ReturnObject(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            obj.gameObject.SetActive(false);
            _objectPool.Enqueue(obj);
        }

        /// <summary>
        /// Creates a new object using the factory method and deactivates it.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        private T CreateObject()
        {
            T obj = _objectFactory();
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}