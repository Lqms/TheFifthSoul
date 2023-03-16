using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private SpriteRenderer _emptySprite;
    [SerializeField] private float _ghostSpawnDelay = 0.05f;
    [SerializeField] private float _ghostDelayMultiplier = 4f;

    private float _ghostDeSpawnDelay;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _sprintingCoroutine;

    private void Start()
    {
        _ghostDeSpawnDelay = _ghostSpawnDelay * _ghostDelayMultiplier;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetFloat("VelocityX", Mathf.Abs(_physics.Rigidbody.velocity.x));
        _animator.SetFloat("VelocityY", _physics.Rigidbody.velocity.y);
    }

    public void SwitchSprintAnimation(bool state)
    {
        if (state == true)
        {
            _sprintingCoroutine = StartCoroutine(Sprinting());
        }
        else
        {
            StopCoroutine(_sprintingCoroutine);
        }
    }

    public void TurnOnTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    private IEnumerator Sprinting()
    {
        while (true)
        {
            var emptySprite = Instantiate(_emptySprite, transform.position, Quaternion.identity);
            emptySprite.sprite = _spriteRenderer.sprite;
            emptySprite.flipX = _spriteRenderer.flipX;
            emptySprite.color = new Color(1, 1, 1, 0.3f);
            Destroy(emptySprite.gameObject, _ghostDeSpawnDelay);
            yield return new WaitForSeconds(_ghostSpawnDelay);
        }
    }
}
