using UnityEngine;

public class SubstanceSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    public static float minSpawnTime;

    [SerializeField]
    public static float maxSpawnTime;

    private float timeUntilNextSpawn;

    void Awake()
    {
        setTimeUntilNextSpawn();
        minSpawnTime = 5;
        maxSpawnTime = 10;
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
