using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 1f; // Ўвидк≥сть руху м'€ча

    private Rigidbody2D rb;
    private Vector2 direction;

    public GameObject loseScreen;
    public GameObject winScreen;
    void Start()
    {
        
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        LaunchBall(speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchBall(float speed)
    {
        // √енеруЇмо випадковий напр€мок дл€ руху м'€ча
        direction = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(0, 1f))
            .normalized;
        // «адаЇмо швидк≥сть м'€ча
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void BallBounce(Collision2D col, float rotationMulti)
    {
        // ќтримуЇмо нормальний вектор з≥ткненн€
        Vector2 normal = col.GetContact(0).normal;
        
        // ¬изначаЇмо напр€мок з≥ткненн€ за допомогою нормал≥
        if (Math.Round(normal.x) != 0)
        {
            direction.x *= -1.0f;   
        }
        else if (col.gameObject.CompareTag("Platform"))
        {
            direction.x *= -1.0f * UnityEngine.Random.Range(0.0f, 2.0f);
            direction.x += 0.2f;
        }
        if (Math.Round(normal.y) != 0)
        {
            direction.y *= -1.0f;
        }
        
        // «адаЇмо нову швидк≥сть м'€ча
        rb.velocity = direction * speed;

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 15f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            BallBounce(collision, 0);
            Destroy(collision.gameObject);
            if (BrickPlacerController.brickAmount > 1)
            {
                BrickPlacerController.brickAmount--;
                
            }
            else
            {
                BrickPlacerController.brickAmount=0;
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            BallBounce(collision, 1.0f);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            
            BallBounce(collision, 0);
            
        }
        else if (collision.gameObject.CompareTag("BottomBorder"))
        {
            
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
