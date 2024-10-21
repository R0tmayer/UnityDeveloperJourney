using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundChecker : MonoBehaviour
{
    public event UnityAction GroundEntered;

    private const int _groundIndex = 8;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _groundIndex)
            GroundEntered?.Invoke();
    }
}
