using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;
    GameManager _gameManager;
    [HideInInspector] public StatController statController;
    
    Transform _playerTransform;
    [SerializeField] Image healthBar;

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

    void FixedUpdate()
    {
        Movement();
        healthBar.fillAmount = statController.health / statController.maxHealth;
    }

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
        CheckIfDead();
    }

    void EnemyBulletHit(Collider2D other)
    {
        if(!other.gameObject.name.Contains("EnemyBullet")) return;
        statController.health -= 3f;
        CheckIfDead();
    }
    
    void CheckIfDead()
    {
        if(statController.health > 0) return;
        _gameManager.isRunStarted = false;
        _gameManager.isGamePaused = true;
        _gameManager.canvasesArray[1].SetActive(true);
    }
}
