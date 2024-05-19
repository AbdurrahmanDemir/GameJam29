using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public GameObject panel;

    private void Start()
    {
        targetOffset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, .125f);
    }
    public void panelOff()
    {
        panel.SetActive(false);
    }
}
