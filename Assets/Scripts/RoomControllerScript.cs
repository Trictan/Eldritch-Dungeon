using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

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

    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public Sprite wallSprite;

    //GameObject[] doors;

    GameObject doorLast;
    List<GameObject> doors = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        doors.Add(doorU);
        doors.Add(doorD);
        doors.Add(doorL);
        doors.Add(doorR);
    }  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true) {
            int doorStateInt = (int) Variables.Object(doorU).Get("state");
            if (doorStateInt==1)
            {
                doorLast = doorD;
                setRoom();
            } else
            {
                print("up: " + doorStateInt);
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) == true) {
            int doorStateInt = (int) Variables.Object(doorD).Get("state");
            if (doorStateInt==1)
            {
                doorLast = doorU;
                setRoom();
            } else
            {
                print("down: " + doorStateInt);
            }
            
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true) {
            int doorStateInt = (int) Variables.Object(doorL).Get("state");
            if (doorStateInt==1)
            {
                doorLast = doorR;
                setRoom();
            } else
            {
                print("left: " + doorStateInt);
            }
            
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            int doorStateInt = (int) Variables.Object(doorL).Get("state");
            if (doorStateInt==1)
            {
                doorLast = doorL;
                setRoom();
            } else
            {
                print("right: " + doorStateInt);
            }
            
        }
            
    }

    void setRoom()
    {
        randomizeRoom();
        setColor(doorU);
        setColor(doorD);
        setColor(doorR);
        setColor(doorL);
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

    void randomizeRoom()
    {
        Variables.Object(doorLast).Set("state",2);

        int r = Random.Range(1,4);
        int n = 0;

        doors = doors.OrderBy( x => Random.value ).ToList( );
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

        //int R1 = Random.Range(0,3);
        //int R2 = Random.Range(0,3);
        //int R3 = Random.Range(0,3);
        //int R4 = Random.Range(0,3);

        //Variables.Object(doorU).Set("state", R1);
        //Variables.Object(doorD).Set("state", R2);
        //Variables.Object(doorL).Set("state", R3);
        //Variables.Object(doorR).Set("state", R4);
    }

    void setColor(GameObject door)
    {
        Color doorColor;
        Sprite doorSprite;

        int doorStateInt = (int) Variables.Object(door).Get("state");
        doorState state = intToDoorState(doorStateInt);

        switch (state)
        {
            case doorState.Wall:
                doorColor=Color.green;
                doorSprite=wallSprite;
                break;
            case doorState.Open:
                doorColor=Color.blue;
                doorSprite=doorOpenSprite;
                break;
            case doorState.Closed:
                doorColor=Color.red;
                doorSprite=doorClosedSprite;
                break;
            default:
                doorColor=Color.white;
                doorSprite=wallSprite;
                break; 
        }
        SpriteRenderer spriteRenderer = door.GetComponentInChildren<SpriteRenderer>();
        //spriteRenderer.color = doorColor;
        spriteRenderer.sprite = doorSprite;
    }
}
