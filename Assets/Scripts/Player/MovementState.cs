using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 5;

    public void TryMove(Vector2Int direction)
    {
        if (_rigidbody.velocity.x == 0 && direction == Vector2.zero)
        {
            Complete();
            return;
        }

        if (ActiveCoroutine != null)
            StopCoroutine(ActiveCoroutine);

        ActiveCoroutine = StartCoroutine(Moving(direction));
    }

    private IEnumerator Moving(Vector2Int direction)
    {
        while (Input.GetAxis("Horizontal") != 0)
        {
            _rigidbody.velocity = new Vector2(direction.x * _speed, _rigidbody.velocity.y);
            yield return null;
        }

        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        Complete();
    }
}
