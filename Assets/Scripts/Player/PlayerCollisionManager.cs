using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public BoxCollider SwordCollision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSwordCollision()
    {
        if (PlayerManager.Instance.PlayerState != PlayerState.Sword)
            return;

        SwordCollision.enabled = true;
    }

    public void DisableSwordCollision()
    {
        if (PlayerManager.Instance.PlayerState != PlayerState.Sword)
            return;
        SwordCollision.enabled = false;
    }

}
