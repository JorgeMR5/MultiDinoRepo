using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject roundManager;
    RoundManager roundScript;
    [SerializeField] GameObject[] pickUpPrefabs;
    [SerializeField] Transform[] generators;
    [SerializeField] float castTime;
    [SerializeField] float repeatTime;
    private int pickUpIndex;
    private int generatorIndex;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("RoundManager");
        roundScript = roundManager.GetComponent<RoundManager>();
        InvokeRepeating("GeneratePickUp", castTime, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (roundScript.gameCompleted) { CancelInvoke(); }
    }

    void GeneratePickUp()
    {
        pickUpIndex = Random.Range(0, pickUpPrefabs.Length - 1);
        generatorIndex = Random.Range(0, generators.Length - 1);
        Instantiate(pickUpPrefabs[pickUpIndex], generators[generatorIndex].position, Quaternion.identity);
        //Instantiate(prefab, posicion a generar, rotación con la que se genera)
    }
}
