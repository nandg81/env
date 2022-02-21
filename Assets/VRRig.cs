using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

using UnityEngine;[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public Transform HeadConstraint;
    public Vector3 headBodyOffset; // Start is called before the first frame update
    private PhotonView photonView;

    public float turnSmoothness;

    void Start()
    {
        XRRig rig = FindObjectOfType<XRRig>();
        photonView = GetComponent<PhotonView>();

        head.vrTarget = rig.transform.Find("Camera Offset/Main Camera");
        leftHand.vrTarget = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHand.vrTarget = rig.transform.Find("Camera Offset/RightHand Controller");
        headBodyOffset = transform.position - HeadConstraint.position;
    } // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            transform.position = HeadConstraint.position + headBodyOffset;
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(HeadConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
            head.Map();
            leftHand.Map();
            rightHand.Map();
        }
    }
}

