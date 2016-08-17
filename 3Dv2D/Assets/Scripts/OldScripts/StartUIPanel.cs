using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUIPanel : MonoBehaviour {

    public string[] levels;
    public GameObject StartPanels;
    public GameObject LevelSelect;

    public void Start()
    {
        StartPanels.SetActive(false);
        LevelSelect.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        StartPanels.SetActive(true);
        LevelSelect.SetActive(false);
    }

    public void OneLeve()
    {
        SceneManager.LoadScene(levels[0]);
    }
    public void TwoLeve()
    {
        SceneManager.LoadScene(levels[1]);
    }
    public void ThreeLeve()
    {
        SceneManager.LoadScene(levels[2]);
    }
    public void FourLeve()
    {
        SceneManager.LoadScene(levels[3]);
    }
    public void FiveLeve()
    {
        SceneManager.LoadScene(levels[4]);
    }
}
