using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.15f; //vitesse déplacement
    public float detectionRadius = 10f;
    public LayerMask colliderLayer;
    private Vector3 deplacement;
    private float nextMove = 0;

    // Update is called once per frame
    void Update()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Vérifie que le joueur ne va pas en diago
        if (x != 0 && y != 0)
        {

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                y = 0;
            }
            else
            {
                x = 0;
            }
        }
        if (!isMoving) 
        {
            Debug.Log("peubouger");
            origPos=transform.position;
            StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));
            nextMove=0;
            isMoving=true;

        }

        nextMove += Time.deltaTime; //temps réel universel
        if(nextMove > timeToMove) 
        {   
            isMoving= false;
        }
       
        
    
    }   
     
        
    void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("coll");
        if(collision.gameObject.tag== "mur")
        {
            transform.position = origPos;
        }
        if(collision.gameObject.tag =="BOUGE")
        {
            Debug.Log("colbouge");
            collision.gameObject.transform.position+=deplacement;
        }
        
        
    }  
    
    private IEnumerator MovePlayer(Vector3 direction)
    
    {
        
        // isMoving = true;

         //là où il est
        deplacement = direction;
        origPos= transform.position;
        targetPos = origPos + direction; //là où il sera
        transform.position = targetPos;
        // while (nextMove < timeToMove) 
        // {
        //     nextMove += Time.deltaTime; //temps réel universel
            yield return null;

        // } 

        // isMoving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
}


