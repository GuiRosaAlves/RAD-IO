using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance; //Singleton

    public GameObject repetidorPrefab;
    public GameObject emissorPrefab;

    public List<Transform> listOfRepetidores;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        
    }
	
	void Update () {
        
	}

    public void NextPhase()
    {
        print("Acertooo miserávi");
    }
}
