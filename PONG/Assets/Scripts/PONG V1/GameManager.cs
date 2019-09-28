using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pongV2;

namespace pong {
    //Autor : Antonio Serrano Miralles
    public enum PONGMode { Unknown, SinglePlayer, Multiplayer };
    public enum Turn { Unknown, PlayerOne, PlayerTwo};

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool matchStarted = false;
        [SerializeField] private PONGMode actualMode = PONGMode.Unknown;
        [SerializeField] private List<GameObject> hidedObjectInPlay = new List<GameObject>();
        [SerializeField] private BallBehaviour ball;
        [SerializeField] private PaddleController playerOne;
        [SerializeField] private PaddleController playerTwo;
        [SerializeField] private Turn actualTurn;
        [SerializeField] private EnhancedBehaviour enhanced;
        private PONGAI aiPlayer = null;
        private Rigidbody2D ballRigid = null;
        public static GameManager instance = null;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        public bool MatchStarted
        {
            get
            {
                return matchStarted;
            }
        }

        public PONGMode ActualMode
        {
            get
            {
                return actualMode;
            }
        }

        public BallBehaviour Ball
        {
            get
            {
                return ball;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape)) Application.Quit();
        }

        public void ServeToAI(Vector2 direction)
        {
            actualTurn = Turn.PlayerTwo;
            aiPlayer.CalculatePosition(ballRigid.position, ball.ActualDirection);
        }

        public void StartSinglePlayer()
        {
            if (ballRigid == null) ballRigid = ball.gameObject.GetComponent<Rigidbody2D>();
            if (aiPlayer == null) aiPlayer = playerTwo.gameObject.GetComponent<PONGAI>(); 
            actualMode = PONGMode.SinglePlayer;
            if (enhanced != null) {
                enhanced.SetPaddleOriginalSize(playerOne.gameObject);
                enhanced.SetPaddleOriginalSize(playerTwo.gameObject);
            }
            matchStarted = true;
            HideObjects();
            playerTwo.ControlledByAI = true;
            StartCoroutine(ball.InitMatch());
        }

        public void StartMultiplayer()
        {
            actualMode = PONGMode.Multiplayer;
            if (enhanced != null)
            {
                enhanced.SetPaddleOriginalSize(playerOne.gameObject);
                enhanced.SetPaddleOriginalSize(playerTwo.gameObject);
            }
            matchStarted = true;
            HideObjects();
            StartCoroutine(ball.InitMatch());
        }

        private void HideObjects()
        {
            foreach (GameObject go in hidedObjectInPlay)
            {
                go.SetActive(false);
            }
        }

        public void NotifyHit(int player)
        {
            if(player == 1)
            {
                actualTurn = Turn.PlayerTwo;
                if (actualMode == PONGMode.SinglePlayer)
                {
                    aiPlayer.CalculatePosition(ballRigid.position, ball.ActualDirection);
                }
                if (enhanced != null) enhanced.ReducePaddle(playerOne.gameObject);
            }
            else
            {
                actualTurn = Turn.PlayerOne;
                if(enhanced !=null) enhanced.ReducePaddle(playerTwo.gameObject);
            }
            if (enhanced != null)
            {
               if (enhanced != null) enhanced.PaddleHit();
            }
        }

        public void NotifyGoal(int player)
        {
            if (enhanced != null)
            {
                if (player == 1)
                {
                    enhanced.SetPaddleOriginalSize(playerOne.gameObject);
                }
                else
                {
                    enhanced.SetPaddleOriginalSize(playerTwo.gameObject);
                }
            }
        }
    }
}