using UnityEngine;

public class ControllerTouchTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject controllerCanvas;
    void Start()
    {
        controllerCanvas.SetActive(false);
    }

    System.Collections.IEnumerator FlashObject(MeshRenderer renderer)
    {
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        renderer.material.color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Detecta toques en pantalla (móvil) o clics (editor)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject touchedObject = hit.collider.gameObject;
                if (touchedObject != null)
                {                    
                    gameObject.GetComponent<ControllerMision>().ValTouch(touchedObject);
                }
                
            }
        }

    }
}
