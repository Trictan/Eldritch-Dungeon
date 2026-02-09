using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    
    public GameObject[] hearts;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float health = player.GetComponent<LifeSystem>().GetCurrentHealth();

        for(int i = hearts.Length-1; i >= 0; i--)
        {
            float heartHealth = Mathf.Clamp(health - i, 0, 1);

            if(heartHealth == 1)
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            else if(heartHealth == 0.5)
                hearts[i].GetComponent<SpriteRenderer>().sprite = halfHeart;
            else
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;

            health -= heartHealth;
        }
    }
}
