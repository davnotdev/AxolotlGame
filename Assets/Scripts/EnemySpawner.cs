using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    public static float _minimumSpawnTime = 2;

    [SerializeField]
    public static float _maximumSpawnTime = 5;

    public static float _minPerSpawn = 3;

    public static float _maxPerSpawn = 5;

    private float _timeUntilSpawn;
    
    void Awake()
    {
        SetTimeUntilSpawn();
        _minimumSpawnTime = 2;
        _maximumSpawnTime = 5;

        _minPerSpawn = 3;
        _maxPerSpawn = 5;
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            for (int i = 0; i < Random.Range(_minPerSpawn, _maxPerSpawn); i++)
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
