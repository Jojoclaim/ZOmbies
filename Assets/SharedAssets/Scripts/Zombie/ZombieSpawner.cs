using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject ZombiePrefab;
    [SerializeField] Transform[] SpawnPoints;

    int toSpawnAmount = 20;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }


    public IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i < toSpawnAmount; i++)
            {
                Instantiate(ZombiePrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position + new Vector3(Random.Range(-2,2), 0, Random.Range(-2, 2)), Quaternion.identity);
                yield return new WaitForSeconds(1f + Random.Range(0f, 2f));
            }
            toSpawnAmount += 3;
            yield return new WaitForSeconds(40);
        }
    }
}
