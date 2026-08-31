using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelUpManager : MonoBehaviour
{
    public GameObject levelUpPanel;

    [Header("Botões")]
    public Button buttonArma;
    public Button buttonHP;
    public Button buttonVelocidade;

    [Header("Texto dos botões")]
    public TMP_Text textArma;
    public TMP_Text textHP;
    public TMP_Text textVelocidade;

    private PlayerStats playerStats;

    private List<int> escolhas = new List<int>();

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }

        levelUpPanel.SetActive(false);
    }

    public void ShowLevelUp()
    {
        Time.timeScale = 0f;

        levelUpPanel.SetActive(true);

        SortearOpcoes();
    }

    void SortearOpcoes()
    {
        escolhas.Clear();

        // 0 = Vida
        // 1 = Dano
        // 2 = Velocidade

        while (escolhas.Count < 3)
        {
            int escolha = Random.Range(0, 3);

            if (!escolhas.Contains(escolha))
            {
                escolhas.Add(escolha);
            }
        }

        ConfigurarBotao(buttonArma, textArma, escolhas[0]);
        ConfigurarBotao(buttonHP, textHP, escolhas[1]);
        ConfigurarBotao(buttonVelocidade, textVelocidade, escolhas[2]);
    }

    void ConfigurarBotao(Button botao, TMP_Text texto, int escolha)
    {
        botao.onClick.RemoveAllListeners();

        if (escolha == 0)
        {
            texto.text = "❤️ +20 HP";

            botao.onClick.AddListener(EscolherVida);
        }
        else if (escolha == 1)
        {
            texto.text = "⚔️ +5 DANO";

            botao.onClick.AddListener(EscolherDano);
        }
        else if (escolha == 2)
        {
            texto.text = "⚡ +0.5 VELOCIDADE";

            botao.onClick.AddListener(EscolherVelocidade);
        }
    }

    void EscolherVida()
    {
        if (playerStats != null)
        {
            playerStats.AddHealth();
        }

        FecharLevelUp();
    }

    void EscolherDano()
    {
        if (playerStats != null)
        {
            playerStats.AddDamage();
        }

        FecharLevelUp();
    }

    void EscolherVelocidade()
    {
        if (playerStats != null)
        {
            playerStats.AddSpeed();
        }

        FecharLevelUp();
    }

    void FecharLevelUp()
    {
        levelUpPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}