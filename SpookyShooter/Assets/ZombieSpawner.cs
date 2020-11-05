using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<GameObject> spawnPositions;

    private List<Zombie> zombies = new List<Zombie>();

    public bool canSpawn;

    public float spawnWaitTimeMin = 5f;
    public float spawnWaitTimeMax = 10f;
    public int maxNumZombies = 20;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    public void StartSpawningZombies()
    {
        StartCoroutine(SpawnZombies());
    }

    public void StopSpawningZombies()
    {
        StopCoroutine(SpawnZombies());
    }

    public IEnumerator SpawnZombies()
    {
        while(canSpawn)
        {
            float timeToWait = Random.Range(spawnWaitTimeMin, spawnWaitTimeMax);
            yield return new WaitForSeconds(timeToWait);
            if (zombies.Count <= maxNumZombies)
            {
                SpawnZombie();
            }
        }
    }

    public void FreezeAllZombies()
    {
        foreach(Zombie z in zombies)
        {
            z.freeze = true;
        }
    }
    public void UnFreezeAllZombies()
    {
        foreach (Zombie z in zombies)
        {
            z.freeze = false;
        }
    }

    private void RemoveZombie(Zombie z)
    {
        z.onDie -= RemoveZombie;
        zombies.Remove(z);
    }

    public void SpawnZombie()
    {
        GameObject spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count - 1)];
        Zombie z = Instantiate(zombiePrefab, spawnPos.transform.position, Quaternion.identity, transform).GetComponent<Zombie>();
        z.onDie += RemoveZombie;
        zombies.Add(z);
        z.name = "Zombie " + zombies.IndexOf(z);
    }
}
