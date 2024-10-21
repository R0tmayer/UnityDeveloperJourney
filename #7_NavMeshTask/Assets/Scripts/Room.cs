using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<Door> _doors = new List<Door>();

    public List<NPC> NPCs { get; private set; } = new List<NPC>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Door door))
        {
            _doors.Add(door);
        }
    }

    public List<Door> GetDoors()
    {
        return _doors;
    }

    public Door GetRandomDoorInRoom()
    {
        if (_doors.Count == 0)
        {
            return null;
        }

        return _doors[Random.Range(0, _doors.Count)];
    }

    public Follower GetRandomFollower
    {
        get
        {
            foreach (var npc in NPCs)
            {
                if (npc is Follower follower)
                {
                    return follower;
                }
            }
            return null;
        }
    }
}
