using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    readonly T objectToPool;

    readonly Transform objectsParent;

    readonly LinkedList<T> pool;

    public T[] GetPoolObjects()
    {
        T[] toReturn = new T[pool.Count];
        pool.CopyTo(toReturn, 0);
        return toReturn;
    }

    public ObjectPool(T poolObject, int startingCapacity) : this(poolObject, startingCapacity, null) { }

    public ObjectPool(T poolObject, int startingCapacity, Transform parent)
    {
        pool = new LinkedList<T>();

        objectToPool = poolObject;
        startingCapacity = Mathf.Clamp(startingCapacity, 1, int.MaxValue);
        objectsParent = parent;

        for (int i = 0; i < startingCapacity; i++)
        {
            T temp = Object.Instantiate(objectToPool);
            temp.transform.SetParent(objectsParent);
            temp.gameObject.SetActive(false);
            pool.AddFirst(temp);
        }
    }

    public T Pull(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (pool.First.Value.gameObject.activeInHierarchy)
            IncreaseSize();

        T spawned = pool.First.Value;
        pool.RemoveFirst();
        pool.AddLast(spawned);
        spawned.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
        spawned.gameObject.SetActive(true);

        return spawned;
    }

    public void DisableObject(T toDisable)
    {
        toDisable.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        toDisable.gameObject.SetActive(false);
        pool.AddFirst(toDisable);
    }

    void IncreaseSize()
    {
        int iterations = pool.Count;
        for (int i = 0; i < iterations; i++)
        {
            T temp = Object.Instantiate(objectToPool);
            temp.transform.SetParent(objectsParent);
            temp.gameObject.SetActive(false);
            pool.AddFirst(temp);
        }
    }
}
