using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordParentCont : MonoBehaviour
{
    public Vector2 damageRange;
    
    Camera _camera;
    Transform _playerTransform;
    CharacterController _playerSc;
    GameManager _gameManager;
    
    
    private void Start()
    {
        _gameManager = GameManager.instance;
        _camera = Camera.main;
        _playerSc = CharacterController.Instance;
        _playerTransform = _playerSc.transform;
    }
    private void Update()
    {
        if (_gameManager.isGamePaused || !_gameManager.isRunStarted) return;
        SwordMovement();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
    }

    void SwordMovement()
    {
        //Get mouse position
        var mousePos = Input.mousePosition; //Get mouse position
        mousePos.z = -_camera.transform.position.z;  //Set z to be the distance from the camera
        var worldPos = _camera.ScreenToWorldPoint(mousePos); //Convert to world position
        //Turn sword to mouse position
        var transform1 = transform;
        var spriteDirection = worldPos - transform1.position;  //Get direction
        transform1.up = spriteDirection; //Turn to direction
        transform1.position = _playerTransform.position; //Set position to player position
    }

    void Hit(Collider2D otherCol)
    {
        if (!otherCol.CompareTag("Enemy")) return; //Return if not enemy
        otherCol.GetComponent<EnemyController>().health -= Random.Range((float)damageRange.x, (float)damageRange.y); //Damage
        otherCol.GetComponent<EnemyController>().BossAttack();
        StartCoroutine(KnockBack(otherCol));
    }

    IEnumerator KnockBack(Collider2D col)
    {
        yield return new WaitForSeconds(.3f);
        var direction = (col.transform.position - _playerTransform.position).normalized; //Direction for knockback
        col.transform.position += direction * 1f; //Knockback
    }
}
