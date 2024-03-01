using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Declaramos el singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("No hay GameManager");
            }
            return instance;
        }
    }
    //Fin del Singleton, se guarda en el Awake

    //Declaración de variables del GameManager
    public int sceneQuantity;
    public int p1RoundWins;
    public int p2RoundWins;
    public int currentRound;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetGameStatus()
    {
        p1RoundWins = 0;
        p2RoundWins = 0;
        currentRound = 0;
    }
    
   
}
