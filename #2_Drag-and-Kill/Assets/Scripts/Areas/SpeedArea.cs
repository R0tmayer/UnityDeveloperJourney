using UnityEngine;

public class SpeedArea : MonoBehaviour
{
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private float _pushForce;


    private void OnEnable() => _detector.PlayerTriggered += TryJump;

    private void OnDisable() => _detector.PlayerTriggered -= TryJump;

    private void TryJump(Player player)
    {
        if (player != null)
        {
            if (player.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(Vector3.forward * _pushForce, ForceMode.VelocityChange);

                gameObject.SetActive(false);
            }
        }
    }
}
