using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Unit unitToSpawn;

    private bool useObjectPool;

    [SerializeField] int poolStartCapacity = 25;

    [SerializeField] float timeBetweenSpawns = 0.5f;
    
    [SerializeField] Vector2 minMaxSpawnRadius = new Vector2(25, 45);

    [SerializeField] GameObject Player;
    [SerializeField] float distance;
    [SerializeField] Transform unitParent;

    float spawnTimer;

    ObjectPool<Unit> unitPool;

    public bool UseObjectPool
    {       
        set { useObjectPool = value; }
    }

    void Start()
    {
        if (useObjectPool)
            unitPool = new ObjectPool<Unit>(unitToSpawn, poolStartCapacity, unitParent);
    }

    void Update()
    {
        while (spawnTimer >= timeBetweenSpawns && timeBetweenSpawns > 0)
        {
            Vector3 spawnPosition = Player.transform.position;

            while (Vector3.Distance(spawnPosition, Player.transform.position) < distance)
            {

                Vector2 circlePosition = Random.onUnitSphere * Random.Range(minMaxSpawnRadius.x, minMaxSpawnRadius.y);
                spawnPosition = new Vector3(circlePosition.x, transform.position.y, circlePosition.y);

                if (Vector3.Distance(spawnPosition, Player.transform.position) < distance)
                    continue;

                Unit spawnedUnit;
                if (useObjectPool)
                {
                    spawnedUnit = unitPool.Pull(spawnPosition, Quaternion.identity);
                    spawnedUnit.SetThisPool(unitPool);
                }
                else
                {
                    spawnedUnit = Instantiate(unitToSpawn, spawnPosition, Quaternion.identity, unitParent);
                }

                spawnedUnit.Initialise();

                spawnTimer -= timeBetweenSpawns;

            }
        }

        spawnTimer += Time.deltaTime;

    }
}
