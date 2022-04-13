using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Transform ghost;
    
    bool m_IsGhostSeen;
    bool m_IsGhostInRange;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.transform == ghost)
        {
            m_IsGhostInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform == ghost)
        {
            m_IsGhostInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsGhostInRange)
        {
            Vector3 direction = ghost.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == ghost)
                {
                    //if(!m_IsGhostSeen)
                    //{
                        Debug.Log("Entered ghost");
                        //m_IsGhostSeen = true;
                    //}
                    // call method of ghost to set disappear to true
                }
            }
            
        }
            //Debug.Log("Exited ghost");
            //m_IsGhostSeen = false;
            // call method of ghost to set disappear to false
    }
}
