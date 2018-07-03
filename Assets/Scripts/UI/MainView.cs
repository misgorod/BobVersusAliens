using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private List<Image> weaponImagesList;

    [SerializeField]
    private List<TextMeshProUGUI> weaponAmmoList;

    private Dictionary<WeaponType, Image> weaponImages;
    private Dictionary<WeaponType, TextMeshProUGUI> weaponAmmo;

    private Image currentWeapon;
    private bool isReloading;
    private float time;

    [SerializeField]
    private Color32 activeWeaponColor;
    [SerializeField]
    private Color32 inActiveWeaponColor;

    private void Awake()
    {
        Assert.IsTrue(weaponImagesList.Count == 3);
        Assert.IsTrue(weaponAmmoList.Count == 3);

        weaponImages = new Dictionary<WeaponType, Image>();

        weaponImages.Add(WeaponType.FirstSlot, weaponImagesList[0]);
        weaponImages.Add(WeaponType.SecondSlot, weaponImagesList[1]);
        weaponImages.Add(WeaponType.ThirdSlot, weaponImagesList[2]);

        weaponAmmo = new Dictionary<WeaponType, TextMeshProUGUI>();

        weaponAmmo.Add(WeaponType.FirstSlot, weaponAmmoList[0]);
        weaponAmmo.Add(WeaponType.SecondSlot, weaponAmmoList[1]);
        weaponAmmo.Add(WeaponType.ThirdSlot, weaponAmmoList[2]);

        currentWeapon = weaponImages[WeaponType.FirstSlot];

        weaponImages[WeaponType.FirstSlot].color = activeWeaponColor;
        weaponImages[WeaponType.SecondSlot].color = inActiveWeaponColor;
        weaponImages[WeaponType.ThirdSlot].color = inActiveWeaponColor;
    }

    private void Update()
    {
        if (isReloading)
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (currentWeapon.fillAmount < 1)
        {
            currentWeapon.fillAmount += Time.deltaTime / time;
        }
        else
        {
            isReloading = false;
        }
    }

    public void StartReload(WeaponType weapon, float time)
    {
            isReloading = true;
            this.time = time;
            weaponImages[weapon].fillAmount = 0;
    }

    public void StopReload(WeaponType weapon)
    {
            isReloading = false;
            weaponImages[weapon].fillAmount = 1;
    }

    public void ChangeWeaponImages(WeaponType weapon)
    {
        foreach (var image in weaponImages)
        {
            image.Value.color = inActiveWeaponColor;
            image.Value.fillAmount = 1;
        }
        weaponImages[weapon].color = activeWeaponColor;

        StopReload(weapon);

        currentWeapon = weaponImages[weapon];
    }

    public void ChangeAmmo(WeaponType weapon, int ammo)
    {
        weaponAmmo[weapon].text = ammo.ToString();
    }

}
