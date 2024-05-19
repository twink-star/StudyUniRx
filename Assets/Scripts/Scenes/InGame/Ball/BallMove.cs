using Scenes.InGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace Scenes.InGame.Ball
{
    [RequireComponent(typeof(BallStatus), typeof(Rigidbody2D))]
    public class BallMove : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        [SerializeField, Tooltip("初速")]
        private float _power;
        private BallStatus _ballStatus;
        private Vector2 _pastVelocity;
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _ballStatus = GetComponent<BallStatus>();
            _velocity = new Vector2(1, 1).normalized;
            _rigidbody2D.AddForce(_velocity * _power, ForceMode2D.Impulse);
            InGameManager.Instance.OnPause
                .Subscribe(_ =>
                {
                    Pause();
                }).AddTo(this);
            InGameManager.Instance.OnRestart
                .Subscribe(_ =>
                {
                    Restart();
                }).AddTo(this);


            _ballStatus.IsMovable.Subscribe(x =>
            {
                if (x == false)
                {
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
            );

        }
        //【完了】TODO:現在UpdateでずっとBallStatusを参照し続けています。イベント機能を使って、IsMovableの値が変更されたときだけ下の処理を実行するように変更してみましょう
        private void Update()
        {
           // if (_ballStatus.IsMovable == false)
           // {
          //      _rigidbody2D.velocity = Vector2.zero;
          //  }
        }

        private void Pause()
        {
            _pastVelocity = _rigidbody2D.velocity;//現在の移動方向を記録する
            _rigidbody2D.velocity = Vector2.zero;//移動を止める
        }

        private void Restart()
        {
            _rigidbody2D.AddForce(_pastVelocity.normalized * _power, ForceMode2D.Impulse);//過去の移動方向に力を加える
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DeadFrame"))
            {
                InGameManager.Instance.GameOver();
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                _rigidbody2D.velocity = Vector2.zero;
                var boundVelocity = transform.position - collision.gameObject.transform.position;
                _rigidbody2D.AddForce(boundVelocity.normalized * _power, ForceMode2D.Impulse);
            }
        }
    }

}