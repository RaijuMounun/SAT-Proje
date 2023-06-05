using UnityEngine;
using Random = UnityEngine.Random;

public class SwordParentCont : MonoBehaviour
{
    public Vector2 damageRange;
    
    Camera _camera;
    Transform _playerTransform;
    CharacterController _playerSc;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _playerSc = CharacterController.Instance;
        _playerTransform = _playerSc.transform;
    }
    private void Update()
    {
        var mousePos = Input.mousePosition;  
        mousePos.z = -_camera.transform.position.z;  
        var worldPos = _camera.ScreenToWorldPoint(mousePos);

        var transform1 = transform;
        var spriteDirection = worldPos - transform1.position;  
        transform1.up = spriteDirection;
        transform1.position = _playerTransform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        other.GetComponent<EnemyController>().health -= Random.Range((float)damageRange.x, (float)damageRange.y);
    }
}
