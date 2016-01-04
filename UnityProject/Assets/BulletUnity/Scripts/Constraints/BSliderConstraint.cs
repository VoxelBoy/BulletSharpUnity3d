﻿using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BSliderConstraint : BTwoFrameConstraint {
        [Header("Limits")]
        public float lowerLinearLimit = -10f;
        public float upperLinearLimit = 10f;
        public float lowerAngularLimit = -Mathf.PI;
        public float upperAngularLimit = Mathf.PI;

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal override bool _BuildConstraint() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (constraintPtr != null) {
                if (isInWorld && world != null) {
                    isInWorld = false;
                    world.RemoveConstraint(constraintPtr);
                }
            }
            if (IsValid())
            {
                if (constraintType == ConstraintType.constrainToAnotherBody)
                {
                    constraintPtr = new SliderConstraint(targetRigidBodyA.GetRigidBody(), targetRigidBodyB.GetRigidBody(), frameInA.CreateBSMatrix(), frameInB.CreateBSMatrix(), false);
                }
                else
                {
                    constraintPtr = new SliderConstraint(targetRigidBodyA.GetRigidBody(), frameInA.CreateBSMatrix(), false);
                }
                SliderConstraint sl = (SliderConstraint)constraintPtr;
                sl.LowerLinearLimit = lowerLinearLimit;
                sl.UpperLinearLimit = upperLinearLimit;

                sl.LowerAngularLimit = lowerAngularLimit;
                sl.UpperAngularLimit = upperAngularLimit;
                return true;
            }
            return false;
        }
    }
}