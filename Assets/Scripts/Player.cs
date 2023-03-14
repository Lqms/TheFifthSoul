using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private PlayerAnimator _animator;

    private Vector3 _lookDirection;

    private void OnEnable()
    {
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.DashKeyPressed += OnDashKeyPressed;
        PlayerInput.JumpKeyPressed += OnJumpKeyPressed;
        PlayerInput.SprintKeyPressed += OnSprintKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.DashKeyPressed -= OnDashKeyPressed;
        PlayerInput.JumpKeyPressed -= OnJumpKeyPressed;
        PlayerInput.SprintKeyPressed -= OnSprintKeyPressed;
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
            _animator.FlipSpriteX(direction == Vector2.left);
        }
    }
}
