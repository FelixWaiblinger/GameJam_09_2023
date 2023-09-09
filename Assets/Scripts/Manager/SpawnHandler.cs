using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [Header("Enemy spawning")]
    [SerializeField] private VoidEventChannel _enemyDeathEvent;
    [SerializeField] private VoidEventChannel _areaClearEvent;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _spawnerCount;
    [SerializeField] private int _enemiesPerSpawner;
    [SerializeField] private float _spawnRadius;
    private Transform[] _spawners;
    private int _deadEnemies = 0;

    #region SETUP

    void OnEnable()
    {
        _enemyDeathEvent.OnVoidEventRaised += RegisterEnemyDeath;
    }

    void OnDisable()
    {
        _enemyDeathEvent.OnVoidEventRaised -= RegisterEnemyDeath;
    }

    void Start()
    {
        if (transform.childCount != _spawnerCount)
        {
            Debug.Log("Not enough spawnpoints defined!");
            return;
        }

        _spawners = new Transform[_spawnerCount];
        for (int i = 0; i < _spawnerCount; i++)
        {
            _spawners[i] = transform.GetChild(i);
            for (int j = 0; j < _enemiesPerSpawner; j++)
            {
                var rand = _spawnRadius * Random.insideUnitCircle;
                var offset = new Vector3(rand.x, 0, rand.y);

                Instantiate(_enemyPrefab, _spawners[i].position + offset, _spawners[i].rotation);
            }
        }
    }

    #endregion

    void RegisterEnemyDeath()
    {
        _deadEnemies++;

        if (_deadEnemies == _enemiesPerSpawner) _areaClearEvent.RaiseVoidEvent();
    }
}
