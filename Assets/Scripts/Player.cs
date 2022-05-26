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

    //public AudioClip audioClip;
    //public AudioController audioController;

    public AudioSource audioSourceExplosao;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    // Start is called before the first frame update
    void Awake()
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
            //audioSourceExplosao.Play();
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
        }
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, 0.75f, 0);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }


}
