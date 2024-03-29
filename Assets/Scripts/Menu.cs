using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject firstPersonPlayer;
    [SerializeField] private GameObject mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        resetButton.GetComponent<Button>().onClick.AddListener(resetGame);
        resumeButton.GetComponent<Button>().onClick.AddListener(resumeGame);
        quitButton.GetComponent<Button>().onClick.AddListener(quitGame);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the menu when pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.enabled)
            {
                closeMenu();
            }
            else
            {
                openMenu();
            }
        }
    }

    private void openMenu() {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        menu.enabled = true;
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = false;
        mainCanvas.GetComponent<Canvas>().enabled = false;
    }

    private void closeMenu() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        menu.enabled = false;
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = true;
        mainCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void resetGame() {
        closeMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void resumeGame() {
        closeMenu();
    }

    private void quitGame() {
        Application.Quit();
    }
}
