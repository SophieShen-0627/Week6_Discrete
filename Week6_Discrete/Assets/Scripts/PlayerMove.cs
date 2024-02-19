using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float PlayerMovingSpeed = 3f;
    public Vector3 Target;
    public bool IsMoving = false;


    private void Start()
    {
        PlayerMovingSpeed = DataManager.datas.PlayerMovingSpeed;
        Target = transform.position;
    }

    void Update()
    {
        if (transform.position != Target && !IsMoving)
        {
            StartCoroutine(Move(Target));
            IsMoving = true;
        }
    }

    IEnumerator Move(Vector3 target)
    {
        yield return new WaitForEndOfFrame();

        if (Vector3.Distance(transform.position, target) >= 0.05f)
        {
            transform.position += (target - transform.position).normalized * PlayerMovingSpeed * Time.deltaTime;
            StartCoroutine(Move(target));
        }
        else
        {
            transform.position = target;
            IsMoving = false;
        }
    }

}
