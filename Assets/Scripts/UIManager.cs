using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Scene Manager Method - OnClick
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quit Game Method - OnClick
    public void QuitGame()
    {
        Application.Quit();
    }

    //When highlighe the button, it will scale up

    public void HighlightButton(RectTransform button)
    {
        button.DOScale(1.15f, 0.1f);
    }

    //When unhighlighe the button, it will scale down
    public void UnHighlightButton(RectTransform button)
    {
        button.DOScale(1f, 0.1f);
    }

}
