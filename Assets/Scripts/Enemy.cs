using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ghostParticle;
    [SerializeField] private float deathForce;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory enemy if force excees predetermined value
        if (collision.relativeVelocity.magnitude > deathForce)
        {
            // create a ghost at current location
            Instantiate(ghostParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // destory enemy if something drops on his head
        if (collision.contacts[0].normal.y < -0.65)
        {
            // create a ghost at current location
            Instantiate(ghostParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
