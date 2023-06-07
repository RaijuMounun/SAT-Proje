using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;
    GameManager _gameManager;
    [HideInInspector] public StatController statController;
    
    Transform _playerTransform;

    private void Awake()
    {
        Instance = this;
        statController = GetComponent<StatController>();
    }

    void Start()
    {
        _gameManager = GameManager.instance;
        _playerTransform = GetComponent<Transform>();
    }

    void FixedUpdate() { Movement(); }

    void Movement()
    {
        if(!_gameManager.isRunStarted || _gameManager.isGamePaused) return;
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _playerTransform.position += new Vector3(horizontal, vertical, 0) * (statController.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBulletHit(other);
        BossFireHit(other);
    }

    void BossFireHit(Collider2D other)
    {
        if(!other.gameObject.name.Contains("BossFire")) return;
        statController.health -= 7f;
    }

    void EnemyBulletHit(Collider2D other)
    {
        if(!other.gameObject.name.Contains("EnemyBullet")) return;
        statController.health -= 3f;
    }
}
