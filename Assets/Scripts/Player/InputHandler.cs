using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerClickHandler
{

    PlayerShootSignal onPlayerShoot;

    [Inject]
    public void Construct(PlayerShootSignal onPlayerShoot)
    {
        this.onPlayerShoot = onPlayerShoot;
    }

    public void OnPointerClick(PointerEventData data)
    {
        onPlayerShoot.Fire(data.pressPosition);
    }

}
