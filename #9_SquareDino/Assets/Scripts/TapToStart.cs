using UnityEngine;

public class TapToStart : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMover;
    [SerializeField] private Canvas _canvas;

    private bool _gameStarted;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_gameStarted)
        {
            _heroMover.SetNewDestination();
            _canvas.enabled = false;
            _gameStarted = true;
        }
    }
}
