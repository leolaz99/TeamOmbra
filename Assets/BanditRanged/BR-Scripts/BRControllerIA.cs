using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BRControllerIA : MonoBehaviour
{
    public GameObject SecurityDistanceArea;         //Si riferesce all'area della distanza di sicurezza del nemico
    public float XValueSDA;                         //Corrisponde alla X della distanza di sicurezza
    public float ZValueSDA;                         //Corrisponde alla Z della distanza di sicurezza

    public GameObject AttackArea;                   //Si riferesce all'area dell'attacco del nemico
    public float XValueAA;                          //Corrisponde alla X dell'attacco
    public float ZValueAA;                          //Corrisponde alla Z dell'attacco

    /*[HideInInspector]*/ public Renderer EnemyRenderer;

    public int RoomNumber = 0;
    public int PlayerInRoom = 0;            //Questo sarà un valore static definito dal player
    
    public GameObject Player;
    public NavMeshAgent agent;

    public bool AlertDistance = false;
    public GameObject Way;

    public GameObject Bullet;

    /// <summary>
    /// Metodo che agisce in editor dopo una modifica dell'inspector
    /// </summary>
    private void OnValidate()
    {
        SecurityDistanceArea.transform.localScale = new Vector3(XValueSDA, SecurityDistanceArea.transform.localScale.y, ZValueSDA);     //Modifica la scala dell'area della distanza di sicurezza
        AttackArea.transform.localScale = new Vector3(XValueAA, AttackArea.transform.localScale.y, ZValueAA);                           //Modifica la scala dell'area dell'attacco del nemico
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region State
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

    #region Event - BR Attack State
    public void EventBRAttackShoot()
    {
        //AttackDrawLine = false; //Disattiva la linea di calcolo (?)
        Instantiate(Bullet, agent.transform.position, agent.transform.rotation);    //Istanzia il proiettile
        //Va in recovery
    }
    public void EventBRAttackFinish()
    {
        GetComponent<Animator>().SetBool("BR-CanAttack", false); //Finisce lo stato di attaco
    }
    #endregion

    #region Event - BR Take Damage State
    public void EventBRFinishTakeDamage()
    {
        //GetComponent<Animator>().Play("BR - Idle State"); //Temp

        if (AlertDistance == true)                                                                          //Se è nel distanziamento
        {
            GetComponent<Animator>().Play("BR - Distancing State"); //Temp - Passaggio istantaneo           TODO: Controllare i parametri
        }
        else if (EnemyRenderer.isVisible && PlayerInRoom == RoomNumber)                                     //Se è nell'aggro
        {
            GetComponent<Animator>().Play("BR - Aggro State"); //Temp - Passaggio istantaneo                TODO: Controllare i parametri
        }
        else if (AlertDistance == false)                                                                    //Se è nella fase di attacco - Va modificato
        {
            GetComponent<Animator>().Play("BR - Attack State"); //Temp - Passaggio istantaneo               TODO: Controllare i parametri
        }
    }
    #endregion
}


//Controllare tutti i commenti, più precisamente i nomi degli stati, dei metodi e delle possibili funzioni


//Prima parte: Strutturazione in pseduo-pseudo codice del progetto e strutturazione delle cartelle, dell'animator e delle note