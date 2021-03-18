﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Life Behaviour")]
    [Tooltip("Indica la vita del nemico")]
    public int Life;

    #region Trigger Zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NormalAttack")
        {
            Life -= AttackSystem.NormalDamage;
        }
        else if (other.tag == "ChargeAttack")
        {
            Life -= AttackSystem.ChargeDamage;
        }

        if (Life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    #region Collision Zone
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NormalAttack")
        {
            Life -= AttackSystem.NormalDamage;
            print("Colpito");
        }
        else if (collision.gameObject.tag == "ChargeAttack")
        {
            Life -= AttackSystem.ChargeDamage;
            print("Colpito");
        }

        if (Life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}