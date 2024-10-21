using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;
using Photon.Realtime;

public class ApplicationController : MonoBehaviourPunCallbacks
{
    private ASceneController _sceneController;
    private NetworkService _networkService;
    private SessionContext _sessionContext;
    private IEnumerator _connectToPhotonCoroutine;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        _sessionContext = new SessionContext();
        _networkService = FindObjectOfType<NetworkService>();
        _networkService.Inject(_sessionContext);

        if (!PhotonNetwork.IsConnected)
        {
            ConnectToPhoton();
        }

        SceneManager.LoadScene("MenuScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _sceneController = FindObjectOfType<ASceneController>();
        _sceneController.RequestedSceneSwitchEvent += OnRequestedSceneSwitch;

        if (_sceneController == null)
        {
            Debug.LogError("Scene Controller not founded!!!");
            return;
        }

        _sceneController.Initialization(_sessionContext, _networkService);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon!");

        if (_connectToPhotonCoroutine != null)
        {
            StopCoroutine(_connectToPhotonCoroutine);
            _connectToPhotonCoroutine = null;
        }
    }

    private IEnumerator TryConnectToPhoton()
    {
        while (true)
        {
            Debug.Log("Try connect to Photon...");
            ConnectToPhoton();
            yield return new WaitForSeconds(5);
        }
    }

    private void ConnectToPhoton()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (_connectToPhotonCoroutine == null)
        {
            _connectToPhotonCoroutine = TryConnectToPhoton();
            StartCoroutine(_connectToPhotonCoroutine);
        }
    }

    private void OnRequestedSceneSwitch(string sceneName)
    {
        _sceneController.RequestedSceneSwitchEvent -= OnRequestedSceneSwitch;
        SceneManager.LoadScene(sceneName);
    }
}

