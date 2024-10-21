using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPC : MonoBehaviour
{
    [SerializeField] private readonly float _timeToNewRandomMovePosition = 1;
    private float _timer;

    protected NavMeshAgent agent;
    protected bool isOnWay;
    protected Vector3 targetPosition;
    protected Room currentRoom;
    protected Door targetDoor;

    protected Room CurrentRoom
    {
        get => currentRoom;
        set
        {

            if (currentRoom != null)
            {
                if (currentRoom.NPCs.Contains(this))
                {
                    currentRoom.NPCs.Remove(this);
                }
            }

            currentRoom = value;
            currentRoom.NPCs.Add(this);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _timer = _timeToNewRandomMovePosition;
        DefineCurrentRoom();
    }

    protected virtual void Update()
    {
        _timer += Time.deltaTime;
        WanderAI();
    }

    private void WanderAI()
    {
        if (_timer >= _timeToNewRandomMovePosition && !isOnWay)
        {
            targetPosition = GetNewRandomMovePosition(currentRoom.transform);
            agent.SetDestination(targetPosition);
            _timer = 0;
        }
    }

    protected Vector3 GetNewRandomMovePosition(Transform room)
    {
        float newXpos = Random.Range(room.position.x - room.localScale.x / 2, room.position.x + room.localScale.x / 2);
        float newZpos = Random.Range(room.position.z - room.localScale.z / 2, room.position.z + room.localScale.z / 2);
        return new Vector3(newXpos, targetPosition.y, newZpos);
    }

    private void DefineCurrentRoom()
    {
        float distance = 10f;
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
        {
            CurrentRoom = hit.collider.GetComponent<Room>();
        }
    }

    protected Room GetOtherRoom()
    {
        foreach (var room in targetDoor.GetRooms())
        {
            if (currentRoom != room)
            {
                return room;
            }
        }

        return null;
    }

}
