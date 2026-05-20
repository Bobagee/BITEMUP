using UnityEngine;
using System.Collections;

public class PersonajeMov : MonoBehaviour
{
    public bool recoverySlam;
    public float tiempoRecoverySlam = 0.32f;

    public Animator animator;


    public PlayerPowerUp powerUp;


    // COMBO
    public int comboActual = 0;
    public float tiempoMaxCombo = 0.6f;
    private float ultimoGolpeCombo;

    private PlayerHealth playerHealth;

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

    // ATAQUE AEREO FUERTE
    public bool golpeAereoFuerte;
    public float fuerzaCaidaAtaqueFuerte = 18f;

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

    public Camera camara;
    public BoxCollider2D zonaMovimiento;

    public HitboxPlayer hitbox;
    public Bloqueo bloqueo;

    public HitboxPlayer hitboxAereaFuerte;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        bloqueo = GetComponent<Bloqueo>();
        ypos_piso = transform.position.y;
        playerHealth = GetComponent<PlayerHealth>();
        powerUp = GetComponent<PlayerPowerUp>();
        animator = GetComponent<Animator>();
    }

    public void Movimiento()
    {


        if (playerHealth != null && playerHealth.stuneado)
        {
            return;
        }

        if (recoverySlam)
        {
            return;
        }
        float actual_speed;

        if (corriendo)
        {
            actual_speed = speed_run;
        }
        else
        {
            actual_speed = speed_char;
        }

        if (powerUp != null && powerUp.transformado)
        {
            actual_speed = speed_char * 0.45f;
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
        if (powerUp != null && powerUp.transformado)
        {
            corriendo = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - ultimoD < doubleTapTime)
            {
                corriendo = true;
                direccion = 1;
            }

            ultimoD = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - ultimoA < doubleTapTime)
            {
                corriendo = true;
                direccion = -1;
            }

            ultimoA = Time.time;
        }
    }

    public void Salto()
    {
        if (powerUp != null && powerUp.transformado)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !saltando && inFloor)
        {
            ypos_piso = transform.position.y;
            saltando = true;
            inFloor = false;

            gravity = altura_salto;
            fase_salto = 1;

            if (animator != null)
            {
                animator.SetBool("isJumping", true);
                
                animator.SetBool("isSlam", false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && saltando && fase_salto == 1 && !golpeAereoFuerte)
        {
            if (gravity > 0)
            {
                gravity *= 0.3f;
            }
        }

        if (saltando)
        {
            if (golpeAereoFuerte)
            {
                transform.Translate(Vector3.down * fuerzaCaidaAtaqueFuerte * Time.deltaTime);

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
                    golpeAereoFuerte = false;
                    air_attack = false;
                    AplicarRecoverySlam();

                    if (animator != null)
                    {
                        animator.SetBool("isSlam", false);
                        animator.SetBool("isJumping", false);
                        animator.SetBool("isFalling", false);
                    }
                }

                return;
            }

            switch (fase_salto)
            {
                case 1:
                    transform.Translate(Vector3.up * gravity * Time.deltaTime);
                    gravity -= potencia_salto * Time.deltaTime;

                    if (gravity <= 0)
                    {
                        fase_salto = 2;

                        if (animator != null)
                        {
                            animator.SetBool("isJumping", false);
                            animator.SetBool("isFalling", true);
                        }
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

    if (animator != null)
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isSlam", false);
    }
}
                    break;
            }
        }
    
}

    public void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!saltando && inFloor)
            {
                if (powerUp != null && powerUp.transformado)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("powerAttack");
                    }

                    Debug.Log("POWER ATTACK");

                    StartCoroutine(ActivarHitbox(true, 0));

                    return;
                }

                ActualizarCombo();

                if (animator != null)
                {
                    animator.SetTrigger("attack1");
                }

                Debug.Log("Golpe combo " + comboActual);

                StartCoroutine(ActivarHitbox(false, comboActual));
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (powerUp != null && powerUp.transformado)
            {
                Debug.Log("Ataque power up");

                StartCoroutine(ActivarHitbox(true, 0));

                return;
            }
            if (saltando || Input.GetKey(KeyCode.Space))
            {
                Debug.Log("ATAQUE FUERTE AEREO TIPO IRON MAN");

                saltando = true;
                inFloor = false;

                golpeAereoFuerte = true;
                air_attack = true;
                fase_salto = 2;
                gravity = 0;

                if (animator != null)
                {
                    animator.ResetTrigger("attack1");

                    animator.SetBool("isSlam", true);
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFalling", false);

                    animator.Play("Slam", 0, 0f);
                }

                StartCoroutine(ActivarHitboxAereaFuerte());
            }
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("strongAttack");
                }
                Debug.Log("ATAQUE FUERTE");

                comboActual = 0;

                StartCoroutine(ActivarHitbox(true, 0));
            }
        }
    }

    void ActualizarCombo()
    {
        if (Time.time - ultimoGolpeCombo <= tiempoMaxCombo)
        {
            comboActual++;
        }
        else
        {
            comboActual = 1;
        }

        if (comboActual > 3)
        {
            comboActual = 1;
        }

        ultimoGolpeCombo = Time.time;
    }

    IEnumerator ActivarHitbox(bool golpeFuerte, int combo)
    {
        if (hitbox == null)
        {
            Debug.LogWarning("No asignaste la hitbox en PersonajeMov");
            yield break;
        }

        hitbox.Activar(golpeFuerte, combo);

        yield return new WaitForSeconds(0.2f);

        hitbox.Desactivar();
    }

    IEnumerator ActivarHitboxAereaFuerte()
    {
        if (hitboxAereaFuerte == null)
        {
            Debug.LogWarning("No asignaste la hitbox aérea fuerte");
            yield break;
        }

        hitboxAereaFuerte.Activar(true, 0);

        yield return new WaitForSeconds(0.3f);

        hitboxAereaFuerte.Desactivar();
    }

    public void Bloquear()
    {
        Debug.Log("Entrando a Bloquear");

        if (powerUp != null && powerUp.transformado)
        {
            Debug.Log("No bloquea porque esta transformado");
            return;
        }

        if (playerHealth != null && playerHealth.stuneado)
        {
            Debug.Log("No bloquea porque esta stuneado");
            return;
        }

        if (bloqueo == null)
        {
            Debug.Log("No hay componente Bloqueo");
            return;
        }

        if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("Presionando L");

            bloqueo.ActivarBloqueo();

            if (animator != null)
            {
                animator.SetBool("isBlocking", true);
                Debug.Log("Animator isBlocking TRUE");
            }
        }
        else
        {
            bloqueo.DesactivarBloqueo();

            if (animator != null)
            {
                animator.SetBool("isBlocking", false);
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

        if (camara != null)
        {
            Transform camTransform = camara.transform;

            if (camara.transform.parent != null)
            {
                camTransform = camara.transform.parent;
            }

            Camarahhh scriptCamara = camTransform.GetComponent<Camarahhh>();

            if (scriptCamara != null && scriptCamara.camaraBloqueada)
            {
                float mitadPantalla = camara.orthographicSize * camara.aspect;

                float bordeIzquierdo = camTransform.position.x - mitadPantalla;
                float bordeDerecho = camTransform.position.x + mitadPantalla;

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

    public void ActualizarAnimacionMovimiento()
    {
        bool moviendo = false;

        if (!saltando && !ground_attack)
        {
            if (
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D)
            )
            {
                moviendo = true;
            }
        }

      
        if (saltando && !golpeAereoFuerte)
        {
            if (gravity > 0)
            {
                saltando = true;
            }
        }

        if (animator != null)

        {

            animator.SetBool("isMoving", moviendo);
            animator.SetBool("isRunning", corriendo && !saltando);

       
            
            animator.SetBool("isSlam", golpeAereoFuerte);

            if (powerUp != null)
            {
                animator.SetBool("isPowerUp", powerUp.transformado);
            }
        }
    }

    public void Terminar_Ani()
    {
        ground_attack = false;
        air_attack = false;
    }
    public void AplicarRecoverySlam()
    {
        recoverySlam = true;

        CancelInvoke("QuitarRecoverySlam");
        Invoke("QuitarRecoverySlam", tiempoRecoverySlam);
    }

    void QuitarRecoverySlam()
    {
        recoverySlam = false;
    }
    void Update()
    {
        DoubleTap();
        Movimiento();
        Salto();
        DetectarSuelo();
        LimitarMovimiento();
        Ataque();
        Bloquear();
        ActualizarAnimacionMovimiento();
    }
}