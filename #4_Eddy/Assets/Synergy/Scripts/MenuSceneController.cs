using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuSceneController : ASceneController
{
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _joinRoomButton;
    [SerializeField] private InputField _userIdInputField;

    private Dictionary<string, RoomInfo> _cachedRoomList = new Dictionary<string, RoomInfo>();
    private string _cachedRoomName = null;

    public override void Initialization(SessionContext sessionContext, NetworkService networkService)
    {
        networkService.GotPresetResponce += OnGotPresetResponse;
        base.Initialization(sessionContext, networkService);
        EnableButtons();
        _joinRoomButton.interactable = false;
        UpdateButtons(false);

    }

    private void EnableButtons()
    {
        _userIdInputField.gameObject.SetActive(sessionContext.userRole == UserRole.Undefined);
        _joinRoomButton.gameObject.SetActive(sessionContext.userRole == UserRole.Student);
        _createRoomButton.gameObject.SetActive(sessionContext.userRole == UserRole.Teacher);
    }

    private void UpdateButtons(bool enabled)
    {
        _userIdInputField.interactable = enabled;
        _userIdInputField.placeholder.GetComponent<Text>().text = "Write your code here...";

        _createRoomButton.interactable = enabled;
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        networkService.StartPresetResponceCoroutine("84771");
        UpdateButtons(true);
    }

    private void OnDestroy()
    {
        networkService.GotPresetResponce -= OnGotPresetResponse;
    }

    public void OnEndEditInputField()
    {
        //networkService.StartPresetResponceCoroutine(_userIdInputField.text);
    }

    public void JoinRoomForStudent()
    {
        if (_cachedRoomName != null)
        {
            PhotonNetwork.JoinRoom(_cachedRoomName);
        }
    }

    public void CreateRoomForTeacher()
    {
        PhotonNetwork.CreateRoom(sessionContext.userID.ToString(), new RoomOptions { MaxPlayers = 8, IsVisible = true });
    }

    public override void OnJoinedRoom()
    {
        RequestSceneSwitch(StaticSceneName.DemoSceneName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        UpdateButtons(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        FindAvailableRoom();
    }

    private void OnGotPresetResponse(GetPresetResponse presetResponse, string inputUserID)
    {
#if UNITY_EDITOR
        sessionContext.Initialization(presetResponse, presetResponse.users, presetResponse.userRole, inputUserID);
        EnableButtons();
#else
        sessionContext.Initialization(presetResponse, presetResponse.users, presetResponse.userRole, inputUserID);
        EnableButtons();
#endif
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        _cachedRoomList.Clear();

        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo tempRoom = roomList[i];

            if (tempRoom.RemovedFromList)
            {
                _cachedRoomList.Remove(tempRoom.Name);
            }
            else
            {
                _cachedRoomList.Add(tempRoom.Name, tempRoom);
            }
        }
    }

    private void FindAvailableRoom()
    {
        if (sessionContext.userRole == UserRole.Student)
        {
            _cachedRoomName = null;

            foreach (int teacherID in sessionContext.relatedTeachersIds)
            {

                if (_cachedRoomList.ContainsKey(teacherID.ToString()))
                {
                    _cachedRoomName = teacherID.ToString();
                    _joinRoomButton.interactable = true;
                    return;
                }
            }

            if (_cachedRoomName == null)
            {
                _joinRoomButton.interactable = false;
            }
        }
    }

}
