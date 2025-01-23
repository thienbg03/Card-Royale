using BreadcrumbAi;
using UnityEngine;
using UnityEngine.UI;

public class Knight : Summon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Ai ai;
    private Animator animator;
    public Slider healthSlider;
    public Collider swordCollision;
    private Vector3 orignalPosition;
    void Start()
    {
        ai = GetComponent<Ai>();
        animator = GetComponent<Animator>();
        orignalPosition = transform.position;
    }


    public override void Update()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.CombatPhase)
        {
            transform.position = orignalPosition;
        }
    }

    void FixedUpdate()
    {
        if (ai.attackState == Ai.ATTACK_STATE.CanAttackPlayer)
        {
            animator.SetBool("IsAttacking", true);
        }
        if (ai.moveState == Ai.MOVEMENT_STATE.IsFollowingPlayer)
        {
            //Animation
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsAttacking", false);
        }

        if (ai.lifeState == Ai.LIFE_STATE.IsDead)
        {
            Die();
        }
    }

    public override void TakeDamage(float damage)
    {
        ai.Health -= damage;
    }

    public override void Die()
    {
        Card.Die();
    }

    public void EnableSwordCollision()
    {
        swordCollision.enabled = true;
    }

    public void DisableSwordCollision() { 
        swordCollision.enabled = false;
    }

}