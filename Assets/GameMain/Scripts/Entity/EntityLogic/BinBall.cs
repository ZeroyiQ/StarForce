using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class BinBall : Entity
    {
        private BinballData m_Data;
        private Collider m_Collider;
        private Rigidbody m_Rigidbody;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Collider = GetComponent<SphereCollider>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_Data = userData as BinballData;
            if (m_Data == null)
            {
                Log.Error("Binball data is invalid.");
                return;
            }
            if (m_Collider != null)
            {

            }
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }



        protected override void OnRecycle()
        {
            base.OnRecycle();
        }



        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}