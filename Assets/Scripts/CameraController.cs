using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator animator;             // used in the initial camera animation
    [SerializeField] private float camBuffer;               // controls delay before camera pans to follow bird
    [SerializeField] private float camSpeed;                // multiplier to control how fast the camera pans
    [SerializeField] private float camStopPoint;            // the farthest right the camera can pan before stopping

    private float timeElapsed;                              // used for initial camera animation
    private bool enemyCamIsActive = true;                   // lets us know when the initial animation is over
    private CinemachineVirtualCamera vcam;                  // our player camera object we want to control       
    private Transform activeBirdTransform;                  // used to locate the active bird


    public void SetBirdToFollow(Transform birdTransform)
    {
        //vcam = FindObjectOfType<CinemachineVirtualCamera>();
        activeBirdTransform = birdTransform;
        //vcam.LookAt = birdTransform;
        //vcam.Follow = birdTransform;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > 2 && enemyCamIsActive)
        {
            animator.Play("Player Camera");
            enemyCamIsActive = false;
        }

        // follow our bird's x position
        if (!enemyCamIsActive)
        {
            Debug.Log(Time.deltaTime);
            // move camera left to follow our bird
            if (activeBirdTransform.position.x < transform.position.x - camBuffer)
            {
                transform.position = new Vector3(transform.position.x - Time.deltaTime*camSpeed,
                                     -2.88f, transform.position.z);
            }

            // move camera right to follow our bird
            if (activeBirdTransform.position.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime*camSpeed,
                                     -2.88f, transform.position.z);
            }

            // don't let the camera pan too far right
            if (transform.position.x > camStopPoint)
                transform.position = new Vector3(camStopPoint, -2.88f, transform.position.z);
        }
    }
}
