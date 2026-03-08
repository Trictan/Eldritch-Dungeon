using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System;

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

    private float elapsedTime;
    private float duration = 0.25f;

    private bool inLerp = false;


    Dictionary <GameObject, GameObject> oppositeDoor = new Dictionary<GameObject, GameObject>();

    //List<GameObject> doors = new List<GameObject>();

    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public Sprite wallSprite;

    public Sprite hatchOpenSprite;
    public Sprite hatchClosedSprite;
    public Sprite floorSprite;


    public GameObject hatch;


    //GameObject[] doors;

    GameObject doorLast;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        endPosition = cam.transform.position;

        oppositeDoor.Add(doors[0], doors[1]);
        oppositeDoor.Add(doors[1], doors[0]);
        oppositeDoor.Add(doors[2], doors[3]);
        oppositeDoor.Add(doors[3], doors[2]);

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
        if (inLerp) {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;
            cam.transform.position = Vector3.Lerp(startPosition, endPosition, percentage);
            if (elapsedTime==duration) {
                inLerp=false;
            }
        }
    }



    void startLerp()
    {
        elapsedTime = 0f;
        inLerp=true;
    }

    public void setRoom(GameObject door, GameObject player)
    {
        // return if room not cleared
        if (!gameController.isClear()) {return;};

        // return if door not open
        int doorStateInt = (int) Variables.Object(door).Get("state");
        if (doorStateInt != 1) {return;}

        if (door==hatch) {nextFloor(); return;}

        doorLast = oppositeDoor[door];

        nextRoom();
        setSprites();

        startPosition = doorLast.transform.position * 2; //(doorLast.transform.position - player.transform.position).normalized *15;
        startPosition = cardinalVector(startPosition);
        player.transform.position = doorLast.transform.Find("Spawnpoint").transform.position;

        gameController.SpawnEnemies();
        startLerp();
    }

    public void nextFloor()
    {
        gameController.floor+=1;
        doorLast=hatch;
        nextRoom();
        setSprites();
        //animation
    }


    public void setBossRoom()
    {
        for (int i = 0; i < doors.Count(); i++) 
        {
            GameObject currentDoor = doors[i];
            Variables.Object(currentDoor).Set("state",2);
            if (doorLast != doors[i])
            {   
                Variables.Object(currentDoor).Set("state",0);
            }
        }

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

    void nextRoom()
    {
        clearProjectiles();

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
        }
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
