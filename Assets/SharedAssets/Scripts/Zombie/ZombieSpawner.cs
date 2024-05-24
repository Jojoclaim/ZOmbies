using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject ZombiePrefab;
    [SerializeField] Transform[] SpawnPoints;

    int toSpawnAmount = 10;

    private void Awake()
    {
        StartCoroutine(Waves());
    }

    public IEnumerator Waves()
    {
        while (true)
        {
            StartCoroutine(Spawn());
            while (GetEnemiesAmount() >= 0) {
                yield return new WaitForSeconds(1); 
            }
            StopCoroutine(Spawn());

            yield return new WaitForSeconds(25);
            toSpawnAmount += 3;
        }
    }

    public IEnumerator Spawn()
    {
        for (int i = 0; i < toSpawnAmount; i++)
        {
            Instantiate(ZombiePrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(2f + Random.Range(0f,2f));
        }

        yield return new WaitForSeconds(60 + (10 * toSpawnAmount));
        if (GetEnemiesAmount() >= 0)
        {
            ZombieAi[] zombies = FindObjectsOfType<ZombieAi>();
            for (int i = 0;i < zombies.Length;i++)
            {
                Destroy(zombies[i].gameObject);
            }
        }
}

    int GetEnemiesAmount()
    {
        ZombieAi[] zombies = FindObjectsOfType<ZombieAi>();
        return zombies.Length;
    }
}
