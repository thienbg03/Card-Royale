using BreadcrumbAi;
using System.Collections;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    public float weaponDamage;
    public float fireDamage;
    public int burnDuration;
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
                Enemy enemy = other.gameObject.GetComponents<Enemy>()[0];
                enemy.TakeDamage(weaponDamage);
                if (!enemy.isBurning && enemy.canBurn)
                {
                    if((int)Random.Range(0,100) > 10)
                    {
                        enemy.TakeBurnDamage(burnDuration, 50);
                    }
                }
            }
           
        }
    }

    

}
