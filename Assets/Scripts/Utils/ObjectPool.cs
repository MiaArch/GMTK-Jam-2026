namespace Utils
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int initialSize = 20;

        readonly Queue<GameObject> pool = new();

        void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
            {
                var obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            var item = pool.Dequeue();
            item.SetActive(true);
            return item;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}