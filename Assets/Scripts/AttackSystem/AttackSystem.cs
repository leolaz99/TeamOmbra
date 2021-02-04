using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool CanOnlyMove;
    public bool CanOnlyRotate;

    public Image WeaponActive;
    public Image WeaponDeactive;

    #endregion
    private void Start()
    {
        InitializerAttack();
    }
    void Update()
    {
        AttackPlayer();

        SwitchWeapon();
    }

    #region Method
    public void InitializerAttack()
    {
        NormalDamage = NormalDamageInspector;
        ChargeDamage = ChargeDamageInspector;

        if(WeaponIsDistance == true)
        {
            WeaponActive.color = Color.red;
            WeaponDeactive.color = Color.green;
        }
        else
        {
            WeaponActive.color = Color.green;
            WeaponDeactive.color = Color.red;
        }
    }

    public void AttackPlayer()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button5))
        {
            PressTimerAttack += Time.deltaTime;
            CanOnlyMove = false;
        }
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Joystick1Button5)) && PressTimerAttack < SecEnergyCharge)      //Rilascio
        {
            if (GetComponentInParent<PlayerManager>().energy >= 1)
            {
                GetComponentInParent<PlayerManager>().energy -= 1;
                //Rimuovi tacca dalla ui (probabile venga fatto in un altro script)
                Debug.Log("Attacco Normale");
                if (WeaponIsDistance == true)
                {
                    GameObject Go = Instantiate(WeaponDistance, transform.position, transform.rotation);
                    Go.tag = "NormalAttack";
                }
                else
                {
                    GameObject Go = Instantiate(WeaponNear, transform.position, transform.rotation);
                    Go.tag = "NormalAttack";
                    Destroy(Go, 2);
                }
                //Play animazione attacco e collider
                //Play Suono attacco
                //AttackParticle[0].Play(); //Play particellare attacco
                PressTimerAttack = 0;
                TimerSecEnergyHeavy = 0;
                CanOnlyMove = true;                             //Da togliere quando ci sarà l'animazione
                CanOnlyRotate = false;
            }
            else
            {
                Discharge();
            }
        }
        else if ((Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))|| (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKeyUp(KeyCode.Joystick1Button5)))
        {
            if (GetComponentInParent<PlayerManager>().energy >= 1)
            {
                TimerSecEnergyHeavy += Time.deltaTime;
                if (TimerSecEnergyHeavy >= SecEnergyCharge)
                {
                    GetComponentInParent<PlayerManager>().energy -= 1;
                    EnergyConsumed += 1;
                    //Animazione carica
                    //Rimozione tacca (probabile venga fatto in un altro script)
                    //Suono rimozione tacca
                    //AttackParticle[3].Play(); //Particellare rimozione tacca
                    TimerSecEnergyHeavy = 0;
                }
                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Joystick1Button5))      //Rilascio
                {
                    ChargeDamage += EnergyConsumed;
                    Debug.Log("Attacco Caricato");
                    if (WeaponIsDistance == true)
                    {
                        GameObject Go = Instantiate(WeaponDistance, transform.position, transform.rotation);
                        Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                        Go.tag = "ChargeAttack";
                    }
                    else
                    {
                        GameObject Go = Instantiate(WeaponNear, transform.position, transform.rotation);
                        Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                        Destroy(Go, 2);
                        Go.tag = "ChargeAttack";
                    }

                    //Play animazione attacco e collider
                    //Play Suono attacco
                    //AttackParticle[1].Play(); //Play particellare attacco
                    PressTimerAttack = 0;
                    TimerSecEnergyHeavy = 0;
                    CanOnlyMove = true;                             //Da togliere quando ci sarà l'animazione
                    CanOnlyRotate = false;
                    //Resettare l'energia consumata
                }
                else
                {
                    if (GetComponentInParent<PlayerManager>().energy <= 0)
                    {
                        ChargeDamage += EnergyConsumed;
                        Debug.Log("Attacco Caricato");

                        if (WeaponIsDistance == true)
                        {
                            GameObject Go = Instantiate(WeaponDistance, transform.position, transform.rotation);
                            Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                            Go.tag = "ChargeAttack";
                        }
                        else
                        {
                            GameObject Go = Instantiate(WeaponNear, transform.position, transform.rotation);
                            Go.transform.localScale = new Vector3(1 * EnergyConsumed, 1 * EnergyConsumed, 1 * EnergyConsumed);
                            Destroy(Go, 2);
                            Go.tag = "ChargeAttack";
                        }

                        //Play animazione attacco e collider
                        //Play Suono attacco
                        //AttackParticle[2].Play(); //Play particellare attacco
                        PressTimerAttack = 0;
                        TimerSecEnergyHeavy = 0;
                        CanOnlyMove = true;                             //Da togliere quando ci sarà l'animazione
                        CanOnlyRotate = false;
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
        CanOnlyMove = true;                             //Da togliere quando ci sarà l'animazione
        CanOnlyRotate = false;
    }

    /// <summary>
    /// Metodo richiamato tramite evento finale della animazione di attaco
    /// </summary>
    public void AnimationAttack()
    {
        CanOnlyMove = true;
        CanOnlyRotate = true;
    }

    /// <summary>
    /// Metodo per cambiare l'arma da melee a ranged quando vengono premuti i relativi tasti
    /// </summary>
    public void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //Animazione + Possibile timer di animazione o booleano
            //Suono
            WeaponIsDistance = false;
            WeaponActive.color = Color.green;
            WeaponDeactive.color = Color.red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //Animazione + Possibile timer di animazione o booleano
            //Suono
            WeaponIsDistance = true;
            WeaponActive.color = Color.red;
            WeaponDeactive.color = Color.green;
        }
    }
    #endregion
}