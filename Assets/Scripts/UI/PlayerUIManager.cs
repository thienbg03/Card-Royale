using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Slider stamina;
    public Slider health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStamina(float stamina)
    {
        this.stamina.value = stamina;
    }

    public void SetHealth(float health) { 
        this.health.value = health;
    }
}
