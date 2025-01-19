using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GamePhase CurrentPhase;

    public GameObject CardArena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private IEnumerator coroutine;
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
        //On Start, add 3 cards to both player and enemy deck
        coroutine = DrawCard();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn()
    {
        //Draw Card
        StartCoroutine(coroutine);
    }

    private IEnumerator DrawCard()
    {
        CardArena.SetActive(true);
        CurrentPhase = GamePhase.DrawPhase;
        //Reference to Player and Enemy Card Manager, add a card to hand
        Debug.Log(CurrentPhase);
        yield return new WaitForSeconds(3f); // Simulate delay

        coroutine = PlayCard();
        StartTurn();
    }

    private IEnumerator PlayCard()
    {
        CurrentPhase = GamePhase.PlayPhase;
        Debug.Log(CurrentPhase);
        //Reference to Player and Enemy Card Manager, add a card to hand
        //Add Logic to Handle Player playing card
        yield return new WaitForSeconds(5f); // Simulate delay
        coroutine = Combat();
        StartTurn();

        //Exit Card Arena
        CardArena.SetActive(false);
    }

    private IEnumerator Combat()
    {
        //Add Logic to Handle Player playing card
        CurrentPhase = GamePhase.CombatPhase;
        Debug.Log(CurrentPhase);
        yield return new WaitForSeconds(7f); // Simulate delay
        Debug.Log("Done Turn");
        coroutine = DrawCard();
        StartTurn();
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
