using UnityEngine;

[RequireComponent(typeof(PlayerTouchContainer))]
public class PlayerRotator : MonoBehaviour
{
    private PlayerTouchContainer _touchContainer;


    private void OnEnable() => _touchContainer.PositionChanged += Rotate;

    private void OnDisable() => _touchContainer.PositionChanged -= Rotate;

    private void Awake() => _touchContainer = GetComponent<PlayerTouchContainer>();

    private void Rotate(Vector2 startPosition, Vector2 currentPosition)
    {
        Vector3 direction = GetDirection(startPosition, currentPosition);

        transform.rotation = Quaternion.LookRotation(direction);
    }

    private Vector3 GetDirection(Vector3 startPosition, Vector3 currentPosition)
    {
        startPosition = Camera.main.ScreenToViewportPoint(startPosition);
        currentPosition = Camera.main.ScreenToViewportPoint(currentPosition);

        startPosition = new Vector3(startPosition.x, 0f, startPosition.y);
        currentPosition = new Vector3(currentPosition.x, 0f, currentPosition.y);

        return (startPosition - currentPosition).normalized;
    }
}
