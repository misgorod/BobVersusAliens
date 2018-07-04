using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;
using RotaryHeart.Lib.SerializableDictionary;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private WeaponImageAmmoDictionary weaponInfo;

    private Image currentWeaponImage;
    private bool isReloading;
    private float time;

    [SerializeField]
    private Color32 activeWeaponColor;
    [SerializeField]
    private Color32 inActiveWeaponColor;

    private void Awake()
    {
        Assert.IsTrue(weaponInfo.Count == 3);

        currentWeaponImage = weaponInfo[WeaponType.FirstSlot].Image;

        weaponInfo[WeaponType.FirstSlot].Image.color = activeWeaponColor;
        weaponInfo[WeaponType.SecondSlot].Image.color = inActiveWeaponColor;
        weaponInfo[WeaponType.ThirdSlot].Image.color = inActiveWeaponColor;
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
        if (currentWeaponImage.fillAmount < 1)
        {
            currentWeaponImage.fillAmount += Time.deltaTime / time;
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
        weaponInfo[weapon].Image.fillAmount = 0;
    }

    public void StopReload(WeaponType weapon)
    {
        isReloading = false;
        weaponInfo[weapon].Image.fillAmount = 1;
    }

    public void ChangeWeaponImages(WeaponType weapon)
    {
        foreach (var info in weaponInfo.Values)
        {
            info.Image.color = inActiveWeaponColor;
            info.Image.fillAmount = 1;
        }
        weaponInfo[weapon].Image.color = activeWeaponColor;

        StopReload(weapon);

        currentWeaponImage = weaponInfo[weapon].Image;
    }

    public void ChangeAmmo(WeaponType weapon, int ammo)
    {
        weaponInfo[weapon].Text.text = ammo.ToString();
    }

    [System.Serializable]
    public class ImageAmmo
    {
        public Image Image;
        public TextMeshProUGUI Text;
    }

    [System.Serializable]
    public class WeaponImageAmmoDictionary : SerializableDictionaryBase<WeaponType, ImageAmmo>
    {

    }
}




