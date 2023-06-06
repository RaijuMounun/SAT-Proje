using System.Collections;
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
        SwordMovement();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Hit(other));
    }

    void SwordMovement()
    {
        //Get mouse position
        var mousePos = Input.mousePosition;  
        mousePos.z = -_camera.transform.position.z;  
        var worldPos = _camera.ScreenToWorldPoint(mousePos);
        //Turn sword to mouse position
        var transform1 = transform;
        var spriteDirection = worldPos - transform1.position;  
        transform1.up = spriteDirection;
        transform1.position = _playerTransform.position;
    }

    IEnumerator Hit(Collider2D otherCol)
    {
        if (!otherCol.CompareTag("Enemy")) yield return null; //Return if not enemy
        otherCol.GetComponent<EnemyController>().health -= Random.Range((float)damageRange.x, (float)damageRange.y); //Damage

        yield return new WaitForSeconds(0.3f);
        var direction = (otherCol.transform.position - _playerTransform.position).normalized; //Direction for knockback
        otherCol.transform.position += direction * 1f; //Knockback
    }
}
