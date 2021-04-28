using Photon.Pun;
using UnityEngine;


public class Sword : MonoBehaviour
{
    [SerializeField] private PhotonView PV;
    private int attackStatus;
    private float time;
    public BoxCollider2D attackHitboxCollider;
    public Animator animator;

    public bool canAttack;

    private void Start()
    {
        canAttack = true;
        time = 0;
        attackStatus = 0;
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            Attack();
        }
    }

    public void Attack()
    {
        //On reset le tout 
        if (!canAttack || time > 0.8) 
        {
            UpdateAttack(false,0) ;
        }
        else
        {
            //On incrémente notre timer en secondes
            if (attackStatus != 0)
            {
                time += Time.deltaTime;
            }
            //On presse le bouton d'attaque
            if(Input.GetMouseButtonDown(0))
            {
                if (attackStatus == 0 || (attackStatus == 2 && time > 0.2))
                {
                    UpdateAttack(true,1);
                }
                else if (attackStatus == 1 && time > 0.2)
                {
                    UpdateAttack(true,2);
                }
            }
        }
        //On met à jour la hitbox d'attaque
        if(time > 0.15)
        {
            /*PV.RPC("AttackEnabled", RpcTarget.All, false);*/
        }       
    }

    private void UpdateAttack(bool update,int attackStatus)
    {
        time = 0;
        this.attackStatus = attackStatus;
        /*if(update)
            PV.RPC("AttackEnabled", RpcTarget.All, true);*/
        animator.SetInteger("AttackStatus", attackStatus);
    }

    /*[PunRPC]
    void AttackEnabled(bool isEnable)
    {
        attackHitboxCollider.enabled = isEnable;
    }*/
}
