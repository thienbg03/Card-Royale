using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject CardArenaCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CardArenaCanvas.SetActive(false);
            Debug.Log("np");
        }

        if (Input.GetMouseButtonDown(0))
        {
            CardArenaCanvas.SetActive(true);
            Debug.Log("Card Arena");
        }
    }
}

