using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float WeaponDamage;
    public bool canDamagePlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (canDamagePlayer)
            {
                //This means it hit the actual player
                if (other.gameObject.GetComponentInChildren<PlayerManager>() != null)
                {
                    PlayerManager.Instance.TakeDamage(WeaponDamage);
                }
                else if (other.gameObject.GetComponent<Summon>() != null)
                {
                    other.gameObject.GetComponent<Summon>().TakeDamage(WeaponDamage);
                    print("HITTING:  " + other.gameObject.name);
                }
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (!canDamagePlayer)
            {
                //This means it hit the actual player
                if (other.gameObject.GetComponent<Enemy>() != null)
                {
                    print("HITTING:  " + other.gameObject.name);
                    other.gameObject.GetComponent<Enemy>().TakeDamage(WeaponDamage);
                }
            }
        }
    }
}
