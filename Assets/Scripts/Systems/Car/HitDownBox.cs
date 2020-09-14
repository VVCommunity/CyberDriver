using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitDownBox : HitBox
{
    public UnityEvent HitDown;
    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnHit");
        if (collision.collider.CompareTag("Enemy"))
        {
            HitEnemy.Invoke();
        }
        else HitDown.Invoke();
    }
}
