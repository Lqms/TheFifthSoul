using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private SpriteRenderer _emptySprite;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _sprintingCoroutine;

    private void Start()
    {
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
            Destroy(emptySprite.gameObject, 0.4f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void FlipSpriteX(bool state)
    {
        _spriteRenderer.flipX = state;
    }
}
