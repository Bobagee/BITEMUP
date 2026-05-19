using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float fuerzaGolpe = 5f;

    public float duracionGolpe = 0.18f;

    public float tiempoParaCombo = 0.5f;

    public float multiplicadorCombo = 0.3f;

    public int maxCombo = 5;

    private Vector2 direccionGolpe;

    private bool recibiendoGolpe;

    private float tiempoGolpe;

    private int comboActual;

    private float tiempoUltimoGolpe;

    void Update()
    {
        if (recibiendoGolpe)
        {
            tiempoGolpe -= Time.deltaTime;

            float fuerzaFinal =
                fuerzaGolpe +
                (comboActual * multiplicadorCombo);

            transform.position +=
                (Vector3)(
                    direccionGolpe *
                    fuerzaFinal *
                    Time.deltaTime
                );

            if (tiempoGolpe <= 0)
            {
                recibiendoGolpe = false;
            }
        }

        if (Time.time - tiempoUltimoGolpe > tiempoParaCombo)
        {
            comboActual = 0;
        }
    }

    public void AplicarGolpe(Vector2 direccion)
    {
        // DISPERSION PARA EVITAR ENEMIGOS ENCIMADOS
        float dispersionX = Random.Range(-0.7f, 0.7f);
        float dispersionY = Random.Range(-0.25f, 0.25f);

        direccion.x += dispersionX;
        direccion.y += dispersionY;

        direccion.Normalize();

        direccionGolpe = direccion;

        recibiendoGolpe = true;

        tiempoGolpe = duracionGolpe;

        if (Time.time - tiempoUltimoGolpe <= tiempoParaCombo)
        {
            comboActual++;

            if (comboActual > maxCombo)
            {
                comboActual = maxCombo;
            }
        }
        else
        {
            comboActual = 1;
        }

        tiempoUltimoGolpe = Time.time;
    }
}