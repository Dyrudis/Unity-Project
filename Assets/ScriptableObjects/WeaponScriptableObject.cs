using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float fireRate;
    public bool isAutomatic;
    public GameObject modelPrefab;
    public AudioClip shotSound;
    public Sprite icon;
}
