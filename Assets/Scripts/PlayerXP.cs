using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int xp = 0;
    public int level = 1;

    public int xpToNextLevel = 50;
    public int xpIncreasePerLevel = 25;

    public XPBar xpBar;

    public LevelUpManager levelUpManager;

    void Start()
    {
        xpBar.SetMaxXP(xpToNextLevel);
        xpBar.SetXP(xp);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("XP"))
        {
            XPGem gem = other.GetComponent<XPGem>();

            if (gem == null)
                return;

            xp += gem.xpValue;

            Destroy(other.gameObject);

            Debug.Log("XP: " + xp + "/" + xpToNextLevel);

            while (xp >= xpToNextLevel)
            {
                xp -= xpToNextLevel;

                LevelUp();
            }

            xpBar.SetXP(xp);
        }
    }

    void LevelUp()
    {
        level++;

        xpToNextLevel += xpIncreasePerLevel;

        xpBar.SetMaxXP(xpToNextLevel);
        xpBar.SetXP(xp);

        Debug.Log("LEVEL UP! Nível: " + level);

        // Abre a tela de melhorias
        if (levelUpManager != null)
        {
            levelUpManager.ShowLevelUp();
        }
    }
}