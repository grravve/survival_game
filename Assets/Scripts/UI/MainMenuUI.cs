using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private GameObject _settingsWindow;

    private void Awake()
    {
        _settingsWindow = transform.Find("Settings_Window").gameObject;
    }

    private void Start()
    {
        _settingsWindow.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Settings()
    {
        _settingsWindow.SetActive(!_settingsWindow.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
