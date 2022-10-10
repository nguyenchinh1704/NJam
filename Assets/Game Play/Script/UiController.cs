using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] int gameLevelNumber;

    public static UiController instance;
    private void OnEnable()
    {
        instance = this;
    }
   
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        startPanel.SetActive(false);
             
    }
    public void Restart()
    {
        SceneManager.LoadScene(gameLevelNumber);
    }
}
