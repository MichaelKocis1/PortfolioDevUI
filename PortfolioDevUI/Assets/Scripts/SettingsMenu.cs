using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject brightnessOverlay;
    public Material floor;
    public Material player;

    bool isOriginalMaterials;

    public void Start() {
        settingsMenu.SetActive(false);
        floor.color = Color.cyan;
        player.color = Color.red;

        brightnessOverlay.SetActive(false);
    }

    public void Return() {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void BrightnessToggle() {
        if (brightnessOverlay.activeSelf) {
            brightnessOverlay.SetActive(false);
        } else {
            brightnessOverlay.SetActive(true);
        }
    }

    public void ChangeMaterials() {
        if (!isOriginalMaterials) {
            floor.color = Color.green;
            player.color = Color.magenta;
            isOriginalMaterials = true;
        } else {
            floor.color = Color.cyan;
            player.color = Color.red;
            isOriginalMaterials = false;
        }
    }

    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void QuitApp() {
        Application.Quit();
    }
}
