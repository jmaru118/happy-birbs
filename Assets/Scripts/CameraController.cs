using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float timeElapsed;
    private bool enemyCamIsActive = true;
    private CinemachineVirtualCamera vcam;


    public void SetBirdToFollow(Transform birdTransform)
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.LookAt = birdTransform;
        vcam.Follow = birdTransform;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
