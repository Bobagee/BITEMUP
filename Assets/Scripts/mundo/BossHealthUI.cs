using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public EnemyController boss;
    public Slider barraVida;
    public GameObject panelBoss;

    void Update()
    {
        if (boss == null)
        {
            if (panelBoss != null)
            {
                panelBoss.SetActive(false);
            }

            return;
        }

        if (panelBoss != null)
        {
            panelBoss.SetActive(true);
        }

        barraVida.maxValue = boss.vidaMaxima;
        barraVida.value = boss.vida;
    }
}