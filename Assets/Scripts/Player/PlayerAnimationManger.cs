using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimatorPlayerSpeed(float speed)
    {
        PlayerManager.Instance.animator.SetFloat("Speed", speed);
    }

    public void PerformAnimationAction(string targetAnimation, bool isPerformingAction, bool applyRootMotion = false, bool canMove = false, bool canRotate = false)
    {
        PlayerManager.Instance.animator.applyRootMotion = applyRootMotion;
        PlayerManager.Instance.animator.CrossFade(targetAnimation, 0.2f);
        PlayerManager.Instance.IsPerformingAction = isPerformingAction;
        PlayerManager.Instance.CanRotate = canMove;
        PlayerManager.Instance.CanMove = canRotate;
    }
}
