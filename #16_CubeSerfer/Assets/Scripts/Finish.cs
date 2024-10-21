using Dreamteck.Splines;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private CrystallAnimation _crystallAnimation;

    private void Start()
    {
        _crystallAnimation = FindObjectOfType<CrystallAnimation>();
    }

    public void EndLevel()
    {
        PlayerProgress.CURRENT_LEVEL++;
        _crystallAnimation.SpawnAnimation();

        Debug.Log("Поздравляем, ты прошёл игру");
    }
    
}
