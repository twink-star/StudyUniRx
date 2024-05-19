using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Scenes.InGame.Stick
{
    [RequireComponent(typeof(StickStatus), typeof(Rigidbody2D))]
    public class StickMove : MonoBehaviour
    {
        private StickStatus _stickStatus;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        private const int CORRECTIONVALUE = 10;//数値を調整するための補正値です
        private Vector2 _mooveVelocity;
        float moveSpeed;
        bool isMovable = true;


        void Start()
        {
            _stickStatus = GetComponent<StickStatus>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _stickStatus.MoveSpeed.Subscribe(x => moveSpeed = x).AddTo(this);

            _stickStatus.IsMovable.Subscribe(x=>
            {
                if (x == false)
                {
                    isMovable = false;
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
            );

        }
        void FixedUpdate()
        {
            //TODO:【完了】現在はStickMoveが毎回StickStatusに参照しています。この部分をUniRxを使って、値が変わった時だけアクセスするようにしてみましょう
            // if (_stickStatus.IsMovable == false)
            //{
            //  _rigidbody2D.velocity = Vector2.zero;
            // return;
            // }  

            _velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow)&& isMovable == true)
            {
                _velocity.x--;
            }
            if (Input.GetKey(KeyCode.RightArrow) && isMovable == true)
            {
                _velocity.x++;
            }

            //【完了】TODO:現在はStickMoveが毎回StickStatusに参照しています。この部分をUniRxを使って、値が変わった時だけアクセスするようにしてみましょう
            Vector2 _mooveVelocity = _velocity * moveSpeed;
            _rigidbody2D.velocity = _mooveVelocity * Time.fixedDeltaTime * CORRECTIONVALUE;
        }


        //テスト用
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //ReactivePropertyを使う際には変数名.Value(_spaceCount.Value)を用いる
                _stickStatus.MoveSpeed.Value++;
            }
        }

    }
}