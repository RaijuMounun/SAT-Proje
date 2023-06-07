using UnityEngine;

public enum EnemyTypes {Melee, Ranged, Boss}
public class EnemyController : MonoBehaviour
{
    CharacterController _playerSc;
    StatController _playerStatCon;
    GameManager _gameManager;
    WaveController _waveController;
    public EnemyTypes enemyType;
    
    public float speed = 5f;
    public float health;
    public float timer, time, range;
    public Vector2 damageRange;
    public GameObject bulletPrefab, firePrefab;
    
    bool _isDead;
    bool _playerColliding;
    Collider2D _collider2D;


    private void Start()
    {
        _waveController = WaveController.Instance;
        _gameManager = GameManager.instance;
        _playerSc = CharacterController.Instance;
        _playerStatCon = _playerSc.statController;
        timer = time-0.1f;
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update() //Her frame'de çağırılır
    {
        if(_isDead) return;
        if(_gameManager.isGamePaused) return;
        if(!_gameManager.isRunStarted) return;
        
        MeleeMovement();
        RangedMovement();
        
        
        MeleeAttack();
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

    void MeleeAttack()
    {
        if(!_playerColliding || _isDead || enemyType == EnemyTypes.Ranged) return; //Return if not colliding with player or dead
        timer += Time.deltaTime;
        if(timer < time) return; //Return if cooldown not finished
        _playerStatCon.health -= Random.Range((float)damageRange.x, (float)damageRange.y); //Damage
        timer = 0; //Reset cooldown
    }

    void RangedAttack()
    {
        if(timer < time || enemyType != EnemyTypes.Ranged) return;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = (_playerSc.transform.position - transform.position).normalized * 10f;
        timer = 0;
        Destroy(bullet, 1f);
    }

    void Die()
    {
        if(health > 0) return;
        _collider2D.enabled = false;
        _isDead = true;
        _waveController.aliveEnemies.Remove(gameObject);
        _waveController.Control();
        Destroy(gameObject, 2f);
    }

    void MeleeMovement()
    {
        if(enemyType == EnemyTypes.Ranged) return;
        var goPlayer = Vector2.MoveTowards(transform.position, _playerSc.transform.position, speed * Time.deltaTime);
        transform.position = goPlayer;
    }

    void RangedMovement()
    {
        if(enemyType != EnemyTypes.Ranged) return;
        timer += Time.deltaTime;
        var dist = Vector2.Distance(transform.position, _playerSc.transform.position);
        var goPlayer = Vector2.MoveTowards(transform.position, _playerSc.transform.position, speed * Time.deltaTime);
        if (dist > range) transform.position = goPlayer;
        else RangedAttack();
    }
    
    public void BossAttack()
    {
        if(enemyType != EnemyTypes.Boss) return;
        

        for (int i = 0; i < 8; i++)
        {
            var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            fire.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 45 * i) * Vector2.up * 10f;
            Destroy(fire, 3f);
        }
    }
}
