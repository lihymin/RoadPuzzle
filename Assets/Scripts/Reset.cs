using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    [SerializeField] private Image[] heart;
    [SerializeField] private int heartCount;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GamePlayer"))
        {
            player.transform.position = new Vector3(-3, -2, 0);
            heartCount--;
            heart[heartCount].gameObject.SetActive(false);
            if (heartCount == 0)
            {
                Time.timeScale = 0;
            }
        }
    }
}
