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
        private const int CORRECTIONVALUE = 10;//���l�𒲐����邽�߂̕␳�l�ł�
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
            //TODO:�y�����z���݂�StickMove������StickStatus�ɎQ�Ƃ��Ă��܂��B���̕�����UniRx���g���āA�l���ς�����������A�N�Z�X����悤�ɂ��Ă݂܂��傤
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

            //�y�����zTODO:���݂�StickMove������StickStatus�ɎQ�Ƃ��Ă��܂��B���̕�����UniRx���g���āA�l���ς�����������A�N�Z�X����悤�ɂ��Ă݂܂��傤
            Vector2 _mooveVelocity = _velocity * moveSpeed;
            _rigidbody2D.velocity = _mooveVelocity * Time.fixedDeltaTime * CORRECTIONVALUE;
        }


        //�e�X�g�p
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //ReactiveProperty���g���ۂɂ͕ϐ���.Value(_spaceCount.Value)��p����
                _stickStatus.MoveSpeed.Value++;
            }
        }

    }
}