using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        var _closestCoin = _coinManager.GetClosestCoin(transform.position);

        if(_closestCoin == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        Vector3 directionToTarget = (_closestCoin.transform.position - transform.position);
        Vector3 currentDirection = Vector3.Lerp(transform.forward, directionToTarget, Time.deltaTime * _rotationSpeed);
        Vector3 toTargetXZ = new Vector3(currentDirection.x, 0, currentDirection.z);
        transform.rotation = Quaternion.LookRotation(toTargetXZ);
    }
}
