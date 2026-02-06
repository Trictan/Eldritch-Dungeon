using UnityEngine;
using Unity.VisualScripting;

public class player : MonoBehaviour
{

    //public PlayerStats reference;
    
    public bool iFrame;
    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(iFrame)
        {
            timer +=Time.deltaTime;
            //if(timer>reference.GetiFrames())
            //{
                iFrame=false;
                timer=0;
            //}
        }
        
    }
    public void SetiFrame()
    {
        iFrame=true;
    }
    public bool GetiFrame()
    {
        return iFrame;
    }
}
