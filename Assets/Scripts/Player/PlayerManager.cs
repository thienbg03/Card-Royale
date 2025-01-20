using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerCardManager CardManager;
    public PlayerInputTPC InputTPC;
    public GameManager GameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
