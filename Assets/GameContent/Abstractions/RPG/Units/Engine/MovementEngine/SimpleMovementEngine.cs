using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Movement
{
    public class SimpleMovementEngine : BaseMonoEngine, IMovementEngine
    {
        private System.Random m_random;

        [SerializeField] private AttributeData _speed;

        [SerializeField] private bool _lockFacingDirection;

        [SerializeField] private bool _lockMovement;

        [SerializeField] private bool _horizontalBound = true;

        [SerializeField] private bool _verticalBound = true;

        [SerializeField] private Bound2D _movementBound;


        private bool _isMoving;
        private Transform _trans;
        private Transform _graphicTrans;
        private Vector3 _lastGoodPos;

        public Transform CachedTransform => _trans;
        [ShowInInspector]
        public float DirectionSign { private set; get; }
        public Vector3 FacingDirection => Vector3.right * DirectionSign;
        public Bound2D MovementBound => _movementBound;

        public AttributeData Speed
        {
            set { _speed = value; }
            get { return _speed; }
        }

        public override bool Locked
        {
            set { _lockMovement = value; }
            get { return _lockMovement; }
        }

        public bool LockFacing
        {
            set { _lockFacingDirection = value; }
            get { return _lockFacingDirection; }
        }

        public bool UsingHorizontalBound
        {
            set { _horizontalBound = value; }
            get { return _horizontalBound; }
        }

        public bool UsingVerticalBound
        {
            get { return _verticalBound; }
            set { _verticalBound = value; }
        }

        public bool Static { set; get; }
        [ShowInInspector]
        public Vector3 CurrentDirection { private set; get; }

        public Vector3 CurrentPosition
        {
            get => _trans.position;
        }


        public bool IsMoving
        {
            set
            {
                _isMoving = value;

                if (!_isMoving)
                    CurrentDirection = FacingDirection;
            }
            get { return _isMoving; }
        }
        public bool ReachBoundLeft => _trans.position.x <= _movementBound.XLow;

        public bool ReachBoundRight => _trans.position.x >= _movementBound.XHigh;

        public bool ReachBoundTop => _trans.position.y >= _movementBound.YHigh;

        public bool ReachBoundBottom => _trans.position.y <= _movementBound.YLow;

        public bool ReachBound => ReachBoundBottom || ReachBoundLeft || ReachBoundRight || ReachBoundTop;


        public override void Initialize(ICharacter character)
        {
            base.Initialize(character);
            _trans = transform;
            _graphicTrans = character.GraphicTrans;
            SyncGraphicRotation(Vector3.right);

            m_random = new System.Random();
        }
        public void SetBound(Bound2D bound)
        {
            this._movementBound = bound;
        }
        public void Teleport(Vector3 pos)
        {
            _trans.position = pos;
        }

        public void SyncGraphicRotation(Vector3 dir)
        {
            if (_lockFacingDirection)
                return;

            DirectionSign = dir.x < 0f ? -1f : 1f;
            CurrentDirection = dir;
            _graphicTrans.localRotation = Quaternion.Euler(0f, DirectionSign >= 0f ? 0f : 180f, 0f);
        }

        public void SetDirection(Vector3 dir)
        {
            CurrentDirection = dir.normalized;
        }

        public void FlipFacingDirection()
        {
            if (_lockFacingDirection)
                return;

            DirectionSign *= -1f;
            _graphicTrans.localRotation = Quaternion.Euler(0f, DirectionSign >= 0f ? 0f : 180f, 0f);
        }

        public void LookAt(Vector3 position)
        {
            if (_lockFacingDirection)
                return;

            SyncGraphicRotation(position - CachedTransform.position);
        }

        public bool MoveTo(Vector3 position)
        {
            return MoveTo(position, Speed.Value);
        }

        public bool MoveDirection(Vector3 direction)
        {
            return MoveDirection(direction, 1);
        }

        public void CopyState(IMovementEngine movementEngine)
        {
            _lockMovement = movementEngine.Locked;
            _speed = movementEngine.Speed;
        }
        public Vector3 Bound(Vector3 position)
        {
            if (UsingHorizontalBound)
            {
                position.x = Mathf.Clamp(position.x, _movementBound.XLow, _movementBound.XHigh);
            }

            if (UsingVerticalBound)
            {
                position.y = Mathf.Clamp(position.y, _movementBound.YLow, _movementBound.YHigh);
            }

            return position;
        }

        public void Bound()
        {
            Vector3 position = _trans.position;

            if (UsingHorizontalBound)
            {
                position.x = Mathf.Clamp(position.x, _movementBound.XLow, _movementBound.XHigh);
            }

            if (UsingVerticalBound)
            {
                position.y = Mathf.Clamp(position.y, _movementBound.YLow, _movementBound.YHigh);
            }

            _trans.position = position;
        }


        private bool CheckingPositionAvailable(Vector3 position)
        {
            return MovementBound.Contains(position);
        }


        public bool MoveTo(Vector3 position, float speed)
        {
            if (Locked)
                return false;

            var curPos = CachedTransform.position;

            if (curPos.Compare(position))
                return false;

            if (!IsMoving)
                IsMoving = true;


            // add some noise to hack enemy is in same direction

            //float angle = (float)m_random.NextDouble() * 2.0f * (float)Mathf.PI;
            //float dist = (float)m_random.NextDouble() * 0.3f;
            //var noise = dist * new Vector3((float)Mathf.Cos(angle), (float)Mathf.Sin(angle));
            //position += noise;

            position = Bound(position);
            position.z = 0f;

            _lastGoodPos = curPos;

            curPos = Vector3.MoveTowards(curPos, position, Time.deltaTime * speed);
            CurrentDirection = Vector3.Normalize(curPos - _lastGoodPos);
            SyncGraphicRotation(CurrentDirection);
            CachedTransform.position = curPos;
            return true;
        }

        public bool MoveDirection(Vector3 direction, float multiply)
        {
            if (Locked)
                return false;

            if (direction.Compare(Vector3.zero))
                return false;

            if (!IsMoving)
                IsMoving = true;

            // add some noise to hack enemy is in same direction

            //float angle = (float)m_random.NextDouble() * 2.0f * (float)Mathf.PI;
            //float dist = (float)m_random.NextDouble() * 0.3f;
            //var noise = dist * new Vector3((float)Mathf.Cos(angle), (float)Mathf.Sin(angle));
            //direction += noise;

            direction.z = 0f;
            CurrentDirection = Vector3.Normalize(direction);
            SyncGraphicRotation(CurrentDirection);

            _lastGoodPos = CachedTransform.position;

            CachedTransform.Translate(Time.deltaTime * multiply * this.Speed.Value * direction, Space.World);
            Bound();
            return true;
        }

        public void MoveTween(Vector3 dest, float duration)
        {
            CachedTransform.DOMove(dest, duration).OnUpdate(() => { Bound(); });
        }
    }
}
