using UnityEngine;

public class EnemyController: MonoBehaviour
{
    public int vida = 3;

    public void RecibirGolpe(int dano)
    {
        vida -= dano;

        Debug.Log("Vida restante: " + vida);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
