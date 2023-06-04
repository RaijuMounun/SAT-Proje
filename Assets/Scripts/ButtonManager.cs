using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public void StartGame() { SceneManager.LoadScene("Game"); }

    public void Settings() { settingsPanel.SetActive(!settingsPanel.activeInHierarchy); }

    public void QuitGame() { Application.Quit(); }
}
