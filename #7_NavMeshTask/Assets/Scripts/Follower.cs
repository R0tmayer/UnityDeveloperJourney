using System.Collections;
using UnityEngine;

public class Follower : NPC
{
    [SerializeField] private Material _followerMaterial;
    public void MoveToAnotherRoom(Room room)
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = _followerMaterial;
        StartCoroutine(MoveToRoomCoroutine(room));
    }

    private IEnumerator MoveToRoomCoroutine(Room room)
    {
        isOnWay = true;
        targetPosition = GetNewRandomMovePosition(room.transform);
        agent.SetDestination(targetPosition);
        yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

        if (targetPosition == transform.position)
        {
            CurrentRoom = room;
        }

        isOnWay = false;
    }

}
