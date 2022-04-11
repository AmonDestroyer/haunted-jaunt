using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomEntry : MonoBehaviour
{
    /*
     * Purpose: When the player collides with this object, if they face in the same direction
     * then the room location is displayed. If this is the first time the room is entered then
     * a tone will be played.
     * Entry Direction: The positive z axis determines entry into the room
     */

    public float fadeDuration = 1f;
    public float displayImageDuration = 2f;
    public string location = "<location>";
    public GameObject player;
    public TextMeshProUGUI locationText;
    public AudioSource entryAudio;

    bool m_HasEntered;
    float m_Timer;
    bool m_HasAudioPlayed;
    float m_Direction;

    Rigidbody m_Rigidbody;

    //Called before the first 
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        locationText.text = location;
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            m_Direction = Vector3.Dot(transform.forward, player.transform.forward);
            if(m_Direction > 0)
            {
                m_HasEntered = true;
                m_Timer = 0;
            }
        }
    }


    void Update()
    {
        if (m_HasEntered)
        {
            EnteredRoom(entryAudio);
        }
    }

    void EnteredRoom(AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        locationText.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration)
        {
            locationText.alpha = 1 - (m_Timer - fadeDuration) / displayImageDuration;
        }
        if (m_Timer > fadeDuration + displayImageDuration)
            m_HasEntered = false;
    }
}
