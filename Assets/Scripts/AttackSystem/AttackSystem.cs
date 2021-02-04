using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    #region Variables
    //public bool CanAttack = true;

    [Space(10)]
    [Header("------- Damage Value - Attack Management")]
    [Tooltip("E' il danno di un'arma non caricata")]
    public int NormalDamageInspector = 2;         //Visualizzazione in inspector del danno leggero e possibilità di settarlo
    public static int NormalDamage;
    [Tooltip("E' il danno base di un'arma caricata - Nel codice diventa un moltiplicatore")]
    public int ChargeDamageInspector = 4;         //Visualizzazione in inspector del danno pesante e possibilità di settarlo
    public static int ChargeDamage;

    [Space(10)]
    [Header("------- Energy Value - Attack Management")]
    [Space(20)]
    public int LoseHitEnergy;

    [Space(10)]
    [Header("------- Charge Value - Attack Management")]
    [Space(20)]
    [Tooltip("E' il tempo che passa tra il consumo di una tacca di energia e l'altra")]
    public float SecEnergyCharge = 1;                    //Second of time between normal and charge attack and drain energy


    //public ParticleSystem[] AttackParticle = new ParticleSystem[5];     //

    [Space(5)]
    [Header("Reference - GameObject che indicano il tipo di proiettile che la relativa arma spara")]
    [Space(10)]
    [Header("------- Gameobject and UI - Attack Management")]
    [Space(20)]

    [Tooltip("Gameobject per il proiettile dell'arma ranged")]
    public GameObject WeaponDistance;
    [Tooltip("Gameobject per il proiettile dell'arma melee")]
    public GameObject WeaponNear;

    [Space(5)]
    [Header("UI - Switch Weapon")]
    [Tooltip("Referenza del gameobject nella ui nella posizione relativa all'arma disattivata")]
    public Image WeaponActive;
    [Tooltip("Referenza del gameobject nella ui nella posizione relativa all'arma disattivata")]
    public Image WeaponDeactive;

    [Space(5)]
    [Header("ReadOnly - Le seguenti variabili sono read only per test dei programmatori - Non Toccare")]
    [Header("------------------------------------------------------------------------------------------------------------------------")]
    [Space(50)]
    [Tooltip("ReadOnly - Booleano che indica se l'arma corrente è melee o ranged")]
    public bool WeaponIsDistance;
    [Tooltip("ReadOnly - Variabile per bloccare il movimento del player quando carica l'attacco e attacca")]
    public bool CanOnlyMove;
    [Tooltip("ReadOnly - Variabile per bloccare la rotazione nel player nel periodo di attacco (dopo il possibile caricamento)")]
    public bool CanOnlyRotate;
    [Tooltip("ReadOnly - E' un timer che indica se il caricamento è superiore a SecEnegyCharge")]
    public float TimerSecEnergyHeavy = 0;               //ReadOnly
    [Tooltip("ReadOnly - Indica quanto tempo hai premuto il pulsante per caricare l'attacco")]
    public float PressTimerAttack = 0;      //ReadOnly - Timer che indica in secondi il tempo in cui il pulsante è premuto
    [Tooltip("ReadOnly - Tiene traccia dell'energia consumata quando attacco senza caricamento e sia con il caricamento")]
    public int EnergyConsumed = 0;                      //ReadOnly - Energia consumata durante un attacco se leggero, e durante la carica di un attacco se pesante








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

    /// <summary>
    /// Metodo richiamato nello start per inizializzare varie variabili
    /// </summary>
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

    /// <summary>
    /// Metodo che gestisce l'attacco del player, sia melee e sia ranged
    /// </summary>
    public void AttackPlayer()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button5))
        {
            PressTimerAttack += Time.deltaTime;
            CanOnlyMove = false;
        }
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Joystick1Button5)) && PressTimerAttack < SecEnergyCharge)      //Rilascio
        {
            if (GetComponentInParent<PlayerManager>().energy >= LoseHitEnergy)
            {
                GetComponentInParent<PlayerManager>().energy -= LoseHitEnergy;
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
            if (GetComponentInParent<PlayerManager>().energy >= LoseHitEnergy)
            {
                TimerSecEnergyHeavy += Time.deltaTime;
                if (TimerSecEnergyHeavy >= SecEnergyCharge)
                {
                    GetComponentInParent<PlayerManager>().energy -= LoseHitEnergy;
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

    /// <summary>
    /// 
    /// </summary>
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