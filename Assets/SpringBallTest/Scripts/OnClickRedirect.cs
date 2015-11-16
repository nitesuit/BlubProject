using UnityEngine;
using System.Collections;

public class OnClickRedirect : MonoBehaviour {

    void OnMouseDown()
    {
        (GetComponentInParent<PlayerController>()).StartLaunch();
    }

    void OnMouseUp()
    {
        (GetComponentInParent<PlayerController>()).Launch();
    }
}
