using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Grounded += OnPlayerGrounded;
        _player.Jumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        _player.Grounded -= OnPlayerGrounded;
        _player.Jumped -= OnPlayerJumped;
    }

    private void OnPlayerGrounded()
    {
        _animator.ResetTrigger("JumpTrigger");
        _animator.SetTrigger("RunTrigger");
    }

    public void OnPlayerJumped()
    {
        _animator.ResetTrigger("RunTrigger");
        _animator.SetTrigger("JumpTrigger");
    }
}
