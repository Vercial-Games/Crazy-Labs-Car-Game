using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gate : MonoBehaviour
{
    [SerializeField] Vector3 openRot;
    [SerializeField] Vector3 closeRot;
    public float speed;
    public void OpenGate()
    {
        transform.DOLocalRotate(openRot, speed);
    }
    public void CloseGate()
    {
        transform.DOLocalRotate(closeRot, speed);
    }
}
