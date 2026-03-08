using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class GameController : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public RoomControllerScript roomController;
    public GameObject testEnemy;
    public GameObject player;
    public GameObject enemiesParentNode;
    public List<GameObject> enemiesOne;
    public List<GameObject> enemiesTwo;
    public List<GameObject> bosses;
    List<Vector3> spawnPoints = new List<Vector3>();
    public static int floor=1;
    public static int traversedRooms=0;
    bool previousRoomStatus=true;

    public UpgradesHandler upgradesHandler;


    public GameObject rangedWeapon;
    public GameObject meleeWeapon;
    // maybe tmp
    public GameObject playerProjectile;
    public GameObject projectileFolder;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();

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
        floor = 1;
        traversedRooms = 0;
    }

    void Update()
    {
        if (changeInRoomStatus())
        {
            textElement.text=$"Floor {floor}\nRooms Cleared: {traversedRooms}";
            roomController.setSprites();
            previousRoomStatus = isClear();

            if(isClear()) {    
                roomCleared();
            } 
            else
            {
                roomEntered();
            }
        }
        
        if (isClear()) {initiateLevelUp();}
    }

    // will always be at least 1 hit as it checks at trigger
    
    public void setRangedWeapon()
    {
        GameObject rangedWeaponInstance = Instantiate(rangedWeapon, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        rangedWeaponInstance.transform.parent = player.transform;
        playerStats.hasRanged=1;
    }

    public void setMeleeWeapon()
    {
        GameObject meleeWeaponInstance = Instantiate(meleeWeapon, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        meleeWeaponInstance.transform.parent = player.transform;
        playerStats.hasMelee=1;
    }


    public void initiateLevelUp()
    {
        int levelUp = player.GetComponent<LevelSystem>().getGainedLevels();
        if (levelUp > 0)
        {
                //Call to upgrade scene
            upgradesHandler.OpenUpgrades();
            player.GetComponent<LevelSystem>().resetGainedLevels();
            Debug.Log("Upgrade");
        }
    }

    public void roomCleared()
    {
        traversedRooms = traversedRooms + 1;
        if (SoundEffectManager.Instance)
        {
            SoundEffectManager.Instance.DoorOpen();
        }
    }


    public void roomEntered()
    {
        if (SoundEffectManager.Instance)
        {
            SoundEffectManager.Instance.DoorClose();
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
        int max;
        if (floor<=3){
            max = traversedRooms * floor;
            max = max + 1;
        }
        else{
            max = traversedRooms * (floor - Mathf.RoundToInt(floor/2));
        };

        int nr = Random.Range(floor, max);
        return nr;
        
    }

    public GameObject pickEnemy()
    {
        // floor as parameter too 
        if (traversedRooms<3)
        {
            return enemiesOne[0];
        } else
        {
            int r = Random.Range(0, enemiesOne.Count);
            return enemiesOne[r];
        }
    }

    public GameObject pickBoss()
    {
        // could have parameters, etc
        int r = Random.Range(0, bosses.Count);
        return bosses[r];
    }

    public void SpawnBoss(Vector3 pos) // add parameters to decide what enemies, how many, etc
    {
        GameObject bossPrefab = pickBoss();
        SpawnEnemy(bossPrefab, pos);
    }

    public void SpawnEnemies() // add parameters to decide what enemies, how many, etc
    {
        int count = 0;
        int nrOfEnemies = NrOfEnemies();
        while (count<nrOfEnemies)
        {
            GameObject enemyPrefab = pickEnemy();
            int r = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPoint = spawnPoints[r];
            if (ValidSpawn(spawnPoint))
            {
              SpawnEnemy(enemyPrefab, spawnPoint);
              count+=1;
            };
        }
    }
}
