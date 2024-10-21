using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystallAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _crystallUIText;
    [SerializeField] private GameObject animatedCrystallPrefab;
    [SerializeField] private Transform _target;

    [Space]
    [SerializeField] private int _crystallsToSpawn;
    private Queue<GameObject> _crystallsEnqueue = new Queue<GameObject>();

    [Space]
    [SerializeField] [Range(0.5f, 2f)] private float _firstAnimDuration;
    [SerializeField] private Ease _firstEaseType;

    [Space]
    [SerializeField] [Range(0.5f, 2f)] private float _secondAnimDuration;
    [SerializeField] private Ease _secondEaseType;

    [Space]
    [SerializeField] private float _spread;
    [SerializeField] private float _increasedScale;


    Sequence sequence;

    private void Awake()
    {
        CreatePool();
    }

    private void Start()
    {
        sequence = DOTween.Sequence();
        _crystallUIText.text = PlayerProgress.CRYSTALLS.ToString();
    }

    private void CreatePool()
    {
        GameObject crystall;

        for (int i = 0; i < _crystallsToSpawn; i++)
        {
            crystall = Instantiate(animatedCrystallPrefab, transform);
            crystall.transform.parent = transform;
            crystall.SetActive(false);
            _crystallsEnqueue.Enqueue(crystall);
        }
    }

    public void SpawnAnimation()
    {
        StartCoroutine(IncreaseCrystalls());

        for (int i = 0; i < _crystallsToSpawn; i++)
        {
            if (_crystallsEnqueue.Count > 0)
            {
                GameObject crystall = _crystallsEnqueue.Dequeue();
                crystall.SetActive(true);

                var randomXspread = Random.Range(-_spread, _spread);
                var randomYspread = Random.Range(-_spread, _spread);
                var randomOffsetSpread = new Vector3(randomXspread, randomYspread, 0);

                crystall.transform.DOMove(randomOffsetSpread, _firstAnimDuration).SetRelative(true).SetEase(_firstEaseType);
                crystall.transform.DOScale(_increasedScale, _firstAnimDuration).SetRelative(true).SetEase(_firstEaseType)
                    .OnComplete(() =>
                    {
                        MoveToTargetAnimation(crystall);
                    });
            }
        }

        UISingleton.Instance.ShowNextLevelButton();
    }

    private void MoveToTargetAnimation(GameObject crystall)
    {
        crystall.transform.DOMove(_target.position, _secondAnimDuration).SetEase(_secondEaseType)
            .OnComplete(() =>
            {
                crystall.SetActive(false);
                _crystallsEnqueue.Enqueue(crystall);
            });
    }

    private IEnumerator IncreaseCrystalls()
    {
        yield return new WaitForSeconds(1f);

        PlayerProgress.CRYSTALLS += 100;
        PlayerProgress.SaveData();
        _crystallUIText.text = PlayerProgress.CRYSTALLS.ToString();
    }
}
