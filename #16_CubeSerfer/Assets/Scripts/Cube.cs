using UnityEngine;

public class Cube : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private CubesHolder _cubesHolder;

    public bool IsCollected { get; private set; }

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        _cubesHolder = FindObjectOfType<CubesHolder>();
    }

    public void StopMovement()
    {
        _cubesHolder.RemoveCube(this);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ActivateTrail()
    {
        _trailRenderer.enabled = true;
    }

    public void Collect()
    {
        _cubesHolder.AddNewCube(this);
        IsCollected = true;
    }
}
