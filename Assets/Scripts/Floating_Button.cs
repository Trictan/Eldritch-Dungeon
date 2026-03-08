using UnityEngine;

public class Floating_Button : monobehaviour
{
    public float amplitude;
    public float frequency;

    public Vector3 rotation;

    private float _t;

    // Start is called once before the first execution of Update after the monobehaviour is created
    void Start()
    {
        rotation = gameObject.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.unscaledDeltaTime;
        gameObject.transform.localEulerAngles = new Vector3(0,0, amplitude * Mathf.Sin(frequency * _t));
    }


}
