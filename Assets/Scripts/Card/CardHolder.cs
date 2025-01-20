using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private bool hasCard = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHasCard(bool hasCard)
    {
       this.hasCard = hasCard;
    }

    public bool GetHasCard() {  
        return hasCard; 
    }
}
