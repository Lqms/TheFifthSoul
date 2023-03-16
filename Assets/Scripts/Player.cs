using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerCombat _combat;

    private Vector3 _lookDirection;

    private void OnEnable()
    {
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.DashKeyPressed += OnDashKeyPressed;
        PlayerInput.JumpKeyPressed += OnJumpKeyPressed;
        PlayerInput.SprintKeyPressed += OnSprintKeyPressed;
        PlayerInput.AttackKeyPressed += OnAttackKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.DashKeyPressed -= OnDashKeyPressed;
        PlayerInput.JumpKeyPressed -= OnJumpKeyPressed;
        PlayerInput.SprintKeyPressed -= OnSprintKeyPressed;
        PlayerInput.AttackKeyPressed -= OnAttackKeyPressed;
    }

    private void OnAttackKeyPressed()
    {
        _combat.Attack();
        _animator.TurnOnTrigger("Attack");
    }

    private void OnSprintKeyPressed(bool isKeyDown)
    {
        print(isKeyDown);
        _animator.SwitchSprintAnimation(isKeyDown);
    }

    private void OnJumpKeyPressed()
    {
        _physics.TryJump();
    }

    private void OnDashKeyPressed()
    {
        _physics.TryDash(_lookDirection);
    }

    private void OnMoveKeyPressing(Vector2Int direction)
    {
        TryChangeDirection(direction);
        _physics.TryMove(direction);
    }

    private void TryChangeDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _lookDirection = direction;
            transform.localScale = new Vector3(_lookDirection.x, transform.localScale.y, transform.localScale.y);
        }
    }
}
