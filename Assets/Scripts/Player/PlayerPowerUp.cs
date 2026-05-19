using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public int energiaActual;
    public int energiaNecesaria = 100;

    public bool transformado;

    public float duracionPowerUp = 30f;
    public float tiempoRestante;

    public float escalaPowerUp = 1.6f;

    private Vector3 escalaOriginal;

    private Bloqueo bloqueo;

    void Start()
    {
        escalaOriginal = transform.localScale;
        bloqueo = GetComponent<Bloqueo>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            IntentarActivarPowerUp();
        }

        if (transformado)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                TerminarPowerUp();
            }
        }
    }

    public void GanarEnergia(int cantidad)
    {
        if (transformado)
        {
            return;
        }

        energiaActual += cantidad;

        if (energiaActual > energiaNecesaria)
        {
            energiaActual = energiaNecesaria;
        }

        Debug.Log("Energia PowerUp: " + energiaActual + "/" + energiaNecesaria);
    }

    public void IntentarActivarPowerUp()
    {
        if (transformado)
        {
            return;
        }

        if (energiaActual >= energiaNecesaria)
        {
            ActivarPowerUp();
        }
        else
        {
            Debug.Log("No tienes energia suficiente para el powerup");
        }
    }

    void ActivarPowerUp()
    {
        transformado = true;
        energiaActual = 0;
        tiempoRestante = duracionPowerUp;

        transform.localScale = escalaOriginal * escalaPowerUp;

        if (bloqueo != null)
        {
            bloqueo.DesactivarBloqueo();
        }

        Debug.Log("POWER UP ACTIVADO");
    }

    public void ReducirTiempoPorDaño(int dano)
    {
        if (!transformado)
        {
            return;
        }

        float reduccion = dano * 5f;

        tiempoRestante -= reduccion;

        Debug.Log("PowerUp reducido por daño. Tiempo restante: " + tiempoRestante);

        if (tiempoRestante <= 0)
        {
            TerminarPowerUp();
        }
    }

    void TerminarPowerUp()
    {
        transformado = false;
        tiempoRestante = 0;

        transform.localScale = escalaOriginal;

        Debug.Log("POWER UP TERMINADO");
    }
}