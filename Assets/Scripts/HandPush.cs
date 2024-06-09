using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using TMPro;

public class HandPush : MonoBehaviour
{
    public Transform hand;
    public Transform player;

    private bool pushing;
    public GameObject winTextObject;
    public GameObject ryan;
    public GameObject jack;
    public GameObject nathan;
    public GameObject miguel;
    public GameObject music;

    public float pushSpeed;

    private void Start()
    {
        winTextObject.SetActive(false);
        ryan.SetActive(false);
        jack.SetActive(false);
        nathan.SetActive(false);
        miguel.SetActive(false);
        music.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (pushing)
        {
            hand.position += new Vector3(0f, 0f, pushSpeed * 1.5f) * Time.deltaTime ;
            player.position += new Vector3(0f, 0f, pushSpeed) * Time.deltaTime;
        }
        if (player.position.y < -100f && player.position.y > -500f)
        {

            winTextObject.SetActive(true);
        }
        else if (player.position.y < -500f && player.position.y > -1250f)
        {
            winTextObject.SetActive(false);
            ryan.SetActive(true);
        }
        else if (player.position.y < -1250f && player.position.y > -2000f)
        {
            ryan.SetActive(false);
            jack.SetActive(true);
        }
        else if (player.position.y < -2000f && player.position.y > -2750f)
        {
            jack.SetActive(false);
            nathan.SetActive(true);
        }
        else if (player.position.y < -2750f && player.position.y > -3500f)
        {
            nathan.SetActive(false);
            music.SetActive(true);
        }
        else if (player.position.y < -3500f) music.SetActive(false);

    }

    public IEnumerator Push()
    {
        yield return new WaitForSeconds(0.25f);
        pushing = true;
        yield return new WaitForSeconds(1.5f);
        pushing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Push());
        }
    }
}
