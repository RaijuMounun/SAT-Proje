using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager _gameManager;
    
    Transform _playerTransform;
    [SerializeField] float moveSpeed = 5f;

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
        _playerTransform.position += new Vector3(horizontal, vertical, 0) * (moveSpeed * Time.deltaTime);
    }
}
