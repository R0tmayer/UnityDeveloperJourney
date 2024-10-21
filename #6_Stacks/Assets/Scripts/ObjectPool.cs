using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _finallPlatform;

    [SerializeField] private GameObject _cutPanelPrefab;

    private List<GameObject> _pool = new List<GameObject>();

    [SerializeField] private GameObject _containerForBlocks;
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private int _amountOfBlocks;

    [SerializeField] private GameObject _containerForSpawnPoints;
    [SerializeField] private GameObject _spawnPointPrefab;
    private readonly List<Transform> _spawnPointsTransforms = new List<Transform>();

    private int _keyStrokesAmount = 0;

    public static float Timer;


    private void Start()
    {
        InitializeSpawnPoints();
        InitializedPool();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        Debug.Log(Timer);

        if (Input.GetKeyDown(KeyCode.Space) && _keyStrokesAmount < _amountOfBlocks && _keyStrokesAmount > 2 && Timer >= 2)
        {
            ActivateObject(_pool[_keyStrokesAmount], _spawnPointsTransforms[_keyStrokesAmount].position);
            _keyStrokesAmount++;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
            //CreatePlane();
    }

    private void InitializeSpawnPoints()
    {
        for (int i = 0; i < _amountOfBlocks; i++)
        {
            GameObject spawnPoint = Instantiate(_spawnPointPrefab, _containerForSpawnPoints.transform);
            _spawnPointsTransforms.Add(spawnPoint.transform);
            _spawnPointsTransforms[i].position = new Vector3(5, _spawnPointPrefab.transform.position.y, i + 1);
        }
    }

    private void InitializedPool()
    {
        _finallPlatform.transform.position = new Vector3(_finallPlatform.transform.position.x, _finallPlatform.transform.position.y, _amountOfBlocks + 2.5f);

        for (int i = 0; i < _amountOfBlocks; i++)
        {
            GameObject spawned = Instantiate(_blockPrefab, _containerForBlocks.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }
    private void ActivateObject(GameObject block, Vector3 spawnPointPosition)
    {
        block.SetActive(true);
        block.transform.position = spawnPointPosition;
    }

    

    
}
