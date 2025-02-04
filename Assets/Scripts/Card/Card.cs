using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private string description;
    [SerializeField] private int manaCost;
    [SerializeField] private string type;
    [SerializeField] private GameObject model;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string CardName
    {
        get { return cardName; }
        set { cardName = value; }
    }

    public int ManaCost
    {
        get { return manaCost; }
        set { manaCost = value; }
    }

    public GameObject Model
    {
        get { return model; }
        set { model = value; }
    }

    public abstract void PlayCard(Transform summon = null); 
    public abstract void ActivateEffect(); 

    public void DisplayCardInfo()
    {
        
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}
