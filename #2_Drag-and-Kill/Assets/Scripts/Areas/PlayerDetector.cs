using UnityEngine.Events;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event UnityAction<Player> PlayerTriggered;


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerTriggered?.Invoke(player);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
            PlayerTriggered?.Invoke(null);
    }
}
