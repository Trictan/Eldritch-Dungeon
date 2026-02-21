using UnityEngine;
using UnityEngine.UI;
public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    private LifeSystem life;
    private float maxHealth;


    void Awake()
    {
        life = GetComponentInParent<LifeSystem>();

        maxHealth = life.GetMaxHealth();
        slider.maxValue = maxHealth;
        slider.value = life.GetCurrentHealth();
    }
    // Update is called once per frame
    void Update()
    {
        float currentHealth = life.GetCurrentHealth();
        slider.value = currentHealth;
        slider.gameObject.SetActive(currentHealth < maxHealth);
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
