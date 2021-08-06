using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    private bool isActiveBird = false;              // determines if this is the bird we are shooting
    private bool hasLaunched;                       // used with _idleTime
    private float idleTime;                         // used to reset scene if bird sits idle for too long
    private CameraController cameraController;      // used to tell the camera to follow this bird
    private LevelController levelController;        // used for readying up the next bird
    private PolygonCollider2D collider;             // will hold our 2dcollider to enable when bird is active
    private Animator animator;                      // help distinguish when a bird is activate

    [SerializeField] private Vector3 launchPosition;// launch position of bird
    [SerializeField] private Vector3 startPosition; // starting position of bird before it is available for launch
    [SerializeField] float gravity = 0.8f;          // affects gravity on bird
    [SerializeField] float launchMultiplier = 300;  // multiplier affects launch speed
    [SerializeField] private float maxPullDistX;    // limits how far we can pull our bird in the X dir
    [SerializeField] private float maxPullDistY;    // limits how far we can pull our bird in the Y dir

    public void ActivateBird()
    {
        isActiveBird = true;
        transform.position = launchPosition;
        // set which bird the camera should follow 
        cameraController = FindObjectOfType<CameraController>();
        cameraController.SetBirdToFollow(transform);
        collider.enabled = true;
        animator.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // grab the levelcontroller for use when bird is destroyed
        levelController = FindObjectOfType<LevelController>();
        transform.position = startPosition;
        collider = GetComponent<PolygonCollider2D>();
        collider.enabled = false;
        animator = GetComponent<Animator>();
        animator.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveBird)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, launchPosition);

            // Conditions for scene reset
            // start timer for timeout
            if ( hasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.2)
            {
                idleTime += Time.deltaTime;
            }

            // check bounds for reset
            if (transform.position.y < -10 ||
                idleTime > 2.5 )
            {
                levelController.ReadyUpNextBird();
                Destroy(gameObject);
            }
        }

            
    }

    private void OnMouseDown()
    {
        GetComponent<LineRenderer>().enabled = true;
        
    }

    // mouse calls will only work when bird is active
    private void OnMouseUp()
    {
        if (isActiveBird)
        {
            // calc launch vector and launch bird
            Vector2 dirToFly = launchPosition - transform.position;
            Rigidbody2D bird_rigidbody2d = GetComponent<Rigidbody2D>();
            bird_rigidbody2d.AddForce(dirToFly * launchMultiplier);
            bird_rigidbody2d.gravityScale = gravity;
            hasLaunched = true;
            GetComponent<LineRenderer>().enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        if (isActiveBird)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y);

            // Check if our bird is attempting to pull out of bounds
            if (launchPosition.x - transform.position.x > maxPullDistX)
            {
                transform.position = new Vector3(launchPosition.x - maxPullDistX, transform.position.y);
            }

            if (transform.position.x > launchPosition.x)
            {
                transform.position = new Vector3(launchPosition.x, transform.position.y);
            }

            if (launchPosition.y - transform.position.y > maxPullDistY)
            {
                transform.position = new Vector3(transform.position.x, launchPosition.y - maxPullDistY);
            }

            if (launchPosition.y - transform.position.y < -maxPullDistY)
            {
                transform.position = new Vector3(transform.position.x, launchPosition.y + maxPullDistY);
            }
        }
    }
    
}
