using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private List<Image> weaponList;

    private Dictionary<Weapons, Image> weaponImages;

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

        weaponImages = new Dictionary<Weapons, Image>();

        weaponImages.Add(Weapons.FirstSlot, weaponList[0]);
        weaponImages.Add(Weapons.SecondSlot, weaponList[1]);
        weaponImages.Add(Weapons.ThirdSlot, weaponList[2]);

        currentWeapon = weaponImages[Weapons.FirstSlot];

        weaponImages[Weapons.FirstSlot].color = activeWeaponColor;
        weaponImages[Weapons.SecondSlot].color = inActiveWeaponColor;
        weaponImages[Weapons.ThirdSlot].color = inActiveWeaponColor;
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

    public void StartReload(Weapons weapon, float time)
    {
            isReloading = true;
            this.time = time;
            weaponImages[weapon].fillAmount = 0;
    }

    public void StopReload(Weapons weapon)
    {
            isReloading = false;
            weaponImages[weapon].fillAmount = 1;
    }

    public void ChangeWeaponImages(Weapons weapon)
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
