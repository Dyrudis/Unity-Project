using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSelectionMenu : MonoBehaviour
{
    [SerializeField] private Canvas buyMenu;
    [SerializeField] private WeaponScriptableObject weapon1;
    [SerializeField] private WeaponScriptableObject weapon2;
    [SerializeField] private WeaponScriptableObject weapon3;
    [SerializeField] private GameObject weapon1Panel;
    [SerializeField] private GameObject weapon2Panel;
    [SerializeField] private GameObject weapon3Panel;

    [SerializeField] private GameObject firstPersonPlayer;

    private WeaponScriptableObject selectedWeapon;

    void Start()
    {
        // Set the name for each weapon
        weapon1Panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = weapon1.weaponName;
        weapon2Panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = weapon2.weaponName;
        weapon3Panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = weapon3.weaponName;

        // Set the icon for each weapon
        weapon1Panel.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = weapon1.icon;
        weapon2Panel.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = weapon2.icon;
        weapon3Panel.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = weapon3.icon;

        // Onclick events for each weapon
        weapon1Panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => selectedWeapon = weapon1);
        weapon2Panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => selectedWeapon = weapon2);
        weapon3Panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => selectedWeapon = weapon3);

        openWeaponSelectionMenu();
    }

    void Update()
    {
        
    }

    private void openWeaponSelectionMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        buyMenu.enabled = true;
    }

    private void closeWeaponSelectionMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        buyMenu.enabled = false;
    }

    public void confirm()
    {
        // Log the weapon name
        Debug.Log(selectedWeapon.weaponName);

        // Close the buy menu
        closeWeaponSelectionMenu();

        // Enable shooting
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = true;
    }
}
