using System.Collections.Generic;
using UnityEngine;

public enum Weapons {Wand, Sword, Bow, Spear}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] canvasesArray;
    
    public bool isRunStarted = false, isGamePaused = true;
    public Weapons currentWeapon;
    public GameObject[] weapons;


    [HideInInspector]public List<GameObject> chestsArray = new List<GameObject>();


    private void Awake()
    {
        instance ??= this;
        canvasesArray[0].SetActive(true);
        canvasesArray[1].SetActive(false);
        canvasesArray[3].SetActive(false);
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

    public void UpgradesScreen()
    {
        canvasesArray[3].SetActive(!canvasesArray[3].activeInHierarchy);
    }
}
