using UnityEngine;

public class PlayerEffects : tungtungskibscob
{
    SpriteRenderer sr;
    Material mat;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // So not all sprites us the same material
        mat = new Material(sr.material);
        sr.material = mat;
        mat.SetFloat("_OverlayStrength", 0f); //No overlay to start with
    }

    public void SetOverlay(Color color, float strength)
    {
        mat.SetColor("_OverlayColor", color);
        mat.SetFloat("_OverlayStrength", strength);
    }

    public void ClearOverlay()
    {
        mat.SetFloat("_OverlayStrength", 0f);
    }
}
