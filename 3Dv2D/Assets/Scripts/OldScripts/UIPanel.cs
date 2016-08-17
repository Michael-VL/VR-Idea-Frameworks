using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{

    public bool StartPanel;
    public bool SoundPanel;
    public bool ExitPanel;

    public Text SoundText;
    public Text StartText;

    public GameObject[] panels;

    bool soundPlaying = true;
    AudioSource music;
    public GameObject musicSource;

    public bool notMenu;

    void Start()
    {
        music = musicSource.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("menuCol");
        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "ControllerL" || col.gameObject.tag == "ControllerR")
        {
            //Debug.Log("menu");
            if (StartPanel)
            {
                if (notMenu)
                {
                    //Debug.Log("Game Start");
                    Manager.gameStart = true;
                    foreach (GameObject _panel in panels)
                    {
                        _panel.SetActive(false);
                    }
                    Destroy(col.gameObject);
                }
                else
                {
                    Application.LoadLevel("Snow Scene");
                }
            }
            else if (SoundPanel)
            {
                if(music.volume == 0f)
                {
                    music.volume = 1;
                }
                else
                {
                    music.volume = 0f;
                }
                
                soundPlaying = !soundPlaying;
                if (notMenu)
                {
                    if (soundPlaying)
                    {
                        SoundText.text = "ON";
                    }
                    else
                    {
                        SoundText.text = "OFF";
                    }
                    Destroy(col.gameObject);
                }
            }
            else if (ExitPanel)
            {
                Application.Quit();
            }
        }
    }
}