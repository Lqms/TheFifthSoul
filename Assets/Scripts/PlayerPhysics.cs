using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5;

    [Header("Dash")]
    [SerializeField] private float _dashPower = 50;
    [SerializeField] private float _dashCompletingTime = 0.3f;
    [SerializeField] private float _dashReloadingTime = 3;

    [Header("Jump")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _legs;
    [SerializeField] private float _jumpPower = 5;
    [SerializeField] private float _legsRadius = 1;
 
    private Rigidbody2D _rigidbody;
    
    private WaitForSeconds _completingDashDelay;
    private Coroutine _dashCompletingCoroutine;

    private WaitForSeconds _reloadingDashDelay;
    private Coroutine _dashReloadingCoroutine;

    public bool CanJump { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _completingDashDelay = new WaitForSeconds(_dashCompletingTime);
        _reloadingDashDelay = new WaitForSeconds(_dashReloadingTime);
    }

    private void FixedUpdate()
    {
        CanJump = CheckJumpingPossibility();
    }

    private bool CheckJumpingPossibility()
    {
        return Physics2D.OverlapCircle(_legs.position, _legsRadius, _groundMask);
    }

    private IEnumerator DashReloading()
    {
        yield return _reloadingDashDelay;

        print("Dash is ready");
        _dashReloadingCoroutine = null;
    }

    private IEnumerator DashCompleting()
    {
        yield return _completingDashDelay;

        _dashCompletingCoroutine = null;
    }

    public void TryJump()
    {
        if (CanJump == false)
            return;

        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    public void TryMove(Vector2 direction)
    {
        if (_rigidbody.velocity == Vector2.zero && direction == Vector2.zero)
            return;

        if (_dashCompletingCoroutine != null)
            return;

        _rigidbody.velocity = new Vector2(direction.x * _speed, _rigidbody.velocity.y);
    }

    public void TryDash(Vector2 direction)
    {
        if (_dashReloadingCoroutine != null)
            return;

        if (_dashCompletingCoroutine != null)
            StopCoroutine(_dashCompletingCoroutine);

        _rigidbody.AddForce(direction * _dashPower, ForceMode2D.Impulse);

        _dashCompletingCoroutine = StartCoroutine(DashCompleting());
        _dashReloadingCoroutine = StartCoroutine(DashReloading());
    }
}
