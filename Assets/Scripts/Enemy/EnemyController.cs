using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterController _playerSc;
    StatController _playerStatCon;
    
    public float speed = 5f;
    public float health;
    public Vector2 damageRange;
    
    bool _isDead;
    bool _playerColliding;

    public float timer, time;

    private void Start()
    {
        _playerSc = CharacterController.Instance;
        _playerStatCon = _playerSc.statController;
        timer = time-0.1f;
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
        if(!_playerColliding) return;
        timer += Time.deltaTime;
        if(timer < time) return;
        _playerStatCon.health -= Random.Range((float)damageRange.x, (float)damageRange.y);
        timer = 0;
    }

    void Die()
    {
        if(health > 0) return;
        _isDead = true;
        Destroy(gameObject, 2f);
    }
}
