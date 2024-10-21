using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Collision : MonoBehaviour
{
    private BlockSpawner _blockSpawner;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _blockSpawner = FindObjectOfType<BlockSpawner>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (gameObject.name == "LeftPlane")
            _blockSpawner.ReplaceLastInstantiatedBlock(CuttingBlock.CutLeft(collision.transform, transform.position));
        else
            _blockSpawner.ReplaceLastInstantiatedBlock(CuttingBlock.CutRight(collision.transform, transform.position));
        _blockSpawner.CanInstantiateBlock();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
