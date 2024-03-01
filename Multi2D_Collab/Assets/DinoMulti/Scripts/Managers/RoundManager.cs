using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoundManager : MonoBehaviour
{
    //Declaración de variables del GameManager
    [Header("General Stats")]
    public int player1Points;
    public int player2Points;
    public bool gameCompleted;

    [Header("User Interface")]
    [SerializeField] TMP_Text p1Text;
    [SerializeField] TMP_Text p2Text;
    [SerializeField] GameObject winPanel;
    [SerializeField] TMP_Text winText;

    Image victoryBackground;

    // Start is called before the first frame update
    void Start()
    {
        victoryBackground = winPanel.GetComponent<Image>();
        gameCompleted = false;
        player1Points = 0;
        player2Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdater();
        Conditions();

        if (gameCompleted)
        {
            GameOver();
        }
    }

    void Conditions()
    {
        //Condición para que los puntos de los jugadores no bajen de cero
        if (player1Points < 0)
        {
            player1Points = 0;
        }
        if (player2Points < 0)
        {
            player2Points = 0;
        }
    }

    void UIUpdater()
    {
        p1Text.text = player1Points.ToString();
        p2Text.text = player2Points.ToString();
    }

    void GameOver()
    {
        //Hay que poner las condiciones por las que acaba el juego

        //Se activa el panel de juego completo
        winPanel.SetActive(true);

        if (player1Points > player2Points)
        {
            victoryBackground.color = new Color32(10, 200, 98, 255);
            winText.text = "The winner is green dino with " + player1Points.ToString() + " points!";
        }
        if (player1Points < player2Points)
        {
            victoryBackground.color = new Color32(142, 5, 18, 255);
            winText.text = "The winner is red dino with " + player2Points.ToString() + " points!";
        }
        if (player1Points == player2Points)
        {

            victoryBackground.color = new Color32(121, 108, 80, 255);
            winText.text = "It's a tie! Both dinos win with " + player1Points.ToString() + " points!";
        }
    }

    public void RoundEnd()
    {
        if (GameManager.Instance.currentRound == 3)
        {
            //SceneManager.LoadScene(0);
        }
        else
        {
            GameManager.Instance.currentRound++;
            int randomScene = Random.Range(2, GameManager.Instance.sceneQuantity);
            SceneManager.LoadScene(randomScene);
        }
    }

    public void BackToMenu()
    {
        GameManager.Instance.ResetGameStatus();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
