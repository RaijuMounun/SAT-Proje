using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpellController : MonoBehaviour
{
    
    WandController _wandController;

    Vector3 _direction;
    CircleCollider2D _collider2D;


    private void Awake()
    {
        _wandController = WandController.Instance;
        _collider2D = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        var transform1 = _wandController.transform;
        var transform2 = transform;
        _direction = transform2.up.normalized;
        transform2.position = transform1.position + (transform1.up.normalized * .7f);
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        var transform1 = transform;
        transform1.position += transform1.up * (_wandController.spellSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
        print("hit");
    }

    void Hit(Collider2D otherCol)
    {
        if(!otherCol.CompareTag("Enemy"))return; //Return if not enemy
        _collider2D.enabled = false;
        otherCol.GetComponent<EnemyController>().health -= Random.Range((float)_wandController.spellDamageRange.x, (float)_wandController.spellDamageRange.y); //Damage
        otherCol.transform.position += _direction * _wandController.knockBackPower; //Knockback
        StartCoroutine(DeactivateSelf());
    }
    IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
