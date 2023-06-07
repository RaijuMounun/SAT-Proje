using UnityEngine;

public enum Weapons {Wand, Sword, Bow, Spear}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] canvasesArray;
    
    public bool isRunStarted = false, isGamePaused = true;
    public Weapons currentWeapon;
    public GameObject[] weapons;


    private void Awake()
    {
        instance ??= this;
        canvasesArray[0].SetActive(true);
    }
    
    public void StartRun()
    {
        isRunStarted = true;
        isGamePaused = false;
        canvasesArray[0].SetActive(false);
    }
    
    public void PickWeapon(int index)
    {
        currentWeapon = (Weapons) index;
        foreach (var weapon in weapons) weapon.SetActive(false);
        weapons[(int) currentWeapon].SetActive(true);
    }
}
