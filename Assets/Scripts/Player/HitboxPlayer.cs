using UnityEngine;
using System.Collections.Generic;

public class HitboxPlayer : MonoBehaviour
{
    public int dano = 1;

    private BoxCollider2D col;

    private List<EnemyController> enemigosGolpeados =
        new List<EnemyController>();

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();

        col.enabled = false;
    }

    public void Activar()
    {
        enemigosGolpeados.Clear();

        col.enabled = true;
    }

    public void Desactivar()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RevisarGolpe(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        RevisarGolpe(other);
    }

    void RevisarGolpe(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemigo =
                other.GetComponent<EnemyController>();

            if (
                enemigo != null &&
                !enemigosGolpeados.Contains(enemigo)
            )
            {
                enemigosGolpeados.Add(enemigo);

                enemigo.RecibirGolpe(dano);

                Knockback knock =
                    other.GetComponent<Knockback>();

                if (knock != null)
                {
                    Vector2 direccion =
                        (
                            other.transform.position -
                            transform.position
                        ).normalized;

                    knock.AplicarGolpe(direccion);
                }

                Debug.Log("Golpe conectado");
            }
        }
    }
}