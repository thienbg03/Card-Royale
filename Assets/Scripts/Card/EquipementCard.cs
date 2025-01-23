using UnityEngine;

public class EquipementCard : Card
{
    public PlayerState PlayerState;
    public override void ActivateEffect()
    {
       
    }

    public override void PlayCard(Transform summon = null)
    {
        PlayerManager.Instance.PlayerState = PlayerState;
        PlayerManager.Instance.EquipWeapon(this.Model);
        PlayerManager.Instance.EquipementCard = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Type = "equipment";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEquipmentCard()
    {
        transform.parent.gameObject.GetComponent<CardHolder>().ResetHolder();
        Destroy(gameObject, 0.5f);
    }
}
