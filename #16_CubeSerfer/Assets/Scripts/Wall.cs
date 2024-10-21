using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            cube.StopMovement();
            GetComponent<Collider>().enabled = false;
        }
    }

}

