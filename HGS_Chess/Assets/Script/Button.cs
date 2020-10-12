using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public static int ESC;
    public AudioClip  SoundButton;
    AudioSource       audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene("GameScene 1");
            audioSource.PlayOneShot(SoundButton);
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }


}
