using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class BulletHandler : MonoBehaviour
{
    private float lifeTime;
    private float speed;
    

    private float currentTimeAlive;

    
    Pool bulletPool;

    [Inject]
    public void Construct(Pool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

	private void Start ()
    {
        currentTimeAlive = 0;
	}
	
	private void Update ()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        currentTimeAlive += Time.deltaTime;
        
        if (currentTimeAlive > lifeTime)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        bulletPool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<float, float, float, BulletHandler>
    {

        protected override void Reinitialize(float damage, float speed, float lifeTime, BulletHandler bullet)
        {
            bullet.lifeTime = lifeTime;
            bullet.speed = speed;
           
            bullet.currentTimeAlive = 0;
        }

    }
}
