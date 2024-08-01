using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    public static event Action<string> OnAnswered;

    private float coolDown = 15f;
    private bool isCoolDown = true;

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (isCoolDown)
            {
                isCoolDown = false;
                StartCoroutine(CoolDown());

                collision.gameObject.transform.SetParent(transform);
                transform.DOMoveY(transform.position.y - 25, 5).OnComplete(() =>
                {
                    transform.DOMoveY(transform.position.y + 75, 0.1f).OnComplete(() => 
                    {
                        collision.gameObject.transform.SetParent(null);
                        transform.DOMoveY(transform.position.y - 50, 0.6f);
                        collision.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000);

                        string answer = collision.transform.GetComponentInChildren<TextMeshProUGUI>().text;
                        OnAnswered?.Invoke(answer);

                        collision.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    });

                });

            }
            
        }
    }

    // Cooldown method
    private IEnumerator CoolDown()
    {
        
        yield return new WaitForSeconds(coolDown);
        isCoolDown = true;
    }
}
