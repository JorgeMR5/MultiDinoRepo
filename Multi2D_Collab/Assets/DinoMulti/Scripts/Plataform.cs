using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private int i; //Indice del array de puntos para que la plataforma detecte un punbto a seguir.
    [Header("Point & Movement Configuratiob")]
    [SerializeField] Transform[] points;// Array de puntos de posición hacai los que la plataforma se moverá.
    [SerializeField] int startingPoint; //Número para determinar el indice del punto de inicio de la plataforma.
    [SerializeField] float speed;//Velocodad de la plataforma.

    // Start is called before the first frame update
    void Start()
    {
        //Setear la posición inicial de la plataforma a uno de los puntos, asignando a stringPoint un valor numérico.
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;//Aumenta el indice, cambia el objeto hacia el que moverse.
            if (i == points.Length)//Chequea si la plataforma ha llegado al último punto del array
            {
                i = 0;//Resetea el indice para volver a empezar, la plataforma va hacia el punto 0
            }
        }

        //Mueve la plataforma a la posición del punto guardado en el array...
        //... que corresponda al espacaio del array con valor igual a la variable "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                //El tranform del objeto que sale de la colisión (player)pierde su condición de hijo del transform de la plataforma
                collision.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //El tranform del objeto que sale de la colisión (player)pierde su condición de hijo del transform de la plataforma
            collision.transform.SetParent(null);
        }
    }
}

