using UnityEngine;

public class HitboxPlayer : MonoBehaviour
{
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemigo = other.GetComponent<EnemyController>();

            if (enemigo != null)
            {
                enemigo.RecibirGolpe(dano);
            }
        }
    }
}

