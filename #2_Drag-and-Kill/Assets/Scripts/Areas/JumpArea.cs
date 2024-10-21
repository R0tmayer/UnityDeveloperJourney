using UnityEngine;

public class JumpArea : MonoBehaviour
{
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private float _jumpForce;


    private void OnEnable() => _detector.PlayerTriggered += TryJump;

    private void OnDisable() => _detector.PlayerTriggered -= TryJump;

    private void TryJump(Player player)
	{
        if (player != null)
        {
            if (player.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);

                gameObject.SetActive(false);
            }
        }
	}
}
