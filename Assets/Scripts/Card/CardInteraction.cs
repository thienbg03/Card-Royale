using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.WSA;

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
    void Start()
    {
        originalParent = this.transform.parent;
        startPosition = this.transform.parent.transform.position;
        cardInfo = GetComponent<Card>();
        CardManager = GameObject.FindWithTag("Player").GetComponent<PlayerCardManager>();
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
            transform.position = startPosition;
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
    }

}
