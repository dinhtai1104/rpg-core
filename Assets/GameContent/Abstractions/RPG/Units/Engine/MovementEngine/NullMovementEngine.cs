using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Movement
{
    public class NullMovementEngine : BaseNullEngine, IMovementEngine
    {
        public float DirectionSign => 0;

        public Vector3 FacingDirection { set; get; }

        public AttributeData Speed { set; get; }
        public bool LockMovement { set; get; }
        public bool LockFacing { set; get; }
        public bool UsingHorizontalBound { set; get; }
        public bool UsingVerticalBound { set; get; }
        public bool IsMoving { set; get; }
        public bool Static { set; get; }

        public Bound2D MovementBound { set; get; }


        public bool ReachBoundLeft { set; get; }

        public bool ReachBoundRight { set; get; }

        public bool ReachBoundTop { set; get; }
        public bool ReachBoundBottom { set; get; }

        public bool ReachBound { set; get; }

        public Vector3 CurrentDirection { set; get; }

        public Vector3 CurrentPosition { set; get; }

        public Vector3 Bound(Vector3 position)
        {
            return new();
        }

        public void Bound()
        {
        }

        public void CopyState(IMovementEngine movementEngine)
        {
        }

        public void FlipFacingDirection()
        {
        }

        public void Init(CharacterActor actor)
        {
        }

        public void LookAt(Vector3 position)
        {
        }

        public bool MoveDirection(Vector3 direction)
        {
            return false;
        }

        public bool MoveDirection(Vector3 direction, float speedMul)
        {
            return false;
        }

        public bool MoveTo(Vector3 position)
        {
            return false;
        }

        public bool MoveTo(Vector3 position, float speedMul)
        {
            return false;
        }

        public void MoveTween(Vector3 dest, float duration)
        {
        }

        public void OnUpdate()
        {
        }

        public void SetBound(Bound2D bound)
        {
        }

        public void SetDirection(Vector3 dir)
        {
        }

        public void SyncGraphicRotation(Vector3 dir)
        {
        }

        public void Teleport(Vector3 pos)
        {
        }
    }
}
