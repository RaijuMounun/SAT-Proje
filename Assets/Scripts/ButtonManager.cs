using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [CanBeNull] public GameObject settingsPanel;
    [HideInInspector] public GameObject player;
    StatController _playerStatCon;
    WandController _wandController;
    SwordParentCont _swordController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerStatCon = player.GetComponent<StatController>();
        _wandController = GameObject.Find("Wand").GetComponent<WandController>();
        _swordController = GameObject.Find("SwordParent").GetComponent<SwordParentCont>();
    }

    public void StartGame() { SceneManager.LoadScene("Game"); }

    public void Settings() { settingsPanel.SetActive(!settingsPanel.activeInHierarchy); }

    public void QuitGame() { Application.Quit(); }


    public void Upgrades(int index)
    {
        switch (index)
        {
            case 0:
                _playerStatCon.health += _playerStatCon.maxHealth / 2;
                _playerStatCon.maxHealth += _playerStatCon.maxHealth / 2;
                break;
            case 1:
                _wandController.spellDamageRange.x += _wandController.spellDamageRange.x * .3f;
                _wandController.spellDamageRange.y += _wandController.spellDamageRange.y * .3f;
                _swordController.damageRange.x += _swordController.damageRange.x * .3f;
                _swordController.damageRange.y += _swordController.damageRange.y * .3f;
                break;
            case 2:
                _playerStatCon.moveSpeed += _playerStatCon.moveSpeed * .25f;
                break;
            case 3:
                GameManager.instance.PickWeapon(0);
                break;
        }
        GameManager.instance.UpgradesScreen();
    }
}
