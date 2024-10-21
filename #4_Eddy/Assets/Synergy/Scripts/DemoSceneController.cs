using Photon.Pun;
using Photon.Realtime;

public class DemoSceneController : ASceneController
{
    public override void OnLeftRoom()
    {
        RequestSceneSwitch(StaticSceneName.MenuSceneName);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        var currentStudents = PhotonNetwork.CurrentRoom.Players;

        foreach (var student in currentStudents)
        {
            PhotonNetwork.CloseConnection(student.Value);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
