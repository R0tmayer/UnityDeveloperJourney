using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class GamePhaseHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _menuObjects;
    [SerializeField] private GameObject[] _playObjects;
    [SerializeField] private GameObject[] _loseObjects;

    [SerializeField] private MonoBehaviour[] _playScripts;

    [SerializeField] private float _timeBetweenPhase;


    private delegate void Phase();
    private Phase _playPhase;
    private Phase _losePhase;
    private Phase _menuPhase;


    private void OnEnable() => _player.PlayerDied += SetLose;

    private void OnDisable() => _player.PlayerDied -= SetLose;

    private void Start()
    {
        _playPhase = SetPlayPhase;
        _losePhase = SetLosePhase;
        _menuPhase = SetMenuPhase;
    }

    public void SetPlay() => StartCoroutine(SetPhase(_playPhase));
    
    public void SetLose() => StartCoroutine(SetPhase(_losePhase));
    
    public void SetMenu() => StartCoroutine(SetPhase(_menuPhase));

    private void SetPlayPhase()
    {
        SetObjectsState(_menuObjects, false);
        SetObjectsState(_playObjects, true);
        SetSctiptsState(_playScripts, true);
    }

    private void SetMenuPhase() => SceneManager.LoadScene("SampleScene");

    private void SetLosePhase()
    {
        SetObjectsState(_playObjects, false);
        SetObjectsState(_loseObjects, true);
    }

    private void SetObjectsState(GameObject[] objects, bool state)
    {
        foreach (GameObject obj in objects)
            obj.SetActive(state);
    }

    private void SetSctiptsState(MonoBehaviour[] scripts, bool state)
    {
        foreach (MonoBehaviour script in scripts)
            script.enabled = state;
    }
    
    private IEnumerator SetPhase(Phase phase)
    {
        yield return new WaitForSeconds(_timeBetweenPhase);

        phase?.Invoke();
    }
}
