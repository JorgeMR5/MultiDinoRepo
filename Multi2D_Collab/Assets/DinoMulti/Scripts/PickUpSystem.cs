using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private float timeToDestroy = 10;
    RoundManager roundScript;

    private void Start()
    {
        roundScript = GameObject.FindObjectOfType<RoundManager>();
        Invoke("DestroyPickUp", timeToDestroy);
    }

    void DestroyPickUp()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("P1"))
        {
            roundScript.player1Points += score;
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("P2"))
        {
            roundScript.player2Points += score;
            gameObject.SetActive(false);
        }
    }
}
