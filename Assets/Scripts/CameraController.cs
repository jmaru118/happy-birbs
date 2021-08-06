using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator animator;             // used in the initial camera animation

    private float timeElapsed;                              // used for initial camera animation
    private bool enemyCamIsActive = true;                   // lets us know when the initial animation is over
    private CinemachineVirtualCamera vcam;                  // our player camera object we want to control       


    public void SetBirdToFollow(Transform birdTransform)
    {
        vcam.Follow = birdTransform;
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
    }
}
