using UnityEngine;

public class StatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 30f, maxHealth = 30f;


    private void Update()
    {
        Die();
    }


    void Die()
    {
        if(health > 0) return;
        //Gameover
    }
}
