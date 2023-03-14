using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode _dashKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _sprinktKey = KeyCode.LeftControl;

    public static event UnityAction<Vector2Int> MoveKeyPressing;
    public static event UnityAction DashKeyPressed;
    public static event UnityAction JumpKeyPressed;
    public static event UnityAction InteractKeyPressed;
    public static event UnityAction<bool> SprintKeyPressed;

    private void FixedUpdate()
    {
        CheckMoveKeys();
    }

    private void Update()
    {
        CheckDashKey();
        CheckJumpKey();
        CheckInteractKey();
        CheckSprintKey();
    }

    private void CheckJumpKey()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyPressed?.Invoke();
        }
    }

    private void CheckDashKey()
    {
        if (Input.GetKeyDown(_dashKey))
        {
            DashKeyPressed?.Invoke();
        }
    }

    private void CheckSprintKey()
    {
        if (Input.GetKeyDown(_sprinktKey))
        {
            SprintKeyPressed?.Invoke(true);
        }

        if (Input.GetKeyUp(_sprinktKey))
        {
            SprintKeyPressed?.Invoke(false);
        }
    }

    private void CheckInteractKey()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            InteractKeyPressed?.Invoke();
        }
    }

    private void CheckMoveKeys()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKey(_moveLeftKey))
        {
            direction += Vector2Int.left;
        }
        else if (Input.GetKey(_moveRightKey))
        {
            direction += Vector2Int.right;
        }

        MoveKeyPressing?.Invoke(direction);
    }
}
