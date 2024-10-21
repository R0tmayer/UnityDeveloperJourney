using UnityEngine.Events;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
    public event UnityAction<Vector2> TouchStarted;
    public event UnityAction<Vector2> TouchMoved;
    public event UnityAction TouchEnded;


    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began: TouchStarted?.Invoke(touch.position); break;
                case TouchPhase.Moved: TouchMoved?.Invoke(touch.position); break;
                case TouchPhase.Ended: TouchEnded?.Invoke(); break;
            }
        }
    }
}
