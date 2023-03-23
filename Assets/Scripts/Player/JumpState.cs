using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _legs;
    [SerializeField] private float _jumpPower = 5;
    [SerializeField] private float _legsRadius = 0.2f;

    public void TryJump()
    {
        if (Physics2D.OverlapCircle(_legs.position, _legsRadius, _groundMask) == false)
        {
            Complete();
            return;
        }

        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }
}
