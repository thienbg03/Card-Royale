using UnityEngine;
using UnityEngine.UI;

public class CardInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 startPosition;
    private Vector2 placingPosition;
    private Transform originalParent;
    private bool isDragging = false;
    private bool isPlacing = false;
    private bool isDraggable = true;
    private CardHolder holder;
    private Card cardInfo;
    private PlayerCardManager CardManager;
    private Image cardImage;
    void Start()
    {
        originalParent = this.transform.parent;
        startPosition = this.transform.parent.transform.position;
        cardInfo = GetComponent<Card>();
        CardManager = GameObject.FindWithTag("Player").GetComponent<PlayerCardManager>();
        cardImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDrag()
    {
        if (isDragging && isDraggable)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        isDragging = true;
        this.transform.SetParent(this.transform.parent.transform.parent, true);
    }

    public void EndDrag()
    {
        if (isPlacing)
        {
           PlaceCard();
        }
        else
        {
            this.transform.SetParent(originalParent, true);
        }
        isDragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CardHolder")
        {
            holder = other.gameObject.GetComponent<CardHolder>();
            if (!holder.GetHasCard() && cardInfo.Type == holder.HolderType)
            {
                placingPosition = other.transform.position;
                other.gameObject.GetComponent<Image>().color = Color.green;
                isPlacing = true;
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "CardHolder")
        {
            if (cardInfo.Type == holder.HolderType && !holder.GetHasCard())
            {
                other.gameObject.GetComponent<Image>().color = Color.red;
                isPlacing = false;
            }
        }
    }

    private void PlaceCard()
    {
        transform.position = placingPosition;
        this.transform.SetParent(holder.transform, true);
        holder.SetHasCard(true);
        isDraggable = false;
        CardManager.NumCardsOnhand = CardManager.NumCardsOnhand - 1;
        if(cardInfo.Type == "summon")
        {
            foreach (CombatPlacement cp in CardManager.CombatPlacement)
            {
                if(cp.ID == holder.ID)
                {
                    cardInfo.PlayCard(cp.SpawnPosition.transform);
                    break;
                }
            }
            
        }
        else
        {
            cardInfo.PlayCard();
        }
        
    }

    public void OnCardEnter()
    {
        if (!isDraggable || isDragging) // Prevent if not draggable or currently dragging
            return;

        transform.position += new Vector3(0, 10);
    }

    public void OnCardExit()
    {
        if (!isDraggable || isDragging) // Prevent if not draggable or currently dragging
            return;

        transform.position -= new Vector3(0, 10);
    }

    public void OnCardClick()
    {
        if (isDragging) // Prevent if currently dragging
            return;

        GameManager.Instance.ShowCardInfo(cardImage);
    }

}
