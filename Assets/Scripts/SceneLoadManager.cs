using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void OnClickStage1()
    {
        SceneManager.LoadScene("Stage");
    }
}
