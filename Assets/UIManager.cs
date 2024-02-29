using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHightScoreUI;

    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;

        gm.onGameOver.AddListener(ActivateGameOverMenuUI);
    }

    public void PlayButtonHandler()
    {
        gm.StartGame(); 
       
    }

    public void QuitButton()
    {
        gm.QuitGame();
    }

    public void ActivateGameOverMenuUI()
    {
        gameOverUI.SetActive(true);

        gameOverScoreUI.text = "Score:" + gm.PrettyScore();
        gameOverHightScoreUI.text = "Highscore:" + gm.PrettyHighscore();
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }

}
