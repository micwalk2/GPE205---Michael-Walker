using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    // List that holds our player(s)
    public List<PlayerController> players;

    private void Awake()
    {
        // Make sure there is only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Temp code to spawn the player as the GameManager starts
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        // Spawn the player controller at the origin
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Get the PlayerController and Pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        // Connect the controller to the pawn
        newController.pawn = newPawn;
    }
}
