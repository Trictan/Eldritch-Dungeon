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

    public GameObject doorU;
    public GameObject doorD;
    public GameObject doorL;
    public GameObject doorR;

    private Camera cam;
    private Vector3 endPosition;
    private Vector3 startPosition;

    private float elapsedTime;
    private float duration = 0.25f;

    private bool inLerp = false;

    

    Dictionary <string, GameObject> oppositeDoor = new Dictionary<string, GameObject>();

    List<GameObject> doors = new List<GameObject>();

    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public Sprite wallSprite;

    //GameObject[] doors;

    GameObject doorLast;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        endPosition = cam.transform.position;

        doors.Add(doorU);
        doors.Add(doorD);
        doors.Add(doorL);
        doors.Add(doorR);

        oppositeDoor.Add("DoorU", doorD);
        oppositeDoor.Add("DoorD", doorU);
        oppositeDoor.Add("DoorL", doorR);
        oppositeDoor.Add("DoorR", doorL);
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
        // return if door not open
        int doorStateInt = (int) Variables.Object(door).Get("state");
        if (doorStateInt != 1) {return;}

        doorLast = oppositeDoor[door.name];

        nextRoom();
        for (int i = 0; i < doors.Count(); i++) {
            setSprite(doors[i]);
        }

        startPosition = doorLast.transform.position * 2; //(doorLast.transform.position - player.transform.position).normalized *15;
        startPosition = cardinalVector(startPosition);
        player.transform.position = doorLast.transform.Find("Spawnpoint").transform.position;
        startLerp();
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

    void nextRoom()
    {
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
                doorSprite=doorOpenSprite;
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
