using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimationNames
{
    Attack,
    Dash,
    Falling,
    Jump,
    Idle,
    Run
}

public class State : MonoBehaviour
{
    [SerializeField] private bool _canBeInterrupted;
    [SerializeField] private AnimationNames _animationName;

    protected Animator Animator;
    protected Coroutine ActiveCoroutine;
    protected float BaseAnimationTime;

    public bool CanBeInterrupted => _canBeInterrupted;

    public event UnityAction ActionCompleted;

    public void Enter()
    {
        Animator = GetComponent<Animator>();
        ActiveCoroutine = null;
        Animator.SetTrigger(_animationName.ToString());
    }

    protected void Complete()
    {
        ActiveCoroutine = null;
        ActionCompleted?.Invoke();
    }
}
