using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCamZoom : MonoBehaviour
{
    public float zoomSpeed;
    public bool zoomStart;

    public Transform diceTransform;
    public Transform camTransform;

    private Vector3 startPos;
    private Vector3 targetPos;

    private Vector3 startRot;
    private Vector3 targetRot;

    private float startFov;
    private float targetFov;

    private float zoomVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomStart)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, zoomVal);
            Vector3 newEuler = Vector3.Lerp(startRot, targetRot, zoomVal);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(startFov, targetFov, zoomVal);
            transform.eulerAngles = newEuler;



            zoomVal += Time.deltaTime * zoomSpeed;
            if (zoomVal > 1)
            {
                zoomStart = false;
                diceTransform.gameObject.GetComponent<roll>().enableUpdate();
                GetComponent<Camera>().enabled = false;
                GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    public void Zoom()
    {
        zoomStart = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        startPos = transform.position;
        targetPos = diceTransform.position;

        startRot = new Vector3(51.531f, 72.397f, 0f);
        targetRot = new Vector3(0f, 90f, 0f);

        startFov = GetComponent<Camera>().fieldOfView;
        targetFov = camTransform.GetComponent<Camera>().fieldOfView;

        zoomVal = 0f;
    }
}
