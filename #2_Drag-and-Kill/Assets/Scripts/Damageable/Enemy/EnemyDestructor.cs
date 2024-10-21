using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class EnemyDestructor : MonoBehaviour
{
    [SerializeField] private GameObject _parts;
    [SerializeField] private MonoBehaviour[] _activatableComponents;

    private Enemy _enemy;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;


    private void OnEnable()
    {
        _enemy.EnemyAppeared += Struct;
        _enemy.EnemyDied += Destruct;
    }

    private void OnDisable()
    {
        _enemy.EnemyAppeared -= Struct;
        _enemy.EnemyDied -= Destruct;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Struct()
    {
        SetActivatableComponentsState(true);

        _parts.SetActive(false);
    }

    private void Destruct()
    {
        SetActivatableComponentsState(false);

        _parts.SetActive(true);
    }

    private void SetActivatableComponentsState(bool isActivate)
    {
        _rigidbody.isKinematic = !isActivate;

        _collider.enabled = isActivate;
        _meshRenderer.enabled = isActivate;

        foreach (MonoBehaviour component in _activatableComponents)
            component.enabled = isActivate;
    }
}
