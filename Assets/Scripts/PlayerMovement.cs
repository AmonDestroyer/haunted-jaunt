using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool debugMode = false;
    public float turnSpeed = 1.0f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement; 
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 face = transform.forward * vertical;
        Vector3 side = Vector3.Cross(transform.forward, Vector3.up) * -horizontal;
        m_Movement = face + side;
        m_Movement.Normalize();

        bool isWalking = !Mathf.Approximately(vertical, 0.0f);
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Movement = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        m_Rotation = Quaternion.LookRotation(m_Movement);

        m_LookDir = Quaternion.AngleAxis(turnSpeed * Time.deltaTime, Vector3.up);

        if (debugMode)
        {
            Debug.Log($"Input: ({horizontal},{vertical})");
            Debug.Log($"Look Direction: ({transform.forward.x},{transform.forward.z})");
            Debug.Log($"Command Direction: ({face},0)");
        }

    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
