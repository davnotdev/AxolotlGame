using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float minSpawnTime;

    [SerializeField]
    private float maxSpawnTime;

    private float timeUntilNextSpawn;

    void Awake()
    {
        setTimeUntilNextSpawn();
    }

    void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn <= 0)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            setTimeUntilNextSpawn();
        }
    }

    private void setTimeUntilNextSpawn()
    {
        timeUntilNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
