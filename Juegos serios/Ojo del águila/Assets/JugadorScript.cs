using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorScript : MonoBehaviour
{

    public Rigidbody rigid;
    public VariableJoystick joystickMovimiento;
    public VariableJoystick joystickRotacion;
    public float velocidadMovimiento = 5f;
    public float suavizadoMovimiento = 0.1f;
    private Vector3 velocidadActual;


    public Transform camara; // Arrastra la c�mara aqu�
    public float velocidadRotacion = 100f;
    public float suavizadoRotacion = 0.1f;
    private float rotacionX; // Rotaci�n vertical (arriba/abajo)
    private float rotacionY; // Rotaci�n horizontal (izquierda/derecha)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 movimiento = new Vector3(joystick.Horizontal, 0f, joystick.Vertical) * velocidadMovimiento * Time.deltaTime;
        //transform.Translate(movimiento, Space.World);

        // --- MOVIMIENTO DE LA C�PSULA ---
        Vector2 inputMovimiento = joystickMovimiento.Direction;
        Vector3 direccion = new Vector3(inputMovimiento.x, 0f, inputMovimiento.y).normalized;

        // Mover suavemente (opcional)
        Vector3 movimiento = direccion * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        // Rotar la c�psula en la direcci�n del movimiento (opcional)
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, suavizadoMovimiento);
        }

        // --- ROTACI�N DE C�MARA ---
        Vector2 inputRotacion = joystickRotacion.Direction;

        rotacionY += inputRotacion.x * velocidadRotacion * Time.deltaTime;
        rotacionX -= inputRotacion.y * velocidadRotacion * Time.deltaTime;
        rotacionX = Mathf.Clamp(rotacionX, -30f, 30f); // Limitar �ngulo vertical

        // Aplicar rotaci�n a la c�mara
        Quaternion rotacionCamara = Quaternion.Euler(rotacionX, rotacionY, 0f);
        camara.rotation = Quaternion.Slerp(camara.rotation, rotacionCamara, suavizadoRotacion);

        // La c�mara sigue al jugador
        camara.position = transform.position + Vector3.up * 1.5f; // Ajusta la altura seg�n necesites


    }
}
