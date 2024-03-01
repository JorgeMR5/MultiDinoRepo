using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Time Stats")]
    [SerializeField] TMP_Text timerText; //Variable para almacenar el texto del cronómetro
    [SerializeField] bool isCountdown; //Bool que determina si la cuenta es hacia adelante o regresiva 
    float timeElapsed; //Variable para determinar el paso del tiempo
    [SerializeField] float timeCountdown; //Tiempo de cuenta regresiva en segundos
    //Variables para contar los minutos, segundos, centésimas, etc...
    int minutes;
    int seconds;
    int cents;

    // Update is called once per frame
    void Update()
    {
        //Si isCountdown es falso, cuenta el tiempo hacia delante (Cronómetro)
        //Si isCountdown es verdadero, cuenta el tiempo hacia atrás (Cuenta Atrás)
        if (!isCountdown)
        {
            TimerUp();
        }
        else
        {
            TimerDown();
            if (timeCountdown <= 0)
            {
                timeCountdown = 0;
            }
        }

        //Añadir condición que complete el juego por tiempo
        //Si el juego es en modo cronómetro, timeCountdown tendrá que tener un valor de 1 o más
        if (timeCountdown == 0)
        {
            RoundManager roundManager = GameObject.FindObjectOfType<RoundManager>();
            roundManager.gameCompleted = true;
        }
        
    }

    void TimerUp()
    {
        timeElapsed += Time.deltaTime;
        minutes = (int)(timeElapsed / 60); //Casteo de int, coge el valor del float sin contar los decimales. 1,9 = 1. 2,5 = 2.
        seconds = (int) (timeElapsed - minutes * 60);
        cents = (int) ((timeElapsed - (int) timeElapsed) * 100);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
    }

    void TimerDown()
    {
        timeCountdown -= Time.deltaTime;
        minutes = (int)(timeCountdown / 60); //Casteo de int, coge el valor del float sin contar los decimales. 1,9 = 1. 2,5 = 2.
        seconds = (int)(timeCountdown - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
