using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Unit unitToSpawn;

    [SerializeField] bool useObjectPool = true;

    [SerializeField] int poolStartCapacity = 25;

    [SerializeField] float timeBetweenSpawns = 0.5f;
    
    [SerializeField] Vector2 minMaxSpawnRadius = new Vector2(25, 45);

    public int spawnedUnits;

    float spawnTimer;

    ObjectPool<Unit> unitPool;

    void Start()
    {
        unitPool = new ObjectPool<Unit>(unitToSpawn, poolStartCapacity);
    }

    void Update()
    {
        while (spawnTimer >= timeBetweenSpawns && timeBetweenSpawns > 0)
        {
            Vector2 circlePosition = Random.onUnitSphere * Random.Range(minMaxSpawnRadius.x, minMaxSpawnRadius.y);
            Vector3 spawnPosition = new Vector3(circlePosition.x, transform.position.y, circlePosition.y);

            if (useObjectPool)
                unitPool.Pull(spawnPosition, Quaternion.identity);
            else
                Instantiate(unitToSpawn, spawnPosition, Quaternion.identity);

            spawnedUnits++;

            spawnTimer -= timeBetweenSpawns;
        }

        spawnTimer += Time.deltaTime;
    }
}
