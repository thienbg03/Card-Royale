using UnityEngine;

public abstract class Summon : MonoBehaviour
{
    [SerializeField] private SummonCard card;


    public SummonCard Card { 
        get { return card; } 
        set { card = value; }
    }

    public abstract void TakeDamage(float damage);

    public abstract void Die();

    public abstract void Update();
}
