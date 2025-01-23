using BreadcrumbAi;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float weaponDamage;
    private bool HasHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && HasHit == false)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
            print("HIT ENEMY: " + collision.gameObject.GetComponent<Enemy>().GetComponent<Ai>().Health);
            Destroy(this.gameObject, 0.5f);
            HasHit = true;
        }
        Destroy(this.gameObject, 0.5f);
    }
}
