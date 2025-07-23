using TMPro;
using UnityEngine;

public class CollisionBarreraM0 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject textDialog;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("NoTerrainCollision")){
            textDialog.GetComponent<TMP_Text>().text = "No puedes avanzar en esta zona hasta finalizar la misión en curso";
            textDialog.SetActive(true);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("NoTerrainCollision"))
        {
            textDialog.SetActive(false);
        }
    }


}
