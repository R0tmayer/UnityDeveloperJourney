using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootForce;


    public void Shoot(Vector3 direction)
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

        bullet.Move(direction, _shootForce);
    }
}
