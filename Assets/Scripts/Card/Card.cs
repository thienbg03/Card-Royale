using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private string description;
    [SerializeField] private int manaCost;
    [SerializeField] private string type;
    public abstract void PlayCard(); 
    public abstract void ActivateEffect(); 

    public void DisplayCardInfo()
    {
        
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}
