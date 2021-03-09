using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTest : MonoBehaviour
{
    [SerializeField] bool isGameOVer = false;
    void Update()
    {
        if (isGameOVer)
        {
            GameManager.Instance.SetNowState(GameState.GameOver);
        }
    }
}
