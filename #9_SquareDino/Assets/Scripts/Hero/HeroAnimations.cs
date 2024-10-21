using UnityEngine;

public class HeroAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int Run = Animator.StringToHash("Run");

    public void RunAnimation()
    {
        _animator.SetBool(Run, true);
    }
    
    public void IdleAnimation()
    {
        _animator.SetBool(Run, false);
    }
}
