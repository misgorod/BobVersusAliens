using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ButtonWeapon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private WeaponType weapon;
    [Inject]
    private ChangeWeaponSignal onChangeWeapon;

    public void OnPointerClick(PointerEventData data)
    {
        onChangeWeapon.Fire(weapon);
    }

}
