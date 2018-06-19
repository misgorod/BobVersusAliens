using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputHandler : ITickable
{

    PlayerShootSignal onPlayerShoot;

    InputHandler(PlayerShootSignal onPlayerShoot)
    {
        this.onPlayerShoot = onPlayerShoot;
    }

	public void Tick ()
    {
		if (Input.GetMouseButton(0))
        {
            onPlayerShoot.Fire(Input.mousePosition);
        }
	}
}
