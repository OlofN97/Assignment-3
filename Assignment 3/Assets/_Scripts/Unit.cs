using UnityEngine;

public class Unit : MonoBehaviour
{
    float timer;
    float speed;
    Vector3 target;
    public ObjectPool<Unit> thisPool;

    public void SetThisPool(ObjectPool<Unit> value) => thisPool = value;

    public void Initialise()
    {
        timer = 0;
        speed = 0.4f;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        Move();

        //if (timer >= 1)
        //    Die();

        //timer += Time.deltaTime;
    }

    public void Die()
    {
        if (thisPool != null)
            thisPool.DisableObject(this);
        else
            Destroy(gameObject);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }
}
