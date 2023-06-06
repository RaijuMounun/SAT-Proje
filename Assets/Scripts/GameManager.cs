using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isRunStarted = true, isGamePaused;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
