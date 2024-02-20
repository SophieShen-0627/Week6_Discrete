using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] DashPoint[] points;
    private bool HasEnd = false;

    [SerializeField] int NumAlive = 0;
    private void Awake()
    {
        points = FindObjectsOfType<DashPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        NumAlive = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].gameObject.activeInHierarchy)
            {
                NumAlive += 1;
            }
        }

        if (NumAlive <= 1 && !HasEnd)
        {
            HasEnd = true;
            StartCoroutine(DoGameEnd());
        }
    }

    IEnumerator DoGameEnd()
    {
        yield return new WaitForEndOfFrame();

        Time.timeScale = 0;
    }
}
