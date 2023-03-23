using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private MovementState _movementState;
    [SerializeField] private IdleState _idleState;
    [SerializeField] private JumpState _jumpState;

    private State _currentState;

    private void OnEnable()
    {
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.DashKeyPressed += OnDashKeyPressed;
        PlayerInput.JumpKeyPressed += OnJumpKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.DashKeyPressed -= OnDashKeyPressed;
        PlayerInput.JumpKeyPressed -= OnJumpKeyPressed;
    }

    private void Start()
    {
        _currentState = _idleState;
        _currentState.enabled = true;
        _currentState.Enter();
    }

    private bool TryChangeState(State newState)
    {
        if (_currentState.CanBeInterrupted == false)
            return false;

        ChangeState(newState);

        return true;
    }

    private void ChangeState(State newState)
    {
        if (newState == _currentState)
            return;

        _currentState.ActionCompleted -= OnActionCompleted;
        _currentState.enabled = false;

        _currentState = newState;

        _currentState.enabled = true;
        _currentState.Enter();
        _currentState.ActionCompleted += OnActionCompleted;
    }

    private void OnActionCompleted()
    {
        ChangeState(_idleState);
    }

    private void OnJumpKeyPressed()
    {
        if (TryChangeState(_jumpState))
        {

        }
    }

    private void OnDashKeyPressed()
    {

    }

    private void OnMoveKeyPressing(Vector2Int direction)
    {
        if (TryChangeState(_movementState))
        {
            _movementState.TryMove(direction);
        }
    }
}
