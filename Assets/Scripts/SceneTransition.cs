using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void StartSelected()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void StageSelected()
    {
        SceneManager.LoadScene("Stage1");
    }
}
