using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System;
using UnityEngine.UI;

public class RoomControllerScript : MonoBehaviour
{
    private enum doorState {
        Wall = 0,
        Open = 1, 
        Closed = 2
    };

    public GameController gameController;

    public List<GameObject> doors;

    private Camera cam;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 staticPosition;

    private float elapsedTime;
    private float duration = 0.15f;

    private bool inLerpOne = false;
    private bool inLerpTwo = false;

    private GameObject blackout;


    Dictionary <GameObject, GameObject> oppositeDoor = new Dictionary<GameObject, GameObject>();

    //List<GameObject> doors = new List<GameObject>();

    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public Sprite wallSprite;

    public Sprite hatchOpenSprite;
    public Sprite hatchClosedSprite;
    public Sprite floorSprite;

    private Color COLOR_BLACKOUT = new Color(0,0,0,1);
    private Color COLOR_SEETHROUGH = new Color(0,0,0,0);


    public GameObject hatch;


    //GameObject[] doors;

    GameObject doorLast;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        staticPosition = cam.transform.position;

        oppositeDoor.Add(doors[0], doors[1]);
        oppositeDoor.Add(doors[1], doors[0]);
        oppositeDoor.Add(doors[2], doors[3]);
        oppositeDoor.Add(doors[3], doors[2]);

