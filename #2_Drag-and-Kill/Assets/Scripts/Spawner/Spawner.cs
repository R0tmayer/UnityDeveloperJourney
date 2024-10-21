using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private float _timer;


    private void Start() => InitializePool(_unitPrefab);

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject unit))
            {
                _timer = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                ActivateObject(unit, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void ActivateObject(GameObject unit, Vector3 spawnPointPosition)
    {
        unit.SetActive(true);

        unit.transform.position = spawnPointPosition;
    }
}
