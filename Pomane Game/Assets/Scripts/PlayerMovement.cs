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
    public Rigidbody2D rb;


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
        
        if (!isMoving && ( (x!=0) || (y!=0))) 
        {
            Debug.Log("peubouger");
            origPos=transform.position;
            StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));
            isMoving = true;
            nextMove=0;
           
            

        }

        nextMove += Time.deltaTime; 
        if(nextMove > timeToMove) 
        {   
            isMoving= false;
        }
       
        
    
    }   
     
        
    void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("coll");
        //if (collision.gameObject.tag == "mur")
        if (collision.gameObject.layer == LayerMask.NameToLayer("colliderLayer")) //ça marche comme ça en mettant le layer(j'ai mis une tilemap mur)
        {
            transform.position = origPos;
        }
        //if (collision.gameObject.tag =="BOUGE")
        //{
        //    Debug.Log("colbouge");
        //    collision.gameObject.transform.position+=deplacement;
        //}
        

        
    }  
    
    private IEnumerator MovePlayer(Vector3 direction)
    
    {
        
        deplacement = direction;
        //origPos= transform.position; t'en as déjà mis un dans l'update
        targetPos = origPos + direction;
        transform.position = targetPos;
     
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


