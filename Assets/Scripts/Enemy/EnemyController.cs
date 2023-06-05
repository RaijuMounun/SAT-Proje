using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterController playerSc;
    Rigidbody2D rb;
    
    public float speed = 5f;
    public float health;
    public Vector2 damageRange;
    public bool isDead;
    public bool playerColliding;

    public float timer, time;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = time-0.1f;
    }

    private void Start()
    {
        playerSc = CharacterController.instance;
    }

    private void Update()
    {
        if(isDead) return;
        var goPlayer = Vector2.MoveTowards(transform.position, playerSc.transform.position, speed * Time.deltaTime);
        transform.position = goPlayer;
        
        Attack();
        Die();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) playerColliding = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) playerColliding = false;
    }

    void Attack()
    {
        if(!playerColliding) return;
        timer += Time.deltaTime;
        if(timer < time) return;
        playerSc.health -= UnityEngine.Random.Range((float)damageRange.x, (float)damageRange.y);
        timer = 0;
    }

    void Die()
    {
        if(health > 0) return;
        isDead = true;
        Destroy(gameObject, 2f);
    }
}
