using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    Rigidbody rb;
    MassControl massControl;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        massControl = GameObject.Find("Mass").GetComponent<MassControl>();
    }

    void Update()
    {
        SendRay();
    }

    void SendRayForward()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * 0.5f, Color.red);
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, transform.localScale.z / 2))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                transform.parent = null;
            }

            if (hit.collider.CompareTag("Cube"))
            {

            }
        }
    }

    void SendRay()
    {
        Vector3 origin = transform.position;

        Debug.DrawRay(origin, transform.forward * 0.5f, Color.red);
        Debug.DrawRay(origin, -transform.up * 0.5f, Color.red);
        Ray rayforward = new Ray(origin, transform.forward);
        Ray raydownward = new Ray(origin, -transform.up);
        RaycastHit forwardhit;
        RaycastHit downhit;

        if (Physics.Raycast(rayforward, out forwardhit, transform.localScale.z / 2))
        {
            if (forwardhit.collider.CompareTag("Obstacle"))
            {
                transform.parent = null;
            }

            //    if (forwardhit.collider.CompareTag("Cube"))
            //    {
            //        Destroy(forwardhit.collider.gameObject);
            //        massControl.AddCube(1);
            //    }
            //}

            //if (Physics.BoxCast(origin, new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y / 2.1f, transform.lossyScale.z / 2), transform.forward, out forwardhit, Quaternion.identity, transform.localScale.z/2))
            //{
            //    if (forwardhit.collider.CompareTag("Obstacle"))
            //    {
            //        transform.parent = null;
            //    }

            //    if (forwardhit.collider.CompareTag("Cube"))
            //    {
            //        Destroy(forwardhit.collider.gameObject);
            //        massControl.AddCube(1);
            //        Debug.Log(forwardhit.collider.gameObject);
            //    }
        }

        if (!Physics.Raycast(raydownward, out downhit, transform.localScale.y / 2))
        {
            rb.AddForce(new Vector3(0, -20f, 0), ForceMode.Acceleration);
            
            if(transform.position.y < -10f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            transform.position = new Vector3(transform.position.x,downhit.point.y + transform.localScale.y/2, transform.position.z);
        }

    }

}
