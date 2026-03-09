using UnityEngine;
using UnityEngine.UI;
public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Image frame;
    public Color low;
    public Color high;
    private LifeSystem life;
    private float maxHealth;

    private Image fillImage;
    void Awake()
    {
        life = GetComponentInParent<LifeSystem>();

        maxHealth = life.GetMaxHealth();
        slider.maxValue = maxHealth;
        slider.value = life.GetCurrentHealth();

        fillImage = slider.fillRect.GetComponent<Image>();

        RectTransform rt = slider.GetComponent<RectTransform>();
        RectTransform frameRT = frame.GetComponent<RectTransform>();

        SpriteRenderer enemySprite = GetComponentInParent<SpriteRenderer>();
        Vector3 localScale = enemySprite.transform.localScale;


        Vector3 spriteBounds = enemySprite.sprite.bounds.size;

        // Sätt bredd baserat på enemy
        rt.sizeDelta = new Vector2((spriteBounds.x * 100f) - 8, rt.sizeDelta.y/localScale.y);
        frameRT.sizeDelta = new Vector2((spriteBounds.x * 100f) , frameRT.sizeDelta.y/localScale.y);

        // Sätt position under enemy
        transform.localPosition = new Vector3((localScale.x-1)*(spriteBounds.x/2), -(spriteBounds.y/(Mathf.Pow(2,localScale.y))), 0);
        }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = life.GetCurrentHealth();
        bool shouldBeActive = currentHealth < maxHealth;

        if (slider.gameObject.activeSelf != shouldBeActive)
        {
            slider.gameObject.SetActive(shouldBeActive);
            frame.gameObject.SetActive(shouldBeActive);

        }
        if (slider.gameObject.activeSelf)
        {
            slider.value = currentHealth;
            fillImage.color = Color.Lerp(low, high, slider.normalizedValue);
        }
        
    }
}
