using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    public float forceMultiplier = 10f;
    public float maximumVelocity = 3f;
    public float maximumXValue = 5;
    public ParticleSystem deathParticles;

    public GameObject mainVcam;
    public GameObject zoomVcam;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance == null)
            return;

        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
        {
            if ((rb.position[0] <= -maximumXValue && horizontalInput < 0) || (rb.position[0] >= maximumXValue && horizontalInput > 0)) return;
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();

            mainVcam.SetActive(false);
            zoomVcam.SetActive(true);
        }
    }


}
