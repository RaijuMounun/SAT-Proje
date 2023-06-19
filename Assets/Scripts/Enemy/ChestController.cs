using System;
using System.Linq;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Sprite openChest;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider2D;
    public bool playerInArea;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    private void Start()
    {
        GameManager.instance.chestsArray.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInArea = false;
    }

    private void Update()
    {
        if (!playerInArea) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            _spriteRenderer.sprite = openChest;
            GameManager.instance.UpgradesScreen();
            foreach (var item in GameManager.instance.chestsArray.Where(item => item != gameObject))
            {
                item.SetActive(false);
            }
            Destroy(gameObject, 3f);
        }
    }
}
