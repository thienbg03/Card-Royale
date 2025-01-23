using UnityEngine;

public class Rock : MonoBehaviour
{
    public Vector3 originalPosition;
    private bool isHit = false;
    private void Awake()
    {
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        isHit = false;
    }

    public void ThrowRock(Transform target)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            Vector3 directionToTarget = target.position - transform.position;
            float throwForce = 25f;
            rb.AddForce(directionToTarget.normalized * throwForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy>() && !isHit)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(50);
                isHit = true;
            }
        }
        else
        {
            print("COLLIDED WITH SOMETHINh: " + collision.gameObject.name);
            isHit = true;
        }
        
        
    }
}
