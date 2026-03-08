using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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

    public GameController gameController;

    GameObject selectedButton;

    private PlayerStats playerStats;
    private GameObject cursor;

    Dictionary<string, string> upgradeDescriptions =
    new Dictionary<string, string>
    {
        {"projectileHits", "Projectiles hit additional enemies."},
        {"attackDelay", "Shorten delay between attacks."},
        {"dmg", "Increase damage dealt to enemies."},
        {"moveSpeed", "Increase player movement speed."},
        {"numberOfProjectiles", "+1 projectiles fired"}
    };

    Dictionary<string, Dictionary<string, float>> upgrades =
    new Dictionary<string, Dictionary<string, float>>{
        {"projectileHits", new Dictionary<string, float>
            {
                {"capValue", 3f},
                {"offset", 1f},
                {"multiplier", 1f},
                {"increasing", 1f},
                {"type", 1f},
            }
        },
        {"attackDelay", new Dictionary<string, float>
            {
                {"capValue", 0.15f},
                {"offset", -0.1f},
                {"multiplier", 1f},
                {"increasing", 0f},
            }
        },
        {"dmg", new Dictionary<string, float>
            {
                {"capValue", 2.5f},
                {"offset", 0f},
                {"multiplier", 1.2f},
                {"increasing", 1f},
            }
        },
        {"moveSpeed", new Dictionary<string, float>
            {
                {"capValue", 5f},
                {"offset", 1f},
                {"multiplier", 1f},
                {"increasing", 1f},
            }
        },
        {"numberOfProjectiles", new Dictionary<string, float>
            {
                {"capValue", 3f},
                {"offset", 1f},
                {"multiplier", 1f},
                {"increasing", 1f},
                {"type", 1f},
            }
        },
    };

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerStats>();
        setVisible(false);
        cursor = GameObject.FindGameObjectWithTag("cursor");
    }

    // ..would not fly in prod..
    // skib skulle flygit
    // lets call it proof of concept.
    private void AddUpgrades()
    {
        // string name, float offset, float currentValue, float capValue, int type, 
        // float offsetMultiplier = 1, string description="No description.", int minLevel=0
    }

    private void setStat(string stat, float value)
    {
        switch(stat)
        {
            case "dmg": playerStats.dmg = (float) value; break;
            case "projectileSpeed": playerStats.projectileSpeed = (float) value; break;
            case "projectileHits": playerStats.projectileHits = (int) value; break;
            case "attackDelay": playerStats.attackDelay = (float) value; break;
            case "moveSpeed": playerStats.movementSpeed = (float) value; break;
            case "numberOfProjectiles": playerStats.numberOfProjectiles = (int) value; break;
            case "meleeWeapon": gameController.setMeleeWeapon(); playerStats.hasMelee=1; break;
            case "rangedWeapon": gameController.setRangedWeapon(); playerStats.hasRanged=1; break;
        }
    }

    private float getStat(string stat)
    {
        switch(stat)
        {
            case "dmg": return (float) playerStats.dmg;
            case "projectileSpeed": return (float) playerStats.projectileSpeed;
            case "projectileHits": return (float) playerStats.projectileHits;
            case "attackDelay": return (float) playerStats.attackDelay;
            case "moveSpeed": return (float) playerStats.movementSpeed;
            case "numberOfProjectiles": return (float) playerStats.numberOfProjectiles;
        }
        return 0f;
    }

    private void createUpgradePool()
    {
        if (playerStats.lvl==1)
        {
            leftButton.name = "meleeWeapon";
            rightButton.name = "rangedWeapon";
            leftButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "melee";
            rightButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "ranged";
            return;
        }

        List<string> upgradePool = new List<string>();

        foreach (var stat in upgrades)
        {
            string statName = stat.Key;
            float statVal = getStat(statName);
            Dictionary<string, float> statDictionary = upgrades[statName];

            // filter upgrades by weapon type
            if (statDictionary.ContainsKey("type"))
            {
                if (statDictionary["type"]==1)
                {
                    // ranged only stat
                    if(playerStats.hasRanged==0) {continue;}
                }
                else if (statDictionary["type"]==2)
                {
                    // melee only stat
                    if(playerStats.hasRanged==0) {continue;}
                }
            }

            bool increasing =  floatToBool(statDictionary["increasing"]);
            float statCapVal = statDictionary["capValue"];
            print(statName + " " + statVal + " cap:" + statCapVal + " t:" + increasing);
            if (increasing)
            {
                if (statVal == statCapVal) {continue;} // reached cap (increasing), skips
            } else {}
            {
                if (statVal == statCapVal) {continue;} // reached cap (decreasing), skips
            }
            upgradePool.Add(statName);
        }

        if (upgradePool.Count < 2) {CloseUpgrades(false); return;}; // tell level system character maxed (?)

        int r1 = Random.Range(0,upgradePool.Count);
        string stat1Name = upgradePool[r1];
        upgradePool.RemoveAt(r1);

        int r2 = Random.Range(0,upgradePool.Count);
        string stat2Name = upgradePool[r2];
        upgradePool.RemoveAt(r2);

        leftButton.name = stat1Name;
        rightButton.name = stat2Name;

        // could be replaced with set sprite from dictionary !
        leftButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = upgradeDescriptions[stat1Name];
        rightButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = upgradeDescriptions[stat2Name];
    }

    public bool floatToBool(float val)
    {
        // 0=false, otherwise true
        return val==0f? false : true;
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
        createUpgradePool();

        selectedButton=null;
        setButtonBg(selectButton, COLOR_LOCKED);
        cursor.GetComponent<CursorManager>().setActiveCursor(true);
    }

    public void CloseUpgrades(bool val = true)
    {
        if(selectedButton==null && val) {return;}
        resetSelection();
        Time.timeScale = 1;
        setVisible(false);
        inUpgrades=false;
        cursor.GetComponent<CursorManager>().setActiveCursor(false);
        if (val) {Upgrade();}
    }

    public void Upgrade()
    {
        print("upgrade()");
        string stat = selectedButton.name;
        setStat(stat, calcNewValue(stat));
    }

    private float calcNewValue(string statName) {
        if (statName.Contains("Weapon")) {return 0f;}
        print("calcNewValue()");
        Dictionary<string, float> statDictionary = upgrades[statName];
        float statVal = getStat(statName);
        float multiplier = statDictionary["multiplier"];
        float offset = statDictionary["offset"];
        float newVal = statVal*multiplier + offset;
        print(statName + ", " + statVal + ", " + newVal);
        bool increasing = floatToBool(statDictionary["increasing"]);
        float statCapVal = statDictionary["capValue"];
        if (increasing)
        {
            newVal = Mathf.Min(newVal, statCapVal);
        } else
        {
            newVal = Mathf.Max(newVal, statCapVal);
        }
        return newVal;
    }

}