using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold=0.1f;
    public Animator animator;
    private Vector3 previousPos;
    public VRRig vrRig;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        //vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        animator.SetBool("IsMoving", headsetLocalSpeed.magnitude > speedTreshold);
    }
}
