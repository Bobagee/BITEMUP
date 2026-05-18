using UnityEngine;
using System.Collections.Generic;

public class HitboxPlayer : MonoBehaviour
{
    public int dano = 1;
    public int danoFuerte = 3;

    public bool golpeFuerte;

    public float stunBloqueoRoto = 0.8f;

    private BoxCollider2D col;

    private List<EnemyController> enemigosGolpeados =
        new List<EnemyController>();

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void Activar(bool esGolpeFuerte)
    {
        golpeFuerte = esGolpeFuerte;
        enemigosGolpeados.Clear();
        col.enabled = true;
    }

    public void Desactivar()
    {
        col.enabled = false;
        golpeFuerte = false;
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

            if (enemigo != null && !enemigosGolpeados.Contains(enemigo))
            {
                enemigosGolpeados.Add(enemigo);

                Bloqueo bloqueo =
                    other.GetComponent<Bloqueo>();

                if (bloqueo != null && bloqueo.EstaBloqueando())
                {
                    if (golpeFuerte)
                    {
                        Debug.Log("ROMPISTE EL BLOQUEO");

                        bloqueo.RomperBloqueo();

                        enemigo.AplicarStun(stunBloqueoRoto);

                        enemigo.RecibirGolpe(danoFuerte);
                    }
                    else
                    {
                        Debug.Log("Golpe bloqueado");
                        return;
                    }
                }
                else
                {
                    if (golpeFuerte)
                    {
                        Debug.Log("GOLPE FUERTE");

                        enemigo.RecibirGolpe(danoFuerte);
                    }
                    else
                    {
                        Debug.Log("Golpe normal");

                        enemigo.RecibirGolpe(dano);

                        EnemyDefense defensa =
                            other.GetComponent<EnemyDefense>();

                        if (defensa != null)
                        {
                            defensa.RecibirGolpeNormal();
                        }
                    }
                }

                Knockback knock =
                    other.GetComponent<Knockback>();

                if (knock != null)
                {
                    Vector2 direccion =
                        (
                            other.transform.position -
                            transform.position
                        ).normalized;

                    if (golpeFuerte)
                    {
                        knock.fuerzaGolpe = 6f;
                    }
                    else
                    {
                        knock.fuerzaGolpe = 3.5f;
                    }

                    knock.AplicarGolpe(-direccion);
                }

                Debug.Log("Golpe conectado");
            }
        }
    }
}