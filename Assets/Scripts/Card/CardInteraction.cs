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
    void Start()
    {
        originalParent = this.transform.parent;
        startPosition = this.transform.parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDrag()
    {
        Debug.Log("Dragging");
        if (isDragging && isDraggable)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        
        isDragging = true;
        print("startdrag");
        this.transform.SetParent(this.transform.parent.transform.parent, true);
    }

    public void EndDrag()
    {
        if (isPlacing)
        {
            transform.position = placingPosition;
            this.transform.SetParent(holder.transform, true);
            holder.SetHasCard(true);
            isDraggable = false;
        }
        else
        {
            this.transform.SetParent(originalParent, true);
            transform.position = startPosition;
        }
        isDragging = false;
        print("enddrag");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CardHolder")
        {
            print("ON HOLDER");
            holder = other.gameObject.GetComponent<CardHolder>();
            if (!holder.GetHasCard())
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
            if (holder.GetHasCard())
            {
                other.gameObject.GetComponent<Image>().color = Color.green;
            }
            else
            {
                other.gameObject.GetComponent<Image>().color = Color.red;
            }
           
            isPlacing = false;
        }
       
    }

}
