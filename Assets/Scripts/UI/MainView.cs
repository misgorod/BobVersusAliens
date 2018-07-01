using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private List<Image> weaponList;

    private Dictionary<WeaponType, Image> weaponImages;

    private Image currentWeapon;
    private bool isReloading;
    private float time;

    [SerializeField]
    private Color32 activeWeaponColor;
    [SerializeField]
    private Color32 inActiveWeaponColor;

    private void Start()
    {
        Assert.IsTrue(weaponList.Count == 3);

        weaponImages = new Dictionary<WeaponType, Image>();

        weaponImages.Add(WeaponType.FirstSlot, weaponList[0]);
        weaponImages.Add(WeaponType.SecondSlot, weaponList[1]);
        weaponImages.Add(WeaponType.ThirdSlot, weaponList[2]);

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

}
