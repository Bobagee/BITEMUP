using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class PersonajeMov : MonoBehaviour
{
    public float speed_char = 5f;
    public bool ground_attack;
    public bool air_attack;

    private float gravity;
    private float ypos;
    private float ypos_piso;

    public bool inFloor = true;
    public bool saltando;
    public int fase_salto;

    public float altura_salto = 5f;
    public float potencia_salto = 8f;
    public float caida = 10f;

    // RUN
    public float speed_run = 10f;
    public float doubleTapTime = 0.3f;

    public float ultimoD;
    public float ultimoA;

    public bool corriendo;
    private int direccion;

    public SpriteRenderer sprite;
    public float delay;
    private int subir_caer;

    // ZONA DE MOVIMIENTO
    public BoxCollider2D zonaMovimiento;

    //camarah
    public Camera camara;

    public HitboxPlayer hitbox;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        ypos_piso = transform.position.y;
    }

    public void Movimiento()
    {
        float actual_speed;

        if (corriendo)
        {
            actual_speed = speed_run;
        }
        else
        {
            actual_speed = speed_char;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            corriendo = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direccion = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direccion = -1;
        }

        if (Input.GetKey(KeyCode.D) && !ground_attack)
        {
            transform.Translate(Vector3.right * actual_speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A) && !ground_attack)
        {
            transform.Translate(Vector3.left * -actual_speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.W) && !ground_attack && !saltando && inFloor)
        {
            transform.Translate(Vector3.up * speed_char * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && !ground_attack && !saltando && inFloor)
        {
            transform.Translate(Vector3.down * speed_char * Time.deltaTime);
        }
    }

    public void DoubleTap()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - ultimoD < doubleTapTime)
            {
                corriendo = true;
                direccion = 1;
                Debug.Log("Estas corriendo a la derecha bro");
            }

            ultimoD = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - ultimoA < doubleTapTime)
            {
                corriendo = true;
                direccion = -1;
                Debug.Log("Estas corriendo a la izquierda bro");
            }

            ultimoA = Time.time;
        }
    }

    public void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !saltando && inFloor)
        {
            ypos_piso = transform.position.y;
            saltando = true;
            inFloor = false;
            fase_salto = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && saltando && fase_salto == 1)
        {
            if (gravity > 0)
            {
                gravity *= 0.3f;
            }
        }

        if (saltando)
        {
            switch (fase_salto)
            {
                case 0:
                    gravity = altura_salto;
                    fase_salto = 1;
                    break;

                case 1:
                    transform.Translate(Vector3.up * gravity * Time.deltaTime);
                    gravity -= potencia_salto * Time.deltaTime;

                    if (gravity <= 0)
                    {
                        fase_salto = 2;
                    }
                    break;

                case 2:
                    gravity += caida * Time.deltaTime;
                    transform.Translate(Vector3.down * gravity * Time.deltaTime);

                    if (transform.position.y <= ypos_piso)
                    {
                        transform.position = new Vector3(
                            transform.position.x,
                            ypos_piso,
                            transform.position.z
                        );

                        saltando = false;
                        inFloor = true;
                        gravity = 0;
                        fase_salto = 0;
                    }
                    break;
            }
        }
    }

    public void DetectarSuelo()
    {
        ypos = transform.position.y;

        if (!saltando && !ground_attack)
        {
            subir_caer = 0;

            if (Mathf.Abs(ypos - ypos_piso) < 0.01f)
            {
                inFloor = true;
            }
        }
    }

    public void LimitarMovimiento()
    {
        if (zonaMovimiento == null)
        {
            return;
        }

        Bounds limites = zonaMovimiento.bounds;

        float minX = limites.min.x;
        float maxX = limites.max.x;

        // 🔥 SI LA CAMARA ESTA BLOQUEADA
        if (camara != null)
        {
            Camarahhh scriptCamara = camara.GetComponent<Camarahhh>();

            if (scriptCamara != null && scriptCamara.camaraBloqueada)
            {
                float mitadPantalla = camara.orthographicSize * camara.aspect; //Esto da la mitad del ancho visible de la cámara
                //con esto sacamos los bordes 

                float bordeIzquierdo = camara.transform.position.x - mitadPantalla;
                float bordeDerecho = camara.transform.position.x + mitadPantalla;

                minX = Mathf.Max(minX, bordeIzquierdo);
                maxX = Mathf.Min(maxX, bordeDerecho);
            }
        }

        float xLimitada = Mathf.Clamp(
            transform.position.x,
            minX,
            maxX
        );

        float yLimitada = transform.position.y;

        if (!saltando)
        {
            yLimitada = Mathf.Clamp(
                transform.position.y,
                limites.min.y,
                limites.max.y
            );
        }

        transform.position = new Vector3(
            xLimitada,
            yLimitada,
            transform.position.z
        );
    }

    public void Terminar_Ani()
    {
        ground_attack = false;
        air_attack = false;
    }

    public void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(ActivarHitbox());
        }
    }

    IEnumerator ActivarHitbox()
    {
        hitbox.Activar();

        yield return new WaitForSeconds(0.2f);

        hitbox.Desactivar();
    }


    void Update()
    {
        DoubleTap();
        Movimiento();
        Salto();
        DetectarSuelo();
        LimitarMovimiento();
        Ataque();
    }
}