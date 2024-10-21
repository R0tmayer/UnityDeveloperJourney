using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneBomb : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out Ground ground) || TryGetComponent(out Unit unit))
        {
            Destroy(gameObject);
        }
    }
}
