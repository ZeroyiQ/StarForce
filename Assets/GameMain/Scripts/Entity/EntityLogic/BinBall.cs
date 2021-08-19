using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class BinBall : Entity
    {
        [SerializeField]
        private BinballData m_Data;
        private float m_Hight;
        private Collider m_Collider;
        private Rigidbody m_Rigidbody;

        private Vector3 m_PreVelocity = Vector3.zero;
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Hight = 0.5f;
        }
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Collider = GetComponent<SphereCollider>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Hight = 2;
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
                var material = m_Collider.material;
                if (material != null)
                {
                    material.dynamicFriction = m_Data.Friction;
                    material.bounciness = m_Data.Bounciness;
                    Log.Info("Modify BinBall Physical Material.");
                }
            }
            if (m_Rigidbody != null)
            {
                m_Rigidbody.mass = m_Data.Mass;
                m_Rigidbody.drag = m_Data.Drag;
                Log.Info("Modify BinBall Rigidbody.");
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


        private void LateUpdate()
        {
            m_PreVelocity = m_Rigidbody.velocity;
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        private void OnCollisionEnter(Collision other)
        {
            float vy = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y * m_Hight * 2));
            ContactPoint contactPoint = other.contacts[0];

            Vector3 newDir = Vector3.Reflect(Vector3.down, contactPoint.normal);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
            transform.rotation = rotation;
            m_Rigidbody.velocity = newDir.normalized * m_PreVelocity.y / m_PreVelocity.normalized.y * 0.8f;
            Debug.LogError($"{m_Rigidbody.velocity.ToString()}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EndPoint")
            {
                Destroy(this.gameObject);
                Debug.LogError($"Game End.");
            }
        }
    }
}