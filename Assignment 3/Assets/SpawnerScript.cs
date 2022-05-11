using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyType;
    public List<GameObject> enemies;
    public GameObject player;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 2);
    }

    void SpawnEnemy()
    {
        Vector3 pos = Random.insideUnitSphere*25;

        pos.y = 1;

        if(Vector3.Distance(player.transform.position, pos)>distance )
        {
            GameObject newEnemy = Instantiate(enemyType, pos, Quaternion.identity);
            enemies.Add(newEnemy);
            Invoke("SpawnEnemy", 0.0f);
        }
        else
        {
            Invoke("SpawnEnemy", 0);
        }



    }
    
    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<enemies.Count;i++)
        {

        }
        
    }
}
