using UnityEngine;

[RequireComponent(typeof(HeroMover))]
public class HeroShooter : MonoBehaviour
{
    [SerializeField] private BulletCreator _bulletCreator;

    private HeroMover _heroMover;
    private Camera _mainCamera;
    
    [HideInInspector] public bool canShoot;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _heroMover = GetComponent<HeroMover>();
    }

    private void OnEnable()
    {
        _heroMover.WaypointReached += OnWaypointReached;
    }

    private void OnDisable()
    {
        _heroMover.WaypointReached -= OnWaypointReached;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            SpawnRaycast();
        }
    }

    private void SpawnRaycast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        const float rayLength = 100f;

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            _bulletCreator.SpawnBullet(hit.point);
        }
    }

    private void OnWaypointReached()
    {
        canShoot = true;
    }
}
