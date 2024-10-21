using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    public void SpawnBullet(Vector3 hitPoint)
    {
        Bullet spawned = Instantiate(_bulletPrefab, transform);
        spawned.Move(hitPoint);
    }
}
