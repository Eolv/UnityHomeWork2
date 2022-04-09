using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool _pauseGameOn = false;
    [SerializeField] private GameObject _gameGUI;
    [SerializeField] private GameObject _gameMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseGameOn = !_pauseGameOn;
            _gameGUI.SetActive(!_pauseGameOn);
            _gameMenu.SetActive(_pauseGameOn);
            Time.timeScale = _pauseGameOn ? 0 : 1;
        }
    }

    public void ResumeGame()
    {
        if (_pauseGameOn)
        {
            _pauseGameOn = false;
            _gameGUI.SetActive(true);
            _gameMenu.SetActive(false);
            Time.timeScale = 1;            
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}