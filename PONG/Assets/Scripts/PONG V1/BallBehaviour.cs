using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong
{
    //Autor : Antonio Serrano Miralles
    public class BallBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D actualRigidbody;
        [SerializeField] private Vector2 actualDirection;
        [SerializeField] private float velocity = 10f;
        [SerializeField] private bool right = false;
        // Start is called before the first frame update
        
        public Vector2 ActualDirection
        {
            get
            {
                return actualDirection;
            }
        }

        public float Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        void Start()
        {
            if (actualRigidbody == null) actualRigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        public IEnumerator InitMatch()
        {
            yield return new WaitForSeconds(2f);
            CalculateStartingAngle();
            actualRigidbody.AddForce(actualDirection * velocity);
        }

        private void CalculateStartingAngle()
        {
            float y = Random.Range(-1f, 1f);
            if (right)
            {
                actualDirection = new Vector2(1f, y);
                if (GameManager.instance.ActualMode == PONGMode.SinglePlayer)
                    GameManager.instance.ServeToAI(actualDirection.normalized);
            }
            else actualDirection = new Vector2(-1f, y);
            actualDirection = actualDirection.normalized;
        }

        public void HitBall(Vector2 direction)
        {
            actualDirection = direction;
            actualRigidbody.velocity = Vector2.zero;
            actualRigidbody.AddForce(direction * velocity);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Side")
            {
                Vector2 reflection = new Vector2(actualDirection.x, -actualDirection.y);
                actualDirection = reflection;
                actualRigidbody.velocity = Vector2.zero;
                actualRigidbody.AddForce(reflection * velocity);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Goal")
            {
                actualRigidbody.velocity = Vector2.zero;
                right = !right;
                collision.gameObject.GetComponent<GoalBehaviour>().Goal();
                gameObject.transform.position = Vector2.zero;
                StartCoroutine(ThrowBall());
            }
        }

        IEnumerator ThrowBall()
        {
            yield return new WaitForSeconds(2f);
            CalculateStartingAngle();
            actualRigidbody.AddForce(actualDirection * velocity);
        }
    }
}