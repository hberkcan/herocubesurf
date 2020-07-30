using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassControl : MonoBehaviour
{
    public float movementvelocity;
    public float cubescale;
    public GameObject cubepref;
    public float horizontalspeed;
    public GameObject maincube;
    Vector3 origin;
    int before = -1;
    public bool startgame = false;
    public bool wingame = false;

    void Start()
    {

    }

    void Update()
    {
        origin = transform.position;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    zz = true;
        //}

        MoveMass();
        SendRay();
        DeleteCube();
    }

    public void AddCube(int amount)
    {
        for(int i=0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetComponent<Rigidbody>().MovePosition(transform.GetChild(i).position + new Vector3(0, amount * cubescale, 0));
            transform.GetChild(i).position += new Vector3(0, amount * cubescale, 0);
        }

        for (int i = 0; i < amount; i++)
        {
            Instantiate(cubepref, transform.position + new Vector3(0, i * cubescale, 0), Quaternion.identity, transform);
        }
    }

    void DeleteCube()
    {
        RaycastHit groundhit;
        Ray rayground = new Ray(origin, -transform.up);
        if(Physics.Raycast(rayground, out groundhit, transform.localScale.y / 2))
        {
            if(groundhit.collider.CompareTag("Ground"))
            {
                int present = groundhit.collider.gameObject.GetInstanceID();

                if(present != before)
                {
                    Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                    before = present;
                }
            }
        }
    }

    void MoveMass()
    {
        float horizontalmovement = Input.GetAxisRaw("Horizontal");
        
        if (startgame)
        {
            transform.Translate(horizontalmovement * horizontalspeed * Time.deltaTime, 0, movementvelocity * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f), transform.position.y, transform.position.z);
        }
    }

    void SendRay()
    {
        RaycastHit hit;
        
        if(Physics.BoxCast(origin,transform.localScale/2,transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Cube"))
            {
                if(hit.distance < transform.localScale.z / 4)
                {
                    Destroy(hit.collider.gameObject);
                    if(hit.transform.childCount < 1)
                    {
                        AddCube(1);
                    }
                    else
                    {
                        AddCube(hit.transform.childCount);
                    }
                }
            }

            if (hit.collider.CompareTag("Win"))
            {
                if (hit.distance < transform.localScale.z / 4)
                {
                    wingame = true;
                }
            }
        }

        if (Physics.BoxCast(origin, transform.localScale / 2, transform.right, out hit))
        {
            if (hit.collider.CompareTag("Cube"))
            {
                if (Mathf.Abs(hit.distance) < transform.localScale.x / 4)
                {
                    Destroy(hit.collider.gameObject);
                    if (hit.transform.childCount < 1)
                    {
                        AddCube(1);
                    }
                    else
                    {
                        AddCube(hit.transform.childCount);
                    }
                }
            }
        }

        if (Physics.BoxCast(origin, transform.localScale / 2, -transform.right, out hit))
        {
            if (hit.collider.CompareTag("Cube"))
            {
                if (Mathf.Abs(hit.distance) < transform.localScale.x / 4)
                {
                    Destroy(hit.collider.gameObject);
                    if (hit.transform.childCount < 1)
                    {
                        AddCube(1);
                    }
                    else
                    {
                        AddCube(hit.transform.childCount);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, transform.localScale / 2, transform.forward, out hit, Quaternion.identity, 100f))
        {
            //Destroy(hit.collider.gameObject);
            //AddCube(1);
        }

        Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.localScale);
    }
}
