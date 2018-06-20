using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private List<Image> weaponImages;

    [SerializeField]
    private Color32 activeWeaponColor;
    [SerializeField]
    private Color32 inActiveWeaponColor;

    private void Start()
    {
        weaponImages[0].color = activeWeaponColor;
        weaponImages[1].color = inActiveWeaponColor;
        weaponImages[2].color = inActiveWeaponColor;
    }

    public void ChangeWeaponImages(Weapons weapon)
    {
        foreach (var image in weaponImages)
        {
            image.color = inActiveWeaponColor;
        }
        weaponImages[(int)weapon].color = activeWeaponColor;
    }

}
