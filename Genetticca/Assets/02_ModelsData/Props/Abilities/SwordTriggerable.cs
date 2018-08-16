using UnityEngine;
using System.Collections;

public class SwordTriggerable : MonoBehaviour
{

    //[HideInInspector]
    //public GameObject swordPrefab;

    [HideInInspector]
    public Sword sword;
    [HideInInspector]
    public GameObject swordGO;
    [HideInInspector]
    public SwordController swordController;



    //string parent;
    float m_NextAttackTime;
    float m_CurrentAttackTime;
    float m_CurrentDurationAttack;
    bool m_isAttacking, m_swordIsInAction;                  // Float variable to hold the amount of force which we will apply to launch our projectiles
    


    // Use this for initialization
    void Start()
    {
        m_NextAttackTime = 0;
        m_CurrentAttackTime = 0;
        m_CurrentDurationAttack = 0;
        m_isAttacking = false;
        m_swordIsInAction = false;
    }

    private void Update()
    {
        if (m_CurrentAttackTime <= Time.time && !m_swordIsInAction && m_isAttacking)
        {
            m_swordIsInAction = true;
            swordGO.SetActive(m_isAttacking);
            m_NextAttackTime = Time.time + sword.timeBetwenAttacks;
            m_CurrentDurationAttack = Time.time + sword.durationSword;

            //version instanciada
            //parent = GameObject.FindGameObjectsWithTag(sword.tagParent)[0];
            //Instantiate(swordPrefab, transform.position, transform.rotation, parent.transform);
        }
        if(m_CurrentDurationAttack<=Time.time && m_isAttacking)
        {
            m_isAttacking = false;
            m_swordIsInAction = false;
            swordGO.SetActive(m_isAttacking);
            m_CurrentAttackTime = Time.time + sword.castTime;
        }
    }


    public void Attack()
    {
        if (m_NextAttackTime < Time.time && !m_isAttacking)
        {
            m_isAttacking = true;
            
        }
    }


    public bool getIsAttacking()
    {
        return this.m_isAttacking;
    }

    public void setIsAttacking(bool value)
    {
        m_isAttacking = value;
    }

}