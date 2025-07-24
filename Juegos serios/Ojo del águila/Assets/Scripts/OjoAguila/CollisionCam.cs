using UnityEngine;

public class CollisionCam : MonoBehaviour
{

    [SerializeField] GameObject BtnCam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
                BtnCam.SetActive(true);
            
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            
                BtnCam.SetActive(false);
            
        }
    }
}
