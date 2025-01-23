using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    private bool hasCard = false;
    public string HolderType;
    private Image image;
    public int ID;
    private Color defaultColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        defaultColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHasCard(bool hasCard)
    {
       this.hasCard = hasCard;
        if (hasCard) {
            image.color = Color.green;
        }
      
    }

    public bool GetHasCard() {  
        return hasCard; 
    }

    public void ResetHolder()
    {
        image.color = defaultColor;
        hasCard = false;
    }
}
