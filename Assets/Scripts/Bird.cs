using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 initPos;                     // initial position of bird
    private bool hasLaunched;                       // used with _idleTime
    private float idleTime;                         // used to reset scene if bird sits idle for too long

    [SerializeField] float gravity;                 // affects gravity on bird
    [SerializeField] float launchMultiplier = 300;  // multiplier affects launch speed
    [SerializeField] private float maxPullDistX;    // limits how far we can pull our bird in the X dir
    [SerializeField] private float maxPullDistY;    // limits how far we can pull our bird in the Y dir


    private void Awake()
    {
        initPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, initPos);

        // Conditions for scene reset
        // start timer for timeout
        if ( hasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.2)
        {
            idleTime += Time.deltaTime;
        }

        // check bounds for reset
        if (transform.position.y > 10  ||
            transform.position.y < -10 ||
            transform.position.x > 15  ||
            transform.position.x < -20 ||
            idleTime > 2.5 )
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

            
    }

    private void OnMouseDown()
    {
        GetComponent<LineRenderer>().enabled = true;
        
    }

    private void OnMouseUp()
    {
        // calc launch vector and launch bird
        Vector2 dirToFly = initPos - transform.position;
        Rigidbody2D bird_rigidbody2d = GetComponent<Rigidbody2D>();
        bird_rigidbody2d.AddForce(dirToFly * launchMultiplier);
        bird_rigidbody2d.gravityScale = gravity;
        hasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y);

        // Check if our bird is attempting to pull out of bounds
        if (initPos.x - transform.position.x > maxPullDistX)
        {
            transform.position = new Vector3(initPos.x - maxPullDistX, transform.position.y);
        }

        if (transform.position.x > initPos.x)
        {
            transform.position = new Vector3(initPos.x, transform.position.y);
        }

        if (initPos.y - transform.position.y > maxPullDistY)
        {
            transform.position = new Vector3(transform.position.x, initPos.y - maxPullDistY);
        }

        if (initPos.y - transform.position.y < -maxPullDistY)
        {
            transform.position = new Vector3(transform.position.x, initPos.y + maxPullDistY);
        }
    }
    
}
