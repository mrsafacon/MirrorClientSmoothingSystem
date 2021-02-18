using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInterpolation : MonoBehaviour
{
    Transform mainCharTransform;
    Vector3 netPosition;

    static readonly float SNAP_DISTANCE = 1.4f;
    static readonly float CLOSE_ENOUGH_DISTANCE = .14f;


    public void Init(Transform mct) {
        mainCharTransform = mct;
        netPosition = mct.position;
    }

    public void UpdateNetPosition(Vector3 np) {
        netPosition = np;
    }

    public Vector3 GetInterpolatedPos() {
        float distance = Vector3.Distance(netPosition, mainCharTransform.position);
        if (distance > SNAP_DISTANCE) { //snap if we are way out of sync with the server
            Debug.Log("snap");
            return netPosition;
        } else if (distance < CLOSE_ENOUGH_DISTANCE) return mainCharTransform.position; //no lerping needed if we are in sync
        else {
            Vector3 interpolated = Vector3.Lerp(mainCharTransform.position, netPosition, .05f); //slide closer to position
            return interpolated;
        }
    }
}
