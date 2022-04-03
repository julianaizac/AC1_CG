using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float forceMultiplier = 10f;
    public float maximumVelocity = 3f;
    public float maximumXValue = 5;
    public ParticleSystem deathParticles;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
        {
            if ((rb.position[0] <= -maximumXValue && horizontalInput < 0) || (rb.position[0] >= maximumXValue && horizontalInput > 0)) return;
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier, 0, 0));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
