using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HeroShooter))]
public class HeroMover : MonoBehaviour
{
    [SerializeField] private WaypointSystem _waypointSystem;
    [SerializeField] private HeroAnimations _animations;
    private Waypoint _currentWaypoint;
    private NavMeshAgent _navMeshAgent;
    private HeroShooter _heroShooter;
    private float _destinationReachedTreshold;

    public event Action WaypointReached;

    public Waypoint CurrentWaypoint
    {
        get => _currentWaypoint;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _heroShooter = GetComponent<HeroShooter>();
        _destinationReachedTreshold = _navMeshAgent.stoppingDistance;
        _currentWaypoint = _waypointSystem.FirstWaypoint;
    }

    private IEnumerator CheckWaypoint()
    {
        while (true)
        {
            var distance = Vector3.Distance(transform.position, _currentWaypoint.transform.position);

            if (distance < _destinationReachedTreshold)
            {
                CheckForEndGame();
                _navMeshAgent.isStopped = true;
                _animations.IdleAnimation();
                
                CurrentWaypoint.AllEnemiesDied += SetNewDestination;
                WaypointReached?.Invoke();
                yield break;
            }

            yield return null;
        }
    }

    public void SetNewDestination()
    {
        _animations.RunAnimation();
        _navMeshAgent.isStopped = false;
        _heroShooter.canShoot = false;
        _currentWaypoint.AllEnemiesDied -= SetNewDestination;
        _currentWaypoint = _waypointSystem.NextWaypoint;
        _navMeshAgent.SetDestination(CurrentWaypoint.transform.position);
        StartCoroutine(CheckWaypoint());
    }

    private void CheckForEndGame()
    {
        if (CurrentWaypoint == _waypointSystem.LastWaypoint)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}