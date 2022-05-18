
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ObjectPool<Bullet> thisPool;
    public void SetThisPool(ObjectPool<Bullet> value) => thisPool = value;

    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletTimer = 3;

    private float timer;
    private bool collieded;

    private void Start()
    {
        timer = 0;
    }

    void Update()
    {
        transform.position = transform.position + transform.forward * bulletVelocity * Time.deltaTime;



        if (timer >= bulletTimer)
            //Die();

            timer += Time.deltaTime;
    }

    void Die()
    {
        if (thisPool != null)
            thisPool.DisableObject(this);
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        timer = 0;
        collieded = false;
    }

    void OnTriggerStay(Collider collision)
    {
        if (gameObject.activeInHierarchy && collision.gameObject.activeInHierarchy)
        {
            if (!collieded)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<Unit>().Die();
                    Die();
                    collieded = true;
                }
            }
        }
    }
}
