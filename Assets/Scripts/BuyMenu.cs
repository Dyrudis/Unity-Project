using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    [SerializeField] private Canvas buyMenu;

    // Update is called once per frame
    void Update()
    {
        // Toggle the buy menu when pressing B
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (buyMenu.enabled)
            {
                closeBuyMenu();
            }
            else
            {
                openBuyMenu();
            }
        }
    }

    private void openBuyMenu() {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        buyMenu.enabled = true;
    }

    private void closeBuyMenu() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        buyMenu.enabled = false;
    }
}
