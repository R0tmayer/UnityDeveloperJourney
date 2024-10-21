using UnityEngine;

public class Lava : MonoBehaviour
{
    private CubesHolder _cubesHolder;

    private void Start()
    {
        _cubesHolder = FindObjectOfType<CubesHolder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            _cubesHolder.RemoveCube(cube);
            cube.gameObject.SetActive(false);
        }
    }
}
