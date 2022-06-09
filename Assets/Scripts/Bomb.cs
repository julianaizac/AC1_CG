using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bomb : MonoBehaviour
{
    Vector3 rotation;
    public ParticleSystem breakingEffect;
    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;

    private void Start()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();

        var xRotation = Random.Range(0.5f, 1f);
        rotation = new Vector3(xRotation, 0);
    }

    private void Update()
    {
        transform.Rotate(rotation);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bomb") && !collision.gameObject.CompareTag("Hazard"))
        {

            Destroy(gameObject);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);

            if (player != null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1 / distance;

                cinemachineImpulseSource.GenerateImpulse(force);
            }
        }

    }
}
