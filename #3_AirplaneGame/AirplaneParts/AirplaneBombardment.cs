using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneBombardment : MonoBehaviour
{
    [SerializeField] private AirplaneBomb _airplaneBombPrefab;
    [SerializeField] private float _bombPeriod;
    private Transform _airplane;
    private float _timer;

    private void Start()
    {
        _airplane = GetComponentInParent<Airplane>().transform;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && _timer > _bombPeriod)
        {
            DropBomb();
            _timer = 0;
        }
    }

    private void DropBomb()
    {
        float upwardAngle = Mathf.Clamp(_airplane.eulerAngles.z, 0, 25);
        float downwardAngle = Mathf.Clamp(_airplane.eulerAngles.z, 335, 360);

        if (_airplane.eulerAngles.z == upwardAngle || _airplane.eulerAngles.z == downwardAngle)
        {
            AirplaneBomb newBomb = Instantiate(_airplaneBombPrefab, transform.position, Quaternion.identity);
            newBomb.GetComponent<Rigidbody>().velocity = new Vector3(0, -15, 0);

        }
    }
}
