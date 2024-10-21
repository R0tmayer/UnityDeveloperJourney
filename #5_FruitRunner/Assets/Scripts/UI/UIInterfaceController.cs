using UnityEngine;
using UnityEngine.UI;

public class UIInterfaceController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _scoreBoardCanvasGroup;
    [SerializeField] private Text _resultScorePointText;
    [SerializeField] private Text _resultFruitsText;
    [SerializeField] private Text _endGameText;

    [SerializeField] private Text _displayedCollectedFruitsText;
    [SerializeField] private Text _displayedScorePointsText;

    private Player _player;
    private VictoryGate _victoryGate;
    private FruitManager _fruitManager;
    private ScorePointsManager _scorePointsManager;
    private Animator _scoreBoardPanelAnimator;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _victoryGate = FindObjectOfType<VictoryGate>();
        _fruitManager = GetComponent<FruitManager>();
        _scorePointsManager = GetComponent<ScorePointsManager>();
        _scoreBoardPanelAnimator = GetComponentInChildren<Animator>();

        Time.timeScale = 1;
        _scoreBoardCanvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _fruitManager.FruitsAmountChanged += OnFruitsAmountChanged;
        _fruitManager.CollectedFruitsGoalReached += OnCollectedFruitsGoalReached;

        _scorePointsManager.ScorePointsGoalReached += OnScorePointsGoalReached;
        _scorePointsManager.ScorePointsChanged += OnScorePointsChanged;

        _player.Died += OnDied;

        _victoryGate.ReachedVictoryGate += OnReachedVictoryGate;
    }

    private void OnDisable()
    {
        _fruitManager.FruitsAmountChanged -= OnFruitsAmountChanged;
        _fruitManager.CollectedFruitsGoalReached -= OnCollectedFruitsGoalReached;

        _scorePointsManager.ScorePointsGoalReached -= OnScorePointsGoalReached;
        _scorePointsManager.ScorePointsChanged -= OnScorePointsChanged;

        _player.Died -= OnDied;

        _victoryGate.ReachedVictoryGate -= OnReachedVictoryGate;

    }

    public void WinGame(bool win)
    {
        _scoreBoardPanelAnimator.enabled = true;
        _scoreBoardCanvasGroup.alpha = 1;
        Time.timeScale = 0;
        _resultFruitsText.text = "your fruits " + _fruitManager.CollectedFruitsAmount.ToString();
        _resultScorePointText.text = "your score " + _scorePointsManager.ScorePoints.ToString();
        _endGameText.text = win == true ? "You win!" : "You Lost! Try Again!";
    }

    private void OnFruitsAmountChanged(int collectedFruitsAmount)
    {
        _displayedCollectedFruitsText.text = collectedFruitsAmount.ToString();
    }

    private void OnScorePointsChanged(int scorePoints)
    {
        _displayedScorePointsText.text = scorePoints.ToString("0000");
    }

    private void OnCollectedFruitsGoalReached()
    {
        WinGame(true);
    }

    private void OnScorePointsGoalReached()
    {
        WinGame(true);
    }

    private void OnDied()
    {
        WinGame(false);
    }

    private void OnReachedVictoryGate()
    {
        WinGame(true);
    }
}
