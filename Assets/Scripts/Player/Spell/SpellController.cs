using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpellController : MonoBehaviour
{
    
    public WandController _wandController;

    Vector3 _direction;
    float _timer;
    bool _isHit;

    private void Awake()
    {
        _wandController = GameObject.Find("Wand").GetComponent<WandController>();
    }

    private void OnEnable()
    {
        var wandTransform = _wandController.transform;
        var transform1 = transform;
        _direction = transform1.up.normalized;
        transform1.position = wandTransform.position + (wandTransform.up.normalized * .7f);
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        var transform1 = transform;
        transform1.position += transform1.up * (_wandController.spellSpeed * Time.deltaTime);
        _timer += Time.deltaTime;
        if (_timer >= _wandController.spellLifeTime && !_isHit)
        {
            gameObject.SetActive(false);
            _timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
    }

    void Hit(Collider2D otherCol)
    {
        if(!otherCol.CompareTag("Enemy"))return; //Return if not enemy
        otherCol.GetComponent<EnemyController>().health -= Random.Range((float)_wandController.spellDamageRange.x, (float)_wandController.spellDamageRange.y); //Damage
        _isHit = true;
        otherCol.GetComponent<EnemyController>().BossAttack();
        StartCoroutine(KnockBack(otherCol));
        StartCoroutine(DeactivateSelf());
    }

    IEnumerator KnockBack(Collider2D col)
    {
        yield return new WaitForSeconds(.16f);
        col.transform.position += _direction * _wandController.knockBackPower; //Knockback
    }
    IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(0.3f);
        _isHit = false;
        gameObject.SetActive(false);
    }

    
}
