using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView photonView;
    private Rigidbody rigidbodyObj;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rigidbodyObj = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        rigidbodyObj.isKinematic = true;
        rigidbodyObj.detectCollisions = false;
        base.OnSelectEntered(interactor);
        
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        rigidbodyObj.isKinematic = false;
        rigidbodyObj.detectCollisions = true;
        base.OnSelectExited(interactor);
    }
}
