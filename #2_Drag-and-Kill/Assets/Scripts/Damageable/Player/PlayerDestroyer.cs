using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _scriptsToOff;
    [SerializeField] private GameObject _partContainer;

    private Player _player;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;


    private void OnEnable() => _player.PlayerDied += DestroyPlayer;

    private void OnDisable() => _player.PlayerDied -= DestroyPlayer;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void DestroyPlayer()
    {
        foreach (MonoBehaviour script in _scriptsToOff)
            script.enabled = false;

        _collider.enabled = false;
        _meshRenderer.enabled = false;
        _rigidbody.isKinematic = true;

        _partContainer.SetActive(true);
    }
}
