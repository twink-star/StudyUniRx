using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes.InGame.Ball;
using Scenes.InGame.Stick;
using TMPro;
using System;
using UniRx;
using UniRx.Triggers;

namespace Scenes.InGame.Manager
{
    public class InGameManager : MonoBehaviour
    {
        BallSpawner _ballSpawner;
        BallStatus _ballStatus;
        StickStatus _stickStatus;
        public static InGameManager Instance;
        private int _score = 0;//�X�R�A
        private int _blockSize = 0;//block�̐�
        [SerializeField, Tooltip("�X�R�A��\������UI")]
        TextMeshProUGUI _socreText;

        private Subject<Unit> Pause = new Subject<Unit>();
        public IObservable<Unit> OnPause => Pause;
        private Subject<Unit> Restart = new Subject<Unit>();
        public IObservable<Unit> OnRestart => Restart;

        private Subject<Unit> Spawn = new Subject<Unit>();
        public IObservable<Unit> OnSpawn => Spawn;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            _ballSpawner = GetComponent<BallSpawner>();
            StartCoroutine(BallSpawn());
        }


        IEnumerator BallSpawn()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            Spawn.OnNext(default);
        }


    public void GameOver()
        {
            _ballStatus = FindObjectOfType<BallStatus>();
            _stickStatus = FindObjectOfType<StickStatus>();
            _ballStatus.StopMove();
            _stickStatus.StopMove();
        }
        public void BlockSize(int i)
        {
            _blockSize = i;
        }

        public void GamePause()
        {
            Pause.OnNext(default);
        }
        public void GameRestart()
        {
            Restart.OnNext(default);
        }
    public void BlockDestroy()
        {
            _score += 100;
            _blockSize--;
            _socreText.text = $"SCORE:{_score}";
            if (_blockSize <= 0)
            {
                GameOver();
            }
        }
    }

}