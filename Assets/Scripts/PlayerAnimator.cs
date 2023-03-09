using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _physics;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("VelocityX", Mathf.Abs(_physics.Rigidbody.velocity.x));
        _animator.SetFloat("VelocityY", _physics.Rigidbody.velocity.y);
    }

    public void TurnOnTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
