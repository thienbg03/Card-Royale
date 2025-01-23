using UnityEngine;

public class SummonCard : Card
{
    public bool isSummoned = false;
    private Vector3 originalPosition;
    private GameObject summon;
    public override void ActivateEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayCard(Transform transform = null)
    {
      
        if (isSummoned) {
            return;
        }
        summon = Instantiate(Model);
        summon.GetComponent<Summon>().Card = this;
        summon.transform.position = transform.position;
        originalPosition = summon.transform.position;
        summon.transform.parent = transform;
        isSummoned = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the summon die, from the script that controls the AI, reference the card and remove it from card table, make sure to reset the card holder as well
    public void Die()
    {
        if (transform.parent.gameObject.GetComponent<CardHolder>()) {
            transform.parent.gameObject.GetComponent<CardHolder>().ResetHolder();

            Destroy(summon, 0.5f);
            Destroy(gameObject, 0.8f);
        }
    }


}
