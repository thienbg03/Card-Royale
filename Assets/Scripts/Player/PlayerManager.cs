using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerCardManager CardManager;
    public PlayerInputTPC InputTPC;
    public GameManager GameManager;
    public Animator animator;
    public PlayerAnimationManager animationManager;
    public bool IsPerformingAction;
    public bool CanMove;
    public bool CanRotate;
    public bool ApplyRootMotion;

    public float stamina;
    public bool CanAddStamina;
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
        stamina = 100;
        CanAddStamina = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.CurrentPhase != GamePhase.CombatPhase)
        {
            InputTPC.enabled = false;
        }
        else
        {
            InputTPC.enabled = true;
        }
    }

    
}
