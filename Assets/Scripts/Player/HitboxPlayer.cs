using UnityEngine;
using System.Collections.Generic;

public class HitboxPlayer : MonoBehaviour
{
    public int danoPowerUp = 5;
    public int danoFuertePowerUp = 10;

    private PlayerPowerUp powerUp;

    public int dano = 1;
    public int danoFuerte = 3;

    public bool golpeFuerte;

    public int golpeCombo = 1;

    public float knockGolpe1 = 3.5f;
    public float knockGolpe2 = 4.5f;
    public float knockGolpe3 = 6f;
    public float knockFuerte = 7f;

    public float stunBloqueoRoto = 1f;

    private BoxCollider2D col;

    private List<EnemyController> enemigosGolpeados =
        new List<EnemyController>();

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        powerUp = GetComponentInParent<PlayerPowerUp>();
    }

    public void Activar(bool esGolpeFuerte, int combo)
    {
        golpeFuerte = esGolpeFuerte;
        golpeCombo = combo;

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

                        if (ScreenShake.instance != null)
                        {
                            ScreenShake.instance.Shake(0.18f, 0.12f);
                        }

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

                        if (ScreenShake.instance != null)
                        {
                            ScreenShake.instance.Shake(0.12f, 0.08f);
                        }

                        enemigo.RecibirGolpe(danoFuerte);
                        if (powerUp != null && !powerUp.transformado)
                        {
                            powerUp.GanarEnergia(5);
                        }
                    }
                    else
                    {
                        Debug.Log("Golpe combo " + golpeCombo);
                        enemigo.RecibirGolpe(dano);
                        if (powerUp != null && !powerUp.transformado)
                        {
                            powerUp.GanarEnergia(2);
                        }

                        EnemyDefense defensa =
                            other.GetComponent<EnemyDefense>();

                        if (defensa != null)
                        {
                            defensa.IntentarBloquear();
                        }
                    }
                }

                EnemyKnockback knock =
                    other.GetComponent<EnemyKnockback>();

                if (knock != null)
                {
                    Vector2 direccion =
                        (
                            other.transform.position -
                            transform.position
                        ).normalized;

                    if (golpeFuerte)
                    {
                        knock.fuerzaGolpe = knockFuerte;
                    }
                    else
                    {
                        if (golpeCombo == 1)
                        {
                            knock.fuerzaGolpe = knockGolpe1;
                        }

                        if (golpeCombo == 2)
                        {
                            knock.fuerzaGolpe = knockGolpe2;
                        }

                        if (golpeCombo == 3)
                        {
                            knock.fuerzaGolpe = knockGolpe3;
                        }
                    }

                    knock.AplicarGolpe(direccion);
                }

                Debug.Log("Golpe conectado");
            }
        }
    }
}