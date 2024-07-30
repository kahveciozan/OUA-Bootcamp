using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PushButtonManager : MonoBehaviour
{
    // when Player collides this object, it will moved down slowly
    [SerializeField] private float moveDownRange;
    private Vector3 initialPosition;
    bool isCoolDown = true;

    private void Start()
    {
        initialPosition = transform.position;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" )
        {
            Debug.Log("Player Entered with push button");

            if (isCoolDown) // && transform.position.y > initialPosition.y - moveDownRange
            {
                isCoolDown = false;
                transform.DOMove(new Vector3(transform.position.x, initialPosition.y - moveDownRange, transform.position.z), 2f).OnComplete(() =>
                {
                    transform.DOMove(initialPosition, 1f).OnComplete(() => 
                    {
                        isCoolDown = true;
                    });
                    
                });

                collision.transform.GetComponentInChildren<TextMeshProUGUI>().text += transform.name;

            }


        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.transform.tag == "Player" && false)
    //    {
    //        Debug.Log("Player Exited from push button");
    //        // wait for 2 second and check OnCollisionEnter then if not entered last 2 second move up
    //        transform.DOMove(initialPosition, 2f);

    //    }
    //}




}
