using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausemenu : MonoBehaviour
{
    private bool IsPaused = false;
    public GameObject Menu;

    // Input Actions
    private  PlayerInput _inputAction = null;

    // Start is called before the first frame update
    private void Awake()
    {
        _inputAction = new PlayerInput();
        _inputAction.PlayerControls.PauseIt.performed += ctx => PauseGame();
    }

    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();
    
    public void PauseGame()
    {
        IsPaused = !IsPaused;
        Menu.SetActive(IsPaused);
        Time.timeScale = IsPaused ? 0 : 1;
    }

    public void QuitGame() => Application.Quit();
}
