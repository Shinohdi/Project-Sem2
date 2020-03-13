using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimCamera : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetForVertical;
    [SerializeField] private Transform pourLaCam;


    [SerializeField] private float rotateSpeed;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;
        var hor = Input.GetAxis("Mouse X") * rotateSpeed;
        var ver = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.transform.Rotate(0, hor, 0);
        targetForVertical.transform.Rotate(ver, 0, 0);
        
        
        var desiredAngleY = target.transform.eulerAngles.y;
        var desiredAngleX = targetForVertical.transform.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredAngleX, desiredAngleY, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(pourLaCam);
      
    }

   
}
