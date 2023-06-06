using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterController _playerSc;
    StatController _playerStatCon;
    
    public float speed = 5f;
    public float health;
    public float timer, time;
    public Vector2 damageRange;
    
    bool _isDead;
    bool _playerColliding;
    BoxCollider2D _collider2D;


    private void Start()
    {
        _playerSc = CharacterController.Instance;
        _playerStatCon = _playerSc.statController;
        timer = time-0.1f;
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(_isDead) return;
        var goPlayer = Vector2.MoveTowards(transform.position, _playerSc.transform.position, speed * Time.deltaTime);
        transform.position = goPlayer;
        
        Attack();
        Die();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) _playerColliding = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) _playerColliding = false;
    }

    void Attack()
    {
        if(!_playerColliding || _isDead) return; //Return if not colliding with player or dead
        timer += Time.deltaTime;
        if(timer < time) return; //Return if cooldown not finished
        _playerStatCon.health -= Random.Range((float)damageRange.x, (float)damageRange.y); //Damage
        timer = 0; //Reset cooldown
    }

    void Die()
    {
        if(health > 0) return;
        _collider2D.enabled = false;
        _isDead = true;
        Destroy(gameObject, 2f);
    }
}
