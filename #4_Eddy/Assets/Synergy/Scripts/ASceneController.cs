using UnityEngine.Events;
using Photon.Pun;

public abstract class ASceneController : MonoBehaviourPunCallbacks
{
    protected SessionContext sessionContext;
    protected NetworkService networkService;
    
    public event UnityAction<string> RequestedSceneSwitchEvent;

    public virtual void Initialization(SessionContext sessionContext, NetworkService networkService)
    {
        this.sessionContext = sessionContext;
        this.networkService = networkService;
    }

    protected void RequestSceneSwitch(string sceneName)
    {
        RequestedSceneSwitchEvent?.Invoke(sceneName);
    }
}
