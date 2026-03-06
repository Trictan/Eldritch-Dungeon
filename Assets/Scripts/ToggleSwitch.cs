using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleSlider : MonoBehaviour
{
    [Header("Slider & Visuals")]
    public Slider slider;            // Slidern i scenen
    public Image background;         // Bakgrundsbild
    public RectTransform handle;     // Handtagets RectTransform

    [Header("Colors")]
    public Color offColor = Color.gray;
    public Color onColor = Color.green;

    [Header("Animation")]
    public float duration = 0.5f;

    [Header("Setting for")]
    public string command;
    private bool isOn = true;
    private Coroutine currentAnim;
    private Vector2 handleOffPos;
    private Vector2 handleOnPos;

    void Start()
    {
        // Init slider
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.interactable = false;

        //Set handle positions
        handleOffPos = handle.anchoredPosition;
        handleOnPos = new Vector2(1 * (slider.GetComponent<RectTransform>().rect.width - handle.rect.width), handleOffPos.y);

        isOn = PlayerPrefs.GetInt(command, 1) == 1;
        
        // Set the correct settings
        slider.value = isOn ? 1f : 0f;
        // Init visuals
        if (background) background.color = isOn ? onColor : offColor;

        handle.anchoredPosition = isOn ? handleOnPos : handleOffPos;
    }

    // Conected to the button on top of the slider
    public void Toggle()
    {
        isOn = !isOn;

        if(command == "SoundOn") SoundEffectManager.Instance.ToggleSoundEffect();
        if(command == "MusicOn") MusicManager.Instance.ToggleMusic();

        // Stoppa tidigare animation
        if (currentAnim != null) StopCoroutine(currentAnim);

        currentAnim = StartCoroutine(AnimateToggle(isOn ? 1f : 0f, isOn ? onColor : offColor));
    }

    private IEnumerator AnimateToggle(float targetValue, Color targetColor)
    {
        float startValue = slider.value;
        Color startColor = background.color;
        float time = 0f;
        

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Slider value
            slider.value = Mathf.Lerp(startValue, targetValue, t);

            // Background color
            if (background) background.color = Color.Lerp(startColor, targetColor, t);

            // Handle position
            if (handle) {
                if(targetValue < 1)handle.anchoredPosition = Vector2.Lerp(handleOnPos, handleOffPos, t);
                else handle.anchoredPosition = Vector2.Lerp(handleOffPos, handleOnPos, t);
            }

            yield return null;
        }

        slider.value = targetValue;
        if (background) background.color = targetColor;
    }
}
