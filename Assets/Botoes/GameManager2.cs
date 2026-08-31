using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public void ComecarJogo()
    {
        int sorteio = Random.Range(0, 100);

        if (sorteio < 70)
        {
            // 70% de chance
            SceneManager.LoadScene("SampleScene");
        }
        else if (sorteio < 95)
        {
            // 25% de chance
            SceneManager.LoadScene("SampleScene2");
        }
    }
}
