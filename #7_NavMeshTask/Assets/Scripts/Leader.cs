using System.Collections;
using System.Linq;
using UnityEngine;

public class Leader : NPC
{
    private readonly float _timeToCloseDoor = 3;
    private readonly float _timeToChangeRoom = 10;
    private float _roomChangingTimer;

    protected override void Update()
    {
        base.Update();

        _roomChangingTimer += Time.deltaTime;

        if (_roomChangingTimer >= _timeToChangeRoom)
        {
            _roomChangingTimer = 0;
            targetDoor = currentRoom.GetRandomDoorInRoom();

            if (targetDoor == null)
            {
                return;
            }

            isOnWay = true;
            StartCoroutine(MoveToDoorCoroutine());
        }
    }

    private IEnumerator MoveToDoorCoroutine()
    {
        agent.SetDestination(targetDoor.transform.position);
        yield return new WaitUntil(() => agent.remainingDistance < 0.1f);
        targetDoor.SetOpeningState(true);
        StartCoroutine(MoveToRoomCoroutine());
    }

    private IEnumerator MoveToRoomCoroutine()
    {
        var newRoom = GetOtherRoom();

        if (CurrentRoom.NPCs.Any())
        {
            var pickedFollower = CurrentRoom.GetRandomFollower;

            if (pickedFollower != null)
            {
                pickedFollower.MoveToAnotherRoom(newRoom);
            }
        }

        CurrentRoom = newRoom;
        targetPosition = GetNewRandomMovePosition(currentRoom.transform);
        agent.SetDestination(targetPosition);
        yield return new WaitForSeconds(_timeToCloseDoor);
        targetDoor.SetOpeningState(false);
        isOnWay = false;
    }
}
