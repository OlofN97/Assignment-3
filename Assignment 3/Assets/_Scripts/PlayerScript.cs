using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool useObjectPool;
    public bool UseObjectPool
    {        
        set { useObjectPool = value; }
    }

    [Header("Bullet Data")]
    [SerializeField] private Bullet bullet;
    [SerializeField] private float rateOfFire;
    [SerializeField] private int poolStartCapacity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform bulletParent;

    private Camera cam;
    private float timeElapsedSinceLastShoot;
    private ObjectPool<Bullet> bulletPool;


    void Start()
    {
        cam = Camera.main;
        if (useObjectPool)
            bulletPool = new ObjectPool<Bullet>(bullet, poolStartCapacity, bulletParent);
    }

    void Update()
    {
        UpdatePlayerRotation();
        UpdateShooting();
    }
    private void UpdatePlayerRotation()
    {
        Vector3 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
    private void UpdateShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            timeElapsedSinceLastShoot += Time.deltaTime;
            if (timeElapsedSinceLastShoot > rateOfFire)
            {
                Bullet spawnedBullet;
                if (useObjectPool)
                {
                    spawnedBullet = bulletPool.Pull(transform.position, transform.rotation);
                    spawnedBullet.SetThisPool(bulletPool);
                }
                else
                {
                    Instantiate(bullet, transform.position, transform.rotation, bulletParent);
                }


                timeElapsedSinceLastShoot -= rateOfFire;
            }

        }
    }


}
