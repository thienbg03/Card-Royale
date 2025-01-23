using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : Summon
{
    private Ai ai;
    private Animator animator;
    public Slider healthSlider;
    public List<Rock> rocks;
    public List<Transform> rockPosition;
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

    public void CastSpell()
    {
        print("SPAWNING");
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        for (int i = 0; i < rocks.Count; i++) {
            if (!rocks[i].gameObject.activeSelf)
            {
                rocks[i].gameObject.SetActive(true);
            }
            else
            {
                rocks[i].originalPosition = rockPosition[i].position;
                rocks[i].ResetPosition();
            }
            print("ROCK SPAWN");
            // Throw the rock at the target
            rocks[i].ThrowRock(ai.Player.transform);
            yield return new WaitForSeconds(0.4f);
        }

    }

}
