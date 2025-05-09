using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    [Header("Pool Object Settings")]
    public string poolTag;
    public bool isActive = false;
    public float lifeTime = 0f;

    private float currentLifeTime;

    public virtual void OnSpawn()
    {
        isActive = true;
        gameObject.SetActive(true);

        if (lifeTime > 0)
        {
            currentLifeTime = lifeTime;
            Invoke("OnDespawn", lifeTime);
        }
    }

    public virtual void OnDespawn()
    {
        isActive = false;
        gameObject.SetActive(false);

        if (lifeTime > 0)
        {
            CancelInvoke("OnDespawn");
        }
    }

    protected virtual void Update()
    {
        if (isActive && lifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
            if (currentLifeTime <= 0)
            {
                OnDespawn();
            }
        }
    }
}