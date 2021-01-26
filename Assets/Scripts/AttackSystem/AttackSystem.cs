using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    #region Variables
    //public bool CanAttack = true;

    public float PressTimerAttack = 0;      //ReadOnly - Timer che indica in secondi il tempo in cui il pulsante è premuto

    public int Energy = 10;                             //Vieni passato da un altro script
    public int EnergyConsumed = 0;                      //ReadOnly - Energia consumata durante un attacco se leggero, e durante la carica di un attacco se pesante

    public float SecEnergyCharge = 1;                    //Second of time between normal and charge attack and drain energy
    public float TimerSecEnergyHeavy = 0;               //ReadOnly

    public int NormalDamageInspector = 2;         //Visualizzazione in inspector del danno leggero e possibilità di settarlo
    public static int NormalDamage;                      //
    public int ChargeDamageInspector = 4;         //Visualizzazione in inspector del danno pesante e possibilità di settarlo
    public static int ChargeDamage;                      //

    //public ParticleSystem[] AttackParticle = new ParticleSystem[5];     //


    [Header("Test")]
    public GameObject WeaponDistance;
    public GameObject WeaponNear;
    public bool WeaponIsDistance;
    #endregion
    private void Start()
    {
        InitializerAttack();
    }
    void Update()
    {
        AttackPlayer();
    }

    #region Method
    public void InitializerAttack()
    {
        NormalDamage = NormalDamageInspector;
        ChargeDamage = ChargeDamageInspector;
    }

    public void AttackPlayer()
    {
        if (Input.GetMouseButton(0))
        {
            PressTimerAttack += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) && PressTimerAttack < SecEnergyCharge)
        {
            if (Energy >= 1)
            {
                Energy -= 1;
                //Rimuovi tacca dalla ui (probabile venga fatto in un altro script)
                Debug.Log("Attacco Normale");
                if (WeaponIsDistance == true)
                {
                    GameObject Go = Instantiate(WeaponDistance, transform.position + transform.forward * 2, transform.rotation);
                    Go.tag = "NormalAttack";
                }
                else
                {
                    GameObject Go = Instantiate(WeaponNear, transform.position + transform.forward * 2, transform.rotation);
                    Go.tag = "NormalAttack";
                    Destroy(Go, 2);
                }
                //Play animazione attacco e collider
                //Play Suono attacco
                //AttackParticle[0].Play(); //Play particellare attacco
                PressTimerAttack = 0;
                TimerSecEnergyHeavy = 0;
            }
            else
            {
                Discharge();
            }
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            if (Energy >= 1)
            {
                TimerSecEnergyHeavy += Time.deltaTime;
                if (TimerSecEnergyHeavy >= SecEnergyCharge)
                {
                    Energy -= 1;
                    EnergyConsumed += 1;
                    //Animazione carica
                    //Rimozione tacca (probabile venga fatto in un altro script)
                    //Suono rimozione tacca
                    //AttackParticle[3].Play(); //Particellare rimozione tacca
                    TimerSecEnergyHeavy = 0;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    ChargeDamage += EnergyConsumed;
                    Debug.Log("Attacco Caricato");
                    if (WeaponIsDistance == true)
                    {
                        GameObject Go = Instantiate(WeaponDistance, transform.position * 2, transform.rotation);
                        Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                        Go.tag = "ChargeAttack";
                    }
                    else
                    {
                        GameObject Go = Instantiate(WeaponNear, transform.position * 2, transform.rotation);
                        Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                        Destroy(Go, 2);
                        Go.tag = "ChargeAttack";
                    }

                    //Play animazione attacco e collider
                    //Play Suono attacco
                    //AttackParticle[1].Play(); //Play particellare attacco
                    PressTimerAttack = 0;
                    TimerSecEnergyHeavy = 0;

                    //Resettare l'energia consumata
                }
                else
                {
                    if (Energy <= 0)
                    {
                        ChargeDamage += EnergyConsumed;
                        Debug.Log("Attacco Caricato");

                        if (WeaponIsDistance == true)
                        {
                            GameObject Go = Instantiate(WeaponDistance, transform.position * 2, transform.rotation);
                            Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                            Go.tag = "ChargeAttack";
                        }
                        else
                        {
                            GameObject Go = Instantiate(WeaponNear, transform.position * 2, transform.rotation);
                            Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                            Destroy(Go, 2);
                            Go.tag = "ChargeAttack";
                        }

                        //Play animazione attacco e collider
                        //Play Suono attacco
                        //AttackParticle[2].Play(); //Play particellare attacco
                        PressTimerAttack = 0;
                        TimerSecEnergyHeavy = 0;

                        //Resettare l'energia consumata
                    }
                }
            }
            else
            {
                Discharge();
            }
        }
    }

    public void Discharge()
    {
        //Suono guanto scarico
        //AttackParticle[4].Play(); //Particellare guanto scarico
        PressTimerAttack = 0;
    }
    #endregion
}