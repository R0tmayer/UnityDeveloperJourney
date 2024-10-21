using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _endPlatform;

    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private int _amountOfBlocks;
    [SerializeField] private int _xPositionToStopBlock;
    [SerializeField] private Vector3 _blockSpawnPointPosition;
    private List<GameObject> _instantiatedBlocks = new List<GameObject>();

    private bool _canInstantiateBlock = false;

    [SerializeField] private GameObject _cutPlanePrefab;
    private int _amountOfPlanePairCreation = 0;

    private void Awake()
    {
        _endPlatform.transform.position = new Vector3(_endPlatform.transform.position.x,
                                                      _endPlatform.transform.position.y,
                                                      _amountOfBlocks + 2.5f);

        GameObject spawned = Instantiate(_blockPrefab, _blockSpawnPointPosition, Quaternion.identity);
        _instantiatedBlocks.Add(spawned);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _instantiatedBlocks[_instantiatedBlocks.Count - 1].transform.position.x <= _xPositionToStopBlock)
        {
            //Бесконечное создание режущих плоскостей при достижении endPlatform
            if (_instantiatedBlocks.Count > 1 && _amountOfPlanePairCreation < (_amountOfBlocks - 1))
                CreateCutPlane();

            if (_instantiatedBlocks.Count < _amountOfBlocks)
            {
                _instantiatedBlocks[_instantiatedBlocks.Count - 1].GetComponent<Block>().SetMoveSpeed(0);
                SpawnBlock();
                _canInstantiateBlock = false;
            }
        }
    }

    private void CreateCutPlane()
    {
        //2 потому что первый блок создаётся в Awake
        Transform lastInstantiatedBlockTransform = _instantiatedBlocks[_instantiatedBlocks.Count - 2].transform;

        GameObject leftCutPlane = Instantiate(_cutPlanePrefab, transform.position, Quaternion.identity);
        leftCutPlane.name = "LeftPlane";
        leftCutPlane.transform.position = lastInstantiatedBlockTransform.position + new Vector3((-1) * lastInstantiatedBlockTransform.localScale.x / 2,
                                                                                                       lastInstantiatedBlockTransform.localScale.y + 0.4f,
                                                                                                       lastInstantiatedBlockTransform.localScale.z);

        GameObject rightCutPlane = Instantiate(_cutPlanePrefab, transform.position, Quaternion.identity);
        rightCutPlane.transform.position = lastInstantiatedBlockTransform.position + new Vector3(lastInstantiatedBlockTransform.localScale.x / 2,
                                                                                                 lastInstantiatedBlockTransform.localScale.y + 0.5f,
                                                                                                 lastInstantiatedBlockTransform.localScale.z);

        _amountOfPlanePairCreation++;
    }

    public void ReplaceLastInstantiatedBlock(GameObject cutSideOfBlock) => _instantiatedBlocks[_instantiatedBlocks.Count - 2] = cutSideOfBlock;

    //private void OnCubeCutted() => SpawnBlock();

    private void SpawnBlock()
    {
        GameObject spawned = Instantiate(_instantiatedBlocks[_instantiatedBlocks.Count - 1], transform.position, Quaternion.identity);
        _blockSpawnPointPosition = new Vector3(_blockSpawnPointPosition.x, _blockSpawnPointPosition.y, _blockSpawnPointPosition.z + 1);
        spawned.transform.position = _blockSpawnPointPosition;
        spawned.GetComponent<Block>().SetMoveSpeed(3);
        _instantiatedBlocks.Add(spawned);
    }

    public void CanInstantiateBlock()
    {
        _canInstantiateBlock = true ;
    }
}
