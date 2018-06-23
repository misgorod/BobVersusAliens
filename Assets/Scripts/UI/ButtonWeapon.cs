using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ButtonWeapon : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private Weapons weapon;
    [Inject]
    private ChangeWeaponSignal signal;

    public void OnPointerClick(PointerEventData data)
    {
        signal.Fire(weapon);
    }
}
