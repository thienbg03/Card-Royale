using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    private Image backgroundImage;
    private Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void MouseHover()
    {
        print("OnMouseEnter");
        backgroundImage.color = Color.white;
        text.color = new Color(0.1568628f, 0.1568628f, 0.1568628f);
    }

    public void MouseExit()
    {
        text.color = Color.white;
        backgroundImage.color = new Color(0.1568628f, 0.1568628f, 0.1568628f);
    }


    public void MouseClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
