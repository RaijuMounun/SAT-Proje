using System.Collections;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    Camera _camera;
    Transform _playerTransform;
    public bool _isAnimationPlayed;
    Animator _animator;
    BoxCollider2D _collider;

    private void Awake()
    {
        _camera = Camera.main;
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        var mousePos = Input.mousePosition;  
        mousePos.z = -_camera.transform.position.z;  
        var worldPos = _camera.ScreenToWorldPoint(mousePos);

        var transform1 = transform;
        var spriteDirection = worldPos - transform1.position;  
        transform1.up = spriteDirection;
        transform1.position = _playerTransform.position;
        
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
