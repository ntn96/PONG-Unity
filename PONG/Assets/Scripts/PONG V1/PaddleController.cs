using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong
{
    //Autor : Antonio Serrano Miralles
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D actualRigidbody;
        [SerializeField] private bool playerOne = true;
        [SerializeField] private float velocity;
        [SerializeField] private bool controlledByAI = false;
        [SerializeField] private float clampY = 0.3f;

        public bool ControlledByAI
        {
            set
            {
                controlledByAI = value;
            }
        }

        public float Velocity
        {
            get
            {
                return velocity;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (actualRigidbody == null) actualRigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (controlledByAI) return;
            float inputValue = 0;
            if (playerOne && GameManager.instance.MatchStarted)
            {
                float value1 = Input.GetAxis("Vertical");
                float value2 = Input.GetAxis("Vertical 3");
                if (Mathf.Abs(value1) > Mathf.Abs(value2)) inputValue = value1;
                else inputValue = value2;
            }
            else if (GameManager.instance.MatchStarted)
            {
                float value1 = Input.GetAxis("Vertical 2");
                float value2 = Input.GetAxis("Vertical 4");
                if (Mathf.Abs(value1) > Mathf.Abs(value2)) inputValue = value1;
                else inputValue = value2;
            }
            actualRigidbody.velocity = Vector2.up * inputValue * velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {

                BallBehaviour ball = collision.gameObject.GetComponent<BallBehaviour>();
                if (Mathf.Abs(actualRigidbody.velocity.y) < 0.5f) //Paddle is considered stopped
                {
                    ball.HitBall(new Vector2(-ball.ActualDirection.x,ball.ActualDirection.y));
                } else if (Mathf.Sign(actualRigidbody.velocity.y) != Mathf.Sign(ball.ActualDirection.y)) //Paddle is considered moving opposite to ball direction
                {
                    Vector2 direction = new Vector2(-ball.ActualDirection.x, -ball.ActualDirection.y);
                    direction += actualRigidbody.velocity;
                    direction = direction.normalized;
                    if (Mathf.Abs(direction.y) >= clampY)
                    {
                        direction.y = Mathf.Sign(direction.y) * clampY;
                        direction = direction.normalized;
                    }
                    ball.HitBall(direction);
                } else //Paddle is considered moving in the ball direction 
                {
                    Vector2 direction = ball.ActualDirection + actualRigidbody.velocity;
                    direction.x = -direction.x;
                    direction = direction.normalized;
                    if (Mathf.Abs(direction.y) >= clampY)
                    {
                        direction.y = Mathf.Sign(direction.y) * clampY;
                        direction = direction.normalized;
                    }
                    ball.HitBall(direction);
                }
                if (playerOne) GameManager.instance.NotifyHit(1);
                else GameManager.instance.NotifyHit(2);
            }
        }
    }
}
