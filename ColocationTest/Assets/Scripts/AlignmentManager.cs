using System;
using System.Collections;
using UnityEngine;

public class AlignmentManager : MonoBehaviour
{
    private Transform cameraRigTransform;
    private void Awake()
    {
        cameraRigTransform = FindAnyObjectByType<OVRCameraRig>().transform;
        
    }

    public void AlignUserToAnchor(OVRSpatialAnchor anchor)
    {
        if (!anchor || !anchor.Localized)
        {
            return;
        }
        
        StartCoroutine(AlignmentCoroutine(anchor));
    }

    private IEnumerator AlignmentCoroutine(OVRSpatialAnchor anchor)
    {
        var anchorTransform = anchor.transform;
        
        for (var alignmentCount = 2; alignmentCount > 0; alignmentCount--)
        {
            cameraRigTransform.position = Vector3.zero;
            cameraRigTransform.eulerAngles = Vector3.zero;

            yield return null;

            cameraRigTransform.position = anchorTransform.InverseTransformPoint(Vector3.zero);
            cameraRigTransform.eulerAngles = new Vector3(0, -anchorTransform.eulerAngles.y, 0);

            yield return new WaitForEndOfFrame();
        }
    }
}
