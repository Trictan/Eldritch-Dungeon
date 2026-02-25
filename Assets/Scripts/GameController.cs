using UnityEngine;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{


    public RoomControllerScript roomController;
    public GameObject testEnemy;
    public GameObject player;
    public GameObject enemiesParentNode;

    public List<List<GameObject>> enemies;
    List<Vector3> spawnPoints = new List<Vector3>();
    int floor;
    int traversedRooms;
    bool previousRoomStatus=true;

    void Start()
    {
        for (int i = 0; i<18; i++)
        {
            for (int k = 0; k<8 ;k++)
            {
                float x = i - 8.5f;
                float y = 3.5f - k;
                Vector3 currentSpawnPoint = new Vector3(x,y, 0);
                spawnPoints.Add(currentSpawnPoint);
            }
        }

        floor=0;
        traversedRooms=0;
    }

    void Update()
    {
        if (changeInRoomStatus())
        {
            print(isClear());
            roomController.setSprites();
            previousRoomStatus = isClear();
        }
    }

    public bool isClear()
    {
        return enemiesParentNode.transform.childCount == 0;
    }

    bool changeInRoomStatus()
    {
        bool currentRoomStatus = isClear();
        return (currentRoomStatus != previousRoomStatus);
    }



    bool CheckDistance(Vector3 u, Vector3 v, float minDistance)
    {
        float distance = (u-v).magnitude;
        if (distance >= minDistance) {return true;} else {return false;}
    }

    bool ValidSpawn(Vector3 pos)
    {
        // Too close to player
        if (!CheckDistance(player.transform.position, pos, 2f)) {return false;}

        // Too close to other enemy
        foreach (Transform child in enemiesParentNode.transform) {
            if (!CheckDistance(child.position, pos, 1f)) {return false;}
        }

        // Valid spawn
        return true;
    }

    void SpawnEnemy(GameObject enemy, Vector3 pos)
    {
        GameObject enemyInstance = Instantiate(enemy, pos, Quaternion.identity) as GameObject;
        enemyInstance.transform.parent = enemiesParentNode.transform;
    }
    
    int NrOfEnemies(){
        
        if (floor<=3){
            int max = traversedRooms * floor;
            max = max + 1;
        }
        else{
            int max = traversedRooms * (floor - (floor/2));
        }
        return max;
        
    }


    public void SpawnEnemies() // add parameters to decide what enemies, how many, etc
    {
        int count = 0;
        while (count<3)
        {
            int r = Random.Range(floor, NrOfEnemies());
            Vector3 spawnPoint = spawnPoints[r];
            if (ValidSpawn(spawnPoint))
            {
              SpawnEnemy(testEnemy, spawnPoint);
              count+=1;
            };
        }
    }
}
