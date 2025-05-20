using UnityEngine;

public class ClickMover3D : MonoBehaviour
{
    private Camera cam;
   
    private Vector3 targetPosition;
    private bool moving = false;
    private int etapa = 1; // ← Para controlar el orden

    public float moveSpeed = 5f;

    void Start()
    {
        cam = Camera.main;
        targetPosition = new Vector3(3.48f, 0.52f, 3.07f); // Capsule-1
        transform.position = targetPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);

                if (hit.transform.name == "Capsule-2" && etapa == 1)
                {
                    targetPosition = new Vector3(-4.32f, 1.01f, 4.02f);
                    moving = true;
                    etapa = 2;
                    Debug.Log("Ir a Capsule-2");
                }
                else if (hit.transform.name == "Capsule-3" && etapa == 2)
                {
                    targetPosition = new Vector3(-2.395756f, 1.033969f, -3.98f);
                    moving = true;
                    etapa = 3;
                    Debug.Log("Ir a Capsule-3");
                }
                else if (hit.transform.name == "Capsule-4" && etapa == 3)
                {
                    targetPosition = new Vector3(4.39f, 0.73f, -4.451383f);
                    moving = true;
                    etapa = 4;
                    Debug.Log("Ir a Capsule-4");
                }
                else if (hit.collider.CompareTag("Ground") && etapa > 1) // permitir clic libre después de la secuencia
                {
                    targetPosition = hit.point;
                    moving = true;
                    Debug.Log("Mover a punto libre en el suelo");
                }
                else
                {
                    Debug.Log("No puedes ir allí todavía. Etapa actual: " + etapa);
                }
            }
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 1.5f)
            {
                moving = false;
                Debug.Log("Movimiento terminado");
            }
        }
    }
}
