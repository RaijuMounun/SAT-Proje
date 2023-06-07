using System;
using System.Collections;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    GameManager _gameManager;
    bool _isAnimationPlayed;
    
    Animator _animator;
    BoxCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = transform.parent.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _gameManager = GameManager.instance;
    }

    private void Update()
    {
        if(_gameManager.isGamePaused || !_gameManager.isRunStarted) return;
        if (Input.GetMouseButtonDown(0)) OnClick();
    }

    void OnClick() {
        if(_isAnimationPlayed) return;
        _collider.enabled = true;
        _isAnimationPlayed = true;
        _animator.Play("SwordAnim", 0, 0.0f);
        StartCoroutine(StopAnimation(_animator));
    }

    IEnumerator StopAnimation(Animator animator){
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isAnimationPlayed", false);
        animator.Play("Idle");
        _isAnimationPlayed = false;
        _collider.enabled = false;
    }

    
}
