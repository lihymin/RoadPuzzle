using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public GameObject player;
    public Image keyImage;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("GamePlayer"))
        {
            keyImage.gameObject.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + Vector3.up * 0.7f);
            keyImage.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("GamePlayer"))
        {
            keyImage.gameObject.SetActive(false);
        }
    }
}
