using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.15f; //vitesse d�placement
    public float detectionRadius = 10f;
    public LayerMask colliderLayer;

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //V�rifie que le joueur ne va pas en diago
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

        if (!isMoving) StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));

        //if (!Physics2D.OverlapCircle(transform.position + new Vector3(x, y, 0f), detectionRadius, colliderLayer))
        //{
            
        //}
    }

    //void OnCollisionEnter2D(Collision2D truc)
    //{
    //    if (truc.gameObject.tag == "Collision")
    //    {
    //        isMoving = false;
    //    }
    //}

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        float nextMove = 0;

        origPos = transform.position; //l� o� il est
        targetPos = origPos + direction; //l� o� il sera

        while (nextMove < timeToMove) //La fonction Lerp utilise trois param�tres, le d�part et l'arriv�e en vector3 et une valeur entre 0 et 1
        {
            transform.position = targetPos;   //Lerp(origPos, targetPos, nextMove / timeToMove);
            nextMove += Time.deltaTime; //temps r�el universel
            yield return null;

        }

        transform.position = targetPos; //recheck la position d'arriv�e

        isMoving = false;
    }
}