        blackout = GameObject.FindGameObjectWithTag("Blackout");
    }  

    Vector3 cardinalVector(Vector3 vec)
    {
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            return new Vector3(vec.x, 0, vec.z);
        }
        else {
            return new Vector3(0, vec.y, vec.z);
        }
    }

    void Update()
    {
        if (inLerpOne | inLerpTwo) {
            elapsedTime += Time.unscaledDeltaTime;
            float percentage = elapsedTime / duration;
            cam.transform.position = Vector3.Lerp(startPosition, endPosition, percentage);
            if (inLerpOne)
            {
                Color color = Color.Lerp(COLOR_SEETHROUGH, COLOR_BLACKOUT, percentage);
                setBlackoutColor(color);
            }
            else if (inLerpTwo)
            {
                Color color = Color.Lerp(COLOR_BLACKOUT, COLOR_SEETHROUGH, percentage);
                setBlackoutColor(color);
            }

            if (elapsedTime>=duration) {
                if (inLerpOne)
                {
                    inLerpOne=false;
                    Time.timeScale=1;
                    startLerpTwo();    
                }
                else if (inLerpTwo)
                {
                    inLerpTwo=false;
                    Time.timeScale=1;
                }
                
            }
        }

        //setSprites();
    }

    void setBlackoutColor(Color color)
    {
        if (blackout.TryGetComponent<UnityEngine.UI.Image>(out UnityEngine.UI.Image image))
        {
            image.color = color;
        }
    }


    void startLerpOne(GameObject door)
    {
        startPosition = staticPosition;
        endPosition = door.transform.position * 2 + new Vector3(0,0,-10);
        endPosition = cardinalVector(endPosition);
        elapsedTime = 0f;
        inLerpOne=true;
        Time.timeScale=0;
    }

    void startLerpTwo()
    {
        startPosition = doorLast.transform.position * 2 + new Vector3(0,0,-10);
        startPosition = cardinalVector(startPosition);
        endPosition = staticPosition;
        elapsedTime = 0f;
        inLerpTwo=true;
        Time.timeScale=0;
    }

    public void setRoom(GameObject door, GameObject player)
    {
        // return if room not cleared
        if (!gameController.isClear()) {return;};

        // return if door not open
        int doorStateInt = (int) Variables.Object(door).Get("state");
        if (doorStateInt != 1) {return;}

        clearProjectiles();

        // special case new floor
        if (door==hatch) {nextFloor(); return;}

        if (GameController.traversedRooms>3 && UnityEngine.Random.value>0.5)
        {
            bossRoom(door, player);
        } else
        {
            normalRoom(door, player);
        }
           
        setSprites();
    }

    public void normalRoom(GameObject door, GameObject player)
    {
        doorLast = oppositeDoor[door];
        setNormalRoom();

        
        startLerpOne(door);

        player.transform.position = doorLast.transform.Find("Spawnpoint").transform.position;

        gameController.SpawnEnemies();
    }

    public void bossRoom(GameObject door, GameObject player)
    {
        doorLast = oppositeDoor[door];
        setBossRoom();
        player.transform.position = doorLast.transform.Find("Spawnpoint").transform.position;
        gameController.SpawnBoss(hatch.transform.Find("Spawnpoint").transform.position);
    }

    public void nextFloor()
    {
        // change tileset (?)
        GameController.floor+=1;
        GameController.traversedRooms=0;
        doorLast=hatch;
        setNormalRoom();
        Variables.Object(hatch).Set("state",0);
        setSprites();
        //animation ?
    }


    public void setBossRoom()
    {
        for (int i = 0; i < doors.Count(); i++) 
        {
            Variables.Object(doors[i]).Set("state",0);
        }
        Variables.Object(doorLast).Set("state",2);
        Variables.Object(hatch).Set("state",1);
    }

    doorState intToDoorState(int n)
    {
        doorState state;
        switch (n)
        {
            case 0: 
            state=doorState.Wall;
            break;
            case 1: 
            state=doorState.Open;
            break;
            case 2: 
            state=doorState.Closed;
            break;
            default:
            state=doorState.Wall;
            break;
        }
        return state;
    }

    public static void DeleteAllChildren(Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void clearProjectiles()
    {
        GameObject pParent = GameObject.FindGameObjectWithTag("projectileParent");
        DeleteAllChildren(pParent.transform);
    }

    void setNormalRoom()
    {
        Variables.Object(hatch).Set("state",0);
        Variables.Object(doorLast).Set("state",2);

        int r = UnityEngine.Random.Range(1,4); // 1-3
        int n = 0;

        doors = doors.OrderBy( x => UnityEngine.Random.value ).ToList( );

        for (int i = 0; i < doors.Count(); i++) 
        {
            if (doorLast != doors[i])
            {   
                GameObject currentDoor = doors[i];
                if (n<r) {
                    Variables.Object(currentDoor).Set("state",1);
                    n++;
                } else
                {
                    Variables.Object(currentDoor).Set("state",0);
                }
            }
        }
    }


    public void setSprites()
    {
        for (int i = 0; i < doors.Count(); i++) {
            setSprite(doors[i]);
        }
        setHatchSprite(hatch);
    }

    void setHatchSprite(GameObject hatch)
    {
        Sprite hatchSprite;

        int hatchState = (int) Variables.Object(hatch).Get("state");

        switch(hatchState)
        {
            case 0:
            hatchSprite=floorSprite;
            break;
            case 1: 
                if (gameController.isClear()) {
                    hatchSprite=hatchOpenSprite;
                } else {
                    hatchSprite = hatchClosedSprite;
                };
                break;
            case 2:
                hatchSprite = hatchClosedSprite;
                break;
            default:
                hatchSprite = floorSprite; break;
        }
        SpriteRenderer spriteRenderer = hatch.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = hatchSprite;
    }
    void setSprite(GameObject door)
    {
        Sprite doorSprite;

        int doorStateInt = (int) Variables.Object(door).Get("state");
        doorState state = intToDoorState(doorStateInt);

        switch (state)
        {
            case doorState.Wall:
                doorSprite=wallSprite;
                break;
            case doorState.Open:
                if (gameController.isClear()) {
                    doorSprite=doorOpenSprite;
                } else {
                    doorSprite = doorClosedSprite;
                }
                break;
            case doorState.Closed:
                doorSprite=doorClosedSprite;
                break;
            default:
                doorSprite=wallSprite;
                break; 
        }
        SpriteRenderer spriteRenderer = door.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = doorSprite;
    }
}
