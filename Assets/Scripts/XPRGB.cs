using UnityEngine;
using UnityEngine.UI;

public class XPRGB : MonoBehaviour
{
    public Image xpBar;
    public float velocidade = 2f;

    void Update()
    {
        float h = Mathf.PingPong(Time.time * velocidade, 1f);

        xpBar.color = Color.HSVToRGB(h, 1f, 1f);
    }
}