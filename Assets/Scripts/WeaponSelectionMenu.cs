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

    [SerializeField] private GameObject weaponParent;
    [SerializeField] private GameObject firstPersonPlayer;
    [SerializeField] private GameObject mainCanvas;

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

    private void openWeaponSelectionMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        buyMenu.enabled = true;
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = false;
        mainCanvas.GetComponent<Canvas>().enabled = false;
    }

    private void closeWeaponSelectionMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        buyMenu.enabled = false;
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = true;
        mainCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void confirm()
    {
        // Spawn the weapon
        GameObject weapon = Instantiate(selectedWeapon.modelPrefab, weaponParent.transform.position, Quaternion.identity);
        weapon.transform.parent = weaponParent.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;        

        // Set the muzzle flash spawn
        Debug.Log(GameObject.Find("Muzzle Flash Spawn"));
        firstPersonPlayer.GetComponent<PlayerShoot>().muzzleFlashSpawn = GameObject.Find("Muzzle Flash Spawn");

        // Enable shooting
        firstPersonPlayer.GetComponent<PlayerShoot>().canShoot = true;

        // Set the stats
        firstPersonPlayer.GetComponent<PlayerShoot>().fireRate = selectedWeapon.fireRate;
        firstPersonPlayer.GetComponent<PlayerShoot>().isAutomatic = selectedWeapon.isAutomatic;

        // Set the audio
        firstPersonPlayer.GetComponent<PlayerShoot>().shotSound = selectedWeapon.shotSound;

        // Close the buy menu
        closeWeaponSelectionMenu();
    }
}
