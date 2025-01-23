using BreadcrumbAi;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public float weaponDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
                print("HIT ENEMY: " + other.gameObject.GetComponent<Enemy>().GetComponent<Ai>().Health);
            }
           
        }
    }
}
