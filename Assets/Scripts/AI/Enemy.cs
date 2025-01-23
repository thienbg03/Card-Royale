using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool canBurn;
    public bool isBurning;
    public abstract void TakeDamage(float damage);

    public abstract void Die();

    public abstract void TakeBurnDamage(int ticks, float damage);

}
