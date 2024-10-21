using UnityEngine;

public class EnemyPartsContainer : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _parts;

    private Vector3[] _partsPosition;
    private Vector3[] _partsRotation;


    private void OnEnable() => Restart();

    private void Awake()
    {
        _partsPosition = new Vector3[_parts.Length];
        _partsRotation = new Vector3[_parts.Length];
        for (int i = 0; i < _parts.Length; i++)
        {
            _partsPosition[i] = _parts[i].transform.localPosition;
            _partsRotation[i] = _parts[i].transform.localEulerAngles;
        }
    }

    private void Restart()
    {
        ResetContainer();
        ResetParts();
    }

    private void ResetParts()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].velocity = Vector3.zero;
            _parts[i].transform.localPosition = _partsPosition[i];
            _parts[i].transform.localEulerAngles = _partsRotation[i];
        }
    }

    private void ResetContainer()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}
