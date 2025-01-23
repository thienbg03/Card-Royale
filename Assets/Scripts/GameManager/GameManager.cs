using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
   
    private float turnTimer = 30f;
    private float maxTimer = 30f;
    private float combatTimer = 30f;
    private float combatMaxTimer = 30f;
    private IEnumerator coroutine;
    private bool isPause;

    public static GameManager Instance;
    public GamePhase CurrentPhase;
    public GameObject CardArena;
    public GameObject CombatArenaHUD;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI combatTimerText;
    public GameObject skipButton;
    public PlayerInput PlayerInput;
    public GameObject CombatObjects;
    public CardHolder equipementHolder;
    public GameObject PauseMenu;
    public GameObject cardInfoOverlay;
    public AudioClip CardArenaMusic;
    public AudioClip CombatArenaMusic;
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        //On Start, add 3 cards to both player and enemy deck
        coroutine = DrawCard();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPhase == GamePhase.PlayPhase)
        {
            turnTimer -= Time.deltaTime;
            countDownText.text = ((int)turnTimer).ToString();
        }
        else if (CurrentPhase == GamePhase.CombatPhase) {
            combatTimer -= Time.deltaTime;
            combatTimerText.text = ((int)combatTimer).ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void StartTurn()
    {
        //Draw Card
        StartCoroutine(coroutine);
        
    }

    private IEnumerator DrawCard()
    {
        audioSource.clip = CardArenaMusic;
        audioSource.Play();
        //Reset to Draw Phase
        Cursor.lockState = CursorLockMode.None;
        skipButton.SetActive(false);
        CombatArenaHUD.SetActive(false);
        CardArena.SetActive(true);
        PlayerInput.enabled = false;
        CurrentPhase = GamePhase.DrawPhase;
        countDownText.text = "Draw";
        yield return new WaitForSeconds(5f);
        coroutine = PlayCard();
        StartTurn();
    }

    private IEnumerator PlayCard()
    {
        CurrentPhase = GamePhase.PlayPhase;
        skipButton.SetActive(true);
        yield return new WaitForSeconds(turnTimer);
        turnTimer = maxTimer;
        coroutine = Combat();
        StartTurn();
        //Exit Card Arena
        CardArena.SetActive(false);
    }

    private IEnumerator Combat()
    {
        //Add Logic to Handle Player playing card
        CombatSetup();
        yield return new WaitForSeconds(combatTimer); // Simulate delay
        PostCombat();
    }

    public void SkipTurn()
    {
        StopCoroutine(coroutine);
        turnTimer = maxTimer;
        coroutine = Combat();
        //Exit Card Arena
        CardArena.SetActive(false);
        StartTurn();
    }

    private void CombatSetup()
    {
        if (!equipementHolder.GetHasCard())
        {
            PlayerManager.Instance.PlayerState = PlayerState.None;
            PlayerManager.Instance.EquipWeapon();
        }
        PlayerManager.Instance.CardManager.ResetDeck();
        CombatObjects.SetActive(true);
        PlayerInput.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CombatArenaHUD.SetActive(true);
        CurrentPhase = GamePhase.CombatPhase;
        audioSource.clip = CombatArenaMusic;
        audioSource.Play();
    }

    private void PostCombat()
    {
        if (equipementHolder.GetHasCard()) { 
            PlayerManager.Instance.ResetEquipment();
        }
        CombatObjects.SetActive(false);
        combatTimer = combatMaxTimer;
        coroutine = DrawCard();
        StartTurn();

    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinMenu");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("LoseMenu");
    }

    public void OnPause()
    {
        if (isPause)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            if(CurrentPhase == GamePhase.CombatPhase)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ShowCardInfo(Image cardImage)
    {
        cardInfoOverlay.GetComponent<Image>().sprite = cardImage.sprite;
        cardInfoOverlay.SetActive(true);
    }

    public void CloseCardInfo()
    {
        cardInfoOverlay.SetActive(false);
    }
}

public enum GamePhase
{
    DrawPhase,
    PlayPhase,
    CombatPhase,
    LosePhase,
    VictoryPhase
}
