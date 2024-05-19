using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Scenes.InGame.Ball
{
    public class BallStatus : MonoBehaviour
    {
        [Header("�{�[���̃p�����[�^")]
        [SerializeField, Tooltip("�{�[���̈ړ����x")]
        private float _ballMoveSpeed;//�{�[���̈ړ����x�����߂�p�����[�^�ł�
        public float BallMoveSpeed { get => _ballMoveSpeed; }//���̃X�N���v�g����_ballMoveSpeed�̒l���Q�Ƃ������ꍇ�͂��̊֐����g���܂�

        public BoolReactiveProperty IsMovable = new BoolReactiveProperty(true);

        //private bool _isMovable = true;//�{�[�����ړ��ł��邩�ǂ����̃p�����[�^�ł�
        //public bool IsMovable { get => _isMovable; }//���̃X�N���v�g����_isMove�̒l���Q�Ƃ������ꍇ�͂��̊֐����g���܂�
        public void StopMove()
        {
            IsMovable.Value = false;
        }
    }
}