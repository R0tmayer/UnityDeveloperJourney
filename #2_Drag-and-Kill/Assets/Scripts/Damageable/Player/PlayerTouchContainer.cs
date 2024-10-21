using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchHandler))]
public class PlayerTouchContainer : MonoBehaviour
{
    private PlayerTouchHandler _touchHandler;

    public event UnityAction<Vector2, Vector2> PositionChanged;
    public event UnityAction<Vector2, Vector2> PositionsDeleted;

    private Vector2 _touchStartPosition;
    private Vector2 _touchCurrentPosition;


    private void OnEnable()
    {
        _touchHandler.TouchStarted += SetTouchStartPosition;
        _touchHandler.TouchMoved += SetTouchCurrentPosition;
        _touchHandler.TouchEnded += PositionsDelete;
    }

    private void OnDisable()
    {
        _touchHandler.TouchStarted -= SetTouchStartPosition;
        _touchHandler.TouchMoved -= SetTouchCurrentPosition;
        _touchHandler.TouchEnded -= PositionsDelete;
    }

    private void Awake() => _touchHandler = GetComponent<PlayerTouchHandler>();

    private void SetTouchStartPosition(Vector2 touchPosition) => _touchStartPosition = touchPosition;

    private void SetTouchCurrentPosition(Vector2 touchPosition)
    {
        _touchCurrentPosition = touchPosition;

        PositionChanged?.Invoke(_touchStartPosition, _touchCurrentPosition);
    }

    private void PositionsDelete() => PositionsDeleted?.Invoke(_touchStartPosition, _touchCurrentPosition);
}
