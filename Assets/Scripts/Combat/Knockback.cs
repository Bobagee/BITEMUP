using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float fuerzaGolpe = 3f;
    public float duracionGolpe = 0.15f;

    public float tiempoParaCombo = 0.6f;
    public float multiplicadorCombo = 0.35f;
    public int maxCombo = 4;

    private bool siendoGolpeado;
    private Vector2 direccionGolpe;

    private int contadorCombo;
    private float ultimoGolpe;

    void Update()
    {
        if (siendoGolpeado)
        {
            transform.position +=(Vector3)(-direccionGolpe * fuerzaGolpe * Time.deltaTime);
        }
    }

    public void AplicarGolpe(Vector2 direccion)
    {
        if (Time.time - ultimoGolpe <= tiempoParaCombo)
        {
            contadorCombo++;
        }
        else
        {
            contadorCombo = 1;
        }

        if (contadorCombo > maxCombo)
        {
            contadorCombo = maxCombo;
        }

        ultimoGolpe = Time.time;

        direccionGolpe = direccion.normalized;
        siendoGolpeado = true;

        CancelInvoke("TerminarGolpe");
        Invoke("TerminarGolpe", duracionGolpe);
    }

    float CalcularMultiplicador()
    {
        return 1f + ((contadorCombo - 1) * multiplicadorCombo);
    }

    void TerminarGolpe()
    {
        siendoGolpeado = false;
    }
}