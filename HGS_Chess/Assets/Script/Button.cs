using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        if (Input.GetButtonDown("StartScene"))
        {
            fade.FadeIn(0.5f, () => print("フェードイン完了"));
            SceneManager.LoadScene("GameScene");
            fade.FadeOut(0.5f, () => print("フェードアウト完了"));
        }


    }

    public void MenuButton()
    {
    }

    public void ResultButton()
    {

    }
}
