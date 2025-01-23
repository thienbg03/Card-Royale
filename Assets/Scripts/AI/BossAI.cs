using BreadcrumbAi;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : Enemy
{

    private Ai ai;
    private Animator animator;
    private float attackIndex;
    public GameObject leftHandCollision;
    public GameObject rightHandCollision;
    public GameObject insideCollision;
    public Slider healthSlider;
    private Vector3 orignalPosition;
    public GameObject burnArea;
    public GameObject fireFX;
    void Start()
    {
        ai = GetComponent<Ai>();
        animator = GetComponent<Animator>();
        attackIndex = 0;
        orignalPosition = transform.position;
    }

    void Update()
    {
        if(GameManager.Instance.CurrentPhase != GamePhase.CombatPhase)
        {
            transform.position = orignalPosition;
        }
    }

    void FixedUpdate()
    {

        if (ai.attackState == Ai.ATTACK_STATE.CanAttackPlayer)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("AttackIndex", attackIndex);
        }
        if (ai.moveState == Ai.MOVEMENT_STATE.IsFollowingPlayer)
        {
            //Animation
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsAttacking", false);
        }
       
        if(ai.lifeState == Ai.LIFE_STATE.IsDead)
        {
            Die();
        }
    }
    public void EnableWeaponCollision()
    {
        leftHandCollision.SetActive(true);
        rightHandCollision.SetActive(true);
        insideCollision.SetActive(true);
    }

    public void DisableWeaponCollision()
    {
        leftHandCollision.SetActive(false);
        rightHandCollision.SetActive(false);
        insideCollision.SetActive(false);
    }



    public void StopAction()
    {
        ai._IsPerformingAction = false;
    }

    public void StartAction()
    {
        ai._IsPerformingAction = true;
    }

    public void SetAttackIndex(float index)
    {
        attackIndex = index;
    }

    public override void TakeDamage(float damage)
    {
        ai.Health -= damage;
        healthSlider.value = ai.Health;
    }

    public override void Die()
    {
        //DEBUG VICTORY
        Destroy(this.gameObject, 0.5f);
        GameManager.Instance.WinGame();
    }

    public override void TakeBurnDamage(int ticks, float damage)
    {
        StartCoroutine(BurnHandler(ticks, damage));
    }

    private IEnumerator BurnHandler(int ticks, float damage)
    {
        isBurning = true;
        for (int i = 0; i < ticks; i++)
        {
            yield return new WaitForSeconds(1f);
            SpawnfireEffects();
            TakeDamage(damage);
            print("BURN");
        }
        isBurning = false;
    }
    private void SpawnfireEffects()
    {

        Vector3 randomPoint = Random.insideUnitSphere;
        randomPoint *= burnArea.GetComponent<SphereCollider>().radius * burnArea.GetComponent<SphereCollider>().transform.localScale.x;

        GameObject go = Instantiate(fireFX, burnArea.transform);
        go.transform.position = randomPoint;
        Destroy(go, 2f);
    }
}
