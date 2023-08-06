using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Collections;

public class FindBotInRange : MonoBehaviour
{

    [SerializeField] private LayerMask botLayerMask;

    public int botInCircleRange;
    public RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[20];
    public Transform nearestPoint;

    public GameObject Player;

    private void Start()
    {
        StartCoroutine(FOVRoutine(0.2f));
    }

    private IEnumerator FOVRoutine(float time)
    {

        while (true)
        {
            yield return Cache.GetWFS(time);
            ScanOtherPlayerInCirlceRange();
        }
    }

    private void ScanOtherPlayerInCirlceRange()
    {
        botInCircleRange = Physics2D.CircleCastNonAlloc(this.transform.position, 6f, Vector2.zero, raycastHit2Ds, 0f, botLayerMask);
           
        // find nearest player
        float distance = 100f, temp;

        if (botInCircleRange == 0)
        {
            nearestPoint = null;
            return;
        }

        if (Player == null)
        {
            nearestPoint = null;
            return;
        }


        for (int i = 0; i < botInCircleRange; i++)
        {
            temp = Vector2.Distance(Player.transform.position, raycastHit2Ds[i].transform.position);
            if (temp < distance)
            {
                distance = temp;
                nearestPoint = raycastHit2Ds[i].transform;
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(this.transform.position, 6f);
    //}

    public bool HaveTarget()
    {
        return botInCircleRange > 0;
    }

    public Transform GetNearestTarget()
    {
        return nearestPoint;
    }

}

