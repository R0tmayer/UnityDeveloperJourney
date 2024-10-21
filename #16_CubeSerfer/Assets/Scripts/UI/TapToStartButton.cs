using System;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartButton : MonoBehaviour
{
    [SerializeField] private Button _tapToStartButton;
    
    private CubesHolder _cubesHolder;
    private SplineFollower _splineFollower;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cubesHolder = FindObjectOfType<CubesHolder>();
        _splineFollower = _cubesHolder.GetComponent<SplineFollower>();
        
        _splineFollower.followSpeed = 0;

        _tapToStartButton.onClick.AddListener(() =>
        {
            _splineFollower.followSpeed = 10;
            gameObject.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        _tapToStartButton.onClick.RemoveAllListeners();
    }

}
