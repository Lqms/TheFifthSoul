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
    [SerializeField] private float _dashReloadingTime = 3;
    [SerializeField] private float _dashCompletingTime = 0.2f;

    [Header("Jump")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _legs;
    [SerializeField] private float _jumpPower = 5;
    [SerializeField] private float _legsRadius = 0.2f;
 
    public Rigidbody2D Rigidbody { get; private set; }
    
    private Coroutine _dashCompletingCoroutine;
    private Coroutine _dashReloadingCoroutine;
    private WaitForSeconds _completingDashDelay;
    private WaitForSeconds _reloadingDashDelay;

    public bool OnGround { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _completingDashDelay = new WaitForSeconds(_dashCompletingTime);
        _reloadingDashDelay = new WaitForSeconds(_dashReloadingTime);
    }

    private void FixedUpdate()
    {
        OnGround = CheckJumpingPossibility();
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
        if (OnGround == false)
            return;

        Rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    public void TryMove(Vector2Int direction)
    {
        if (_dashCompletingCoroutine != null)
            return;

        if (Rigidbody.velocity.x == 0 && direction == Vector2.zero)
            return;

        Rigidbody.velocity = new Vector2(direction.x * _speed, Rigidbody.velocity.y);
    }

    public void TryDash(Vector2 direction)
    {
        if (_dashReloadingCoroutine != null)
            return;

        if (_dashCompletingCoroutine != null)
            StopCoroutine(_dashCompletingCoroutine);

        Rigidbody.AddForce(direction * _dashPower, ForceMode2D.Impulse);

        _dashCompletingCoroutine = StartCoroutine(DashCompleting());
        _dashReloadingCoroutine = StartCoroutine(DashReloading());
    }
}
