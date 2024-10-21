using UnityEngine;

public class CubesCollector : MonoBehaviour
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
            if (cube.IsCollected == false)
            {
                cube.Collect();
                _cubesHolder.MoveHeroUp();
                cube.transform.position = _cubesHolder.GetPositionForNewCube();
            }
        }
    }
}
