using UnityEngine;
using UnityEngine.UI;


public class UpgradesHandler : MonoBehaviour
{

    public Color COLOR_LOCKED;
    public Color COLOR_DESELECTED;
    public Color COLOR_SELECTED;

    public static bool inUpgrades;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;

    public GameObject leftButton;
    public GameObject rightButton;

    public GameObject selectButton;

    GameObject selectedButton;

    private PlayerStats playerStats;

    private Upgrade[] potentialUpgrades;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerStats>();
        setVisible(false);
    }

    private void setStat(string stat, object value)
    {
        switch(stat)
        {
            case "dmg": playerStats.dmg = (float) value; break;
            case "projectileSpeed": playerStats.projectileSpeed = (float) value; break;
            case "projectileHits": playerStats.projectileHits = (int) value; break;
            case "attackDelay": playerStats.attackDelay = (float) value; break;
        }
    }

    private void createUpgradePool()
    {
        
    }

    public void setSelected(GameObject button) 
    {
        resetSelection();
        setButtonBg(button, COLOR_SELECTED);
        selectedButton = button;
        setButtonBg(selectButton, COLOR_DESELECTED);
    }

    void resetSelection()
    {
        setButtonBg(leftButton, COLOR_DESELECTED);
        setButtonBg(rightButton, COLOR_DESELECTED);
    }

    void setButtonBg(GameObject button, Color color)
    {
        if (button.TryGetComponent<Image>(out Image image))
        {
            image.color = color;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (inUpgrades)
            {
                CloseUpgrades();
            } else
            {
                OpenUpgrades();
            }
        }
    }

    public bool InUpgrades()
    {
        return inUpgrades;
    }

    void setVisible(bool val)
    {
        canvas.enabled = val;
        spriteRenderer.enabled = val;
    }

    public void OpenUpgrades()
    {
        Time.timeScale = 0;
        setVisible(true);
        inUpgrades=true;

        selectedButton=null;
        setButtonBg(selectButton, COLOR_LOCKED);
    }

    public void CloseUpgrades()
    {
        if(selectedButton==null) {return;}
        resetSelection();
        Time.timeScale = 1;
        setVisible(false);
        inUpgrades=false;
        Upgrade();
    }

    public void Upgrade()
    {
        print(selectedButton.name);
    }

}

class Upgrade<T> {
   
    public string name;
    public int mutiplier;
    public float offset;
    public float offsetMultiplier;
    public T currentValue;

    // min or max
    public T capValue;

    // toggle, increase, decrease
    public int type;

    public string description;
   
    public Upgrade(string name, float offset, float offsetMultiplier, T currentValue, T capValue, int type, string description) {
        this.name = name;
        this.mutiplier = mutiplier;
        this.offset=offset;
        this.offsetMultiplier=offsetMultiplier;
        this.currentValue=currentValue;
        this.capValue=capValue;
        this.type=type;
        this.description=description;
    }
}
