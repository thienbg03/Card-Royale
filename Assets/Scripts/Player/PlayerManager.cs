using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject player;
    public PlayerCardManager CardManager;
    public PlayerInputTPC InputTPC;
    public GameManager GameManager;
    public PlayerUIManager PlayerUIManager;
    public Animator animator;
    public PlayerAnimationManager animationManager;
    public PlayerCollisionManager playerCollisionManager;
    public CinemachineCamera playerDefaultCamera;
    public CinemachineCamera playerAimCamera;


    public bool IsPerformingAction;
    public bool CanMove;
    public bool CanRotate;
    public bool CanDoCombo;
    public bool ApplyRootMotion;
    public float stamina;
    public float health;
    public bool CanAddStamina;
    public float meeleDamage;
    public PlayerState PlayerState;
    public Transform SwordSocket;
    public Transform BowSocket;
    public GameObject Weapon;
    public EquipementCard EquipementCard;
    private Vector3 ogPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        meeleDamage = 100;
        stamina = 100;
        health = 2000;
        CanAddStamina = true;
        CanDoCombo = false;
        ogPosition = player.transform.position;
    }

    // Update is called once per fram
    void Update()
    {
        if(GameManager.CurrentPhase != GamePhase.CombatPhase)
        {
            InputTPC.enabled = false;
            player.transform.position = ogPosition;
        }
        else
        {
            InputTPC.enabled = true;
        }
    }

   public void TakeDamage(float damage)
    {
        health -= damage;
        PlayerUIManager.SetHealth(health);
        if(health <= 0)
        {
            Die();
        }
        
    }

    public void EquipWeapon(GameObject weapon = null)
    {
        if(PlayerState == PlayerState.Sword)
        {
            //Equip Sword
            Weapon = Instantiate(weapon, SwordSocket, false);
            playerCollisionManager.SwordCollision = Weapon.GetComponent<BoxCollider>();
            animationManager.UpdateAnimationParamter("IsSword", true);
            animationManager.UpdateAnimationParamter("IsCombat", true);
            animationManager.UpdateAnimationParamter("IsBow", false);
        }
        else if(PlayerState == PlayerState.Bow)
        {
            Weapon = Instantiate(weapon, BowSocket, false);
            //Equip Bow
            animationManager.UpdateAnimationParamter("IsSword", false);
            animationManager.UpdateAnimationParamter("IsCombat", true);
            animationManager.UpdateAnimationParamter("IsBow", true);
        }
        else
        {
            //Prevent Combat
            print("NO EQUIPMENT");
            animationManager.UpdateAnimationParamter("IsSword", false);
            animationManager.UpdateAnimationParamter("IsCombat", false);
            animationManager.UpdateAnimationParamter("IsBow", false);
        }
    }

    public void Die()
    {
        print("PLAYER DEAD");
        GameManager.Instance.LoseGame();
    }
    
    public void SwitchToAimingCamera()
    {
        playerDefaultCamera.Priority = 0;
        playerAimCamera.Priority = 10;
    }

    public void SwitchToDefaultCamera()
    {
        playerDefaultCamera.Priority = 10;
        playerAimCamera.Priority = 0;
    }

    public void ResetEquipment()
    {
        this.EquipementCard.DestroyEquipmentCard();
        Destroy(Weapon, 0.5f);
    }
}

public enum PlayerState{
    Sword,
    Bow,
    None
}
