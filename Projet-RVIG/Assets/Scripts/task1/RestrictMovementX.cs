using UnityEngine;

public class RestrictMovementX : MonoBehaviour
{
    [SerializeField] private Transform minX; // Borne minimale sur X
    [SerializeField] private Transform maxX;  // Borne maximale sur X

    [SerializeField] private Rigidbody rb;
    
    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Sauvegarde la position initiale sur Y et Z
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        // Récupère la position actuelle et applique les limites
        float x = transform.position.x;
        if(transform.position.x < minX.position.x) x = minX.position.x;
        else if (transform.position.x > maxX.position.x) x = maxX.position.x;
        //float clampedX = Mathf.Clamp(transform.position.x, minX.position.x, maxX.position.x);

        // Garde Y et Z fixes, permet seulement X de changer dans les bornes
        transform.position = new Vector3(x, fixedY, fixedZ);
        //rb.velocity = Vector3.zero;
    }

    public void StopMomentum()
    {
        rb.velocity = Vector3.zero;
    }
}
