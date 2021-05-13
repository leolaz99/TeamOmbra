using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BRControllerIA : MonoBehaviour
{
    public static BRControllerIA BRController;
    
    public GameObject Player;                           //Variabile dedicata al player utilizzata come target per l'attacco e il movimento
    
    [Header("Life Behaviour")]
    [Tooltip("Indica la vita del nemico")]
    public int Life;                                    //Indica il valore della vita
    string OtherTag;                                    //Variabile temporanea che contiene il tag dell'oggetto che ha colpito il gameobjcet collegato a questo script
    
    [Header("Colllider Child Management")]
    public GameObject SecurityDistanceArea;             //Si riferesce all'area della distanza di sicurezza del nemico
    public float XValueSDA;                             //Corrisponde alla X della distanza di sicurezza
    public float ZValueSDA;                             //Corrisponde alla Z della distanza di sicurezza

    [Header("Attack value")]
    [HideInInspector] public float TimerAttack;         //Timer che incrementa automaticamente
    public float MaxTimerAttack;                        //Massimo valore che può raggiungere il timer che definisce ogni quanto può andare in stato di attacco
    public GameObject Bullet;                           //Proiettile che viene istanziato quando attaccherà

    [Header("Distancing Value")]
    public float SpeedDistancing;                       //Velocità utilizzata solamente nello stato di distanziamento
    public bool AlertDistance = false;


    [Header("Activate enemy value in room")]
    /*[HideInInspector]*/ public Renderer EnemyRenderer;
    /*[HideInInspector]*/ public int RoomNumber = 0;
    /*[HideInInspector]*/ public int PlayerInRoom = 0;                      //Questo sarà un valore static definito dal player
    
    [Header("Other Value")]
    public NavMeshAgent agent;


    #region Define Variable Action
    public Action ActionRemoveLife;
    #endregion

    #region Lifecycle
    /// <summary>
    /// Metodo che agisce in editor dopo una modifica dell'inspector
    /// </summary>
    private void OnValidate()
    {
        SecurityDistanceArea.transform.localScale = new Vector3(XValueSDA, SecurityDistanceArea.transform.localScale.y, ZValueSDA);     //Modifica la scala dell'area della distanza di sicurezza
    }

    void Start()
    {
        BRController = this;                                    //Creo una istanza per passarla negli stati
        EnemyRenderer = GetComponent<Renderer>();               //Utilizzato per accedere al renderer di se stesso per vedere se è visibile nella inquadratura della camera

        #region Define Registration Action
        ActionRemoveLife = ColliderRemoveLife;                  //Iscrivo il metodo ColliderRemoveLife all'azione ActionRemoveLife 
        #endregion
    }

    void Update()
    {
        GetComponent<Animator>().SetInteger("BR-Life", Life);       //Uguaglio il parametro life con l'effettiva vita del player
    }
    #endregion

    /// <summary>
    /// Metodo che verrà richiamato nello stato di take damage che in base al tag del collider rimuoverà la vita
    /// - Verificare se in base alle esigenze lasciare il metodo o accorparlo nell'OnTriggerEnter
    /// </summary>
    public void ColliderRemoveLife()
    {
        if (OtherTag == "NormalAttack")                     //Se il tag è uguale a "NormalAttack"
            Life -= AttackSystem.NormalDamage;              //Diminuisco la vita con il valore di NormalDamage
        else if (OtherTag == "ChargeAttack")                //Se il tag è uguale a "ChargeAttack"
            Life -= AttackSystem.ChargeDamage;              //Diminuisco la vita con il valore di ChargeDamage
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NormalAttack" || other.tag == "ChargeAttack")         //Se il tag è uguale a uno di quelli proposti
        {
            OtherTag = other.tag;                                               //Definisco la string con il tag dell'attacco ricevuto
            GetComponent<Animator>().SetTrigger("BR-Damage");                   //Cambio stato tramite il parametro BR-Damage e passare allo stato BRTakeDamage
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    #region Non usato e non serve - State
    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Idle State" nel nemico "Bandito Ranged"
    /// Dallo stato di "BR - Idle State" si può passare allo stato di "BR - Aggro State" quando se stesso viene inquadrato dalla camera e il giocatore è nella stessa stanza
    /// </summary>
    public void BanditRangedIdleState()
    {
        //Se il nemico è visibile dalla camera e il giocatore è nella stanza
        //Da "BR - Idle State" vai a "BR - Aggro State"
    }

    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Aggro State" nel nemico "Bandito Ranged"
    /// 
    /// </summary>
    public void BanditRangedAggroState()
    {
        //Si avvicina al giocatore fino a che non è a portata di attacco e non ci sono ostacoli in mezzo alla linea di tiro
        //Mantiene distanza di X unità

        //Se le due condizioni prima vengono rispettate
        //Passa allo stato di attacco

        //Se il giocatore è più vicino della distanza minima del nemico
        //Passa allo stato di distanziamento
    }

    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Distanziamento State" nel nemico "Bandito Ranged"
    /// 
    /// </summary>
    public void BanditRangedDistanziamentoState()
    {
        //Si allontana dal giocatore spostandosi nella direzione opposta

        //Se il giocatore è fuori dalla distanza di sicurezza
        //Passo allo stato di aggro

        //Ogni x secondi
        //Passa allo stato di attacco
    }

    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Distanziamento State" nel nemico "Bandito Ranged"
    /// 
    /// </summary>
    public void BanditRangedAttackState()
    {
        //Il nemico è rivolto verso il player

        //Durata di attacco = Durata di animazione
        //Animazione divisa in Anticipazione (puntamento), colpo (sparo), recovery

        //Anticipazione: il nemico calcola la linea di tiro (è una linea retta)

        //Colpo: istanziato il proiettile che si muoverà lungo la linea retta precedentemente calcolata
        //Se il proiettile collide il player:
        //Fa danno e applica un knockback nella direzione del proiettile
        //Il proiettile viene distrutto

        //Se il proiettile collide un altro nemico:
        //Non fa danno ma applica un knockback nella direzione del proiettile
        //Il proiettile viene distrutto

        //Se il proiettile collide con un prop
        //Il proiettile viene distrutto

        //Finita l'animazione:

        //Se il giocatore è dentro la distanza di sicurezza
        //Passa allo stato di distanziamento

        //Se il giocatore è fuori dalla distanza di sicurezza
        //Passa allo stato di aggro

        //Se il giocatore è dentro la portata di attacco ma fuori dalla distanza di sicurezza
        //Passa allo stato di attacco (ripete l'attacco)
    }

    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Take Damage State" nel nemico "Bandito Ranged"
    /// 
    /// </summary>
    public void BanditRangedTakeDamageState()
    {
        //Se subisce danni da una trappola o dal giocatore e la sua vita non è a 0 o meno           --> non va qua, va nell'update o nell'anystate

        //Il nemico non può far niente fino al termine dell'animazione

        //Se il giocatore è dentro la distanza di sicurezza
        //Passa allo stato di distanziamento

        //Se il giocatore è fuori dalla distanza di sicurezza
        //Passa allo stato di aggro

        //Se il giocatore è dentro la portata di attacco ma fuori dalla distanza di sicurezza
        //Passa allo stato di attacco
    }

    /// <summary>
    /// Metodo che si riferisce allo stato "BR - Death State" nel nemico "Bandito Ranged"
    /// 
    /// </summary>
    public void BanditRangedDeathState()
    {
        //Se subisce danni da una trappola o dal giocatore e la sua vita è a 0 o meno           --> non va qua, va nell'update o nell'anystate

        //Esegue animazione e non può fare niente
        //Non viene distrutto e non viene disattivato
    }
    #endregion

    //Controllare tutti i commenti, più precisamente i nomi degli stati, dei metodi e delle possibili funzioni
    //Prima parte: Strutturazione in pseduo-pseudo codice del progetto e strutturazione delle cartelle, dell'animator e delle note
}