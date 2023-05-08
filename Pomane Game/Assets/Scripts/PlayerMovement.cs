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
            
            origPos=transform.position;
            StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));
        }


        // transform.position= origPos;
        // if (Physics2D.OverlapCircle(transform.position, detectionRadius, colliderLayer))
        //  {
        //     ;
            
        // }
        
    
    }   
     
        // if (!Physics2D.OverlapCircle(transform.position + new Vector3(x, y, 0f), detectionRadius, colliderLayer))
        // {
        //     if (!isMoving) StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));
        // }
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
        
        isMoving = true;
        float nextMove = 0;

         //là où il est
        deplacement = direction;
        origPos= transform.position;
        targetPos = origPos + direction; //là où il sera
        transform.position = targetPos;
        while (nextMove < timeToMove) //La fonction Lerp utilise trois paramètres, le départ et l'arrivée en vector3 et une valeur entre 0 et 1
        {
            nextMove += Time.deltaTime; //temps réel universel
            yield return null;

        } //recheck la position d'arrivée

        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
}


