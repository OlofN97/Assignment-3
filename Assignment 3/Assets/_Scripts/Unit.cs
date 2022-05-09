using UnityEngine;

public class Unit : MonoBehaviour
{
    float timer;

    ObjectPool<Unit> thisPool;

    public void SetThisPool(ObjectPool<Unit> value) => thisPool = value;

    public void Initialise()
    {
        timer = 0;
    }

    void Update()
    {
        if (timer >= 1)
            Die();

        timer += Time.deltaTime;
    }

    void Die()
    {
        if (thisPool != null)
            thisPool.DisableObject(this);
        else
            Destroy(gameObject);
    }
}
