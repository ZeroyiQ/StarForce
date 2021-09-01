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
        public float Score
        {
            get;
            private set;
        }

        public void Reset()
        {
            Visible = true;
            Score = 0;
            transform.position = m_Data.StartPosition;
            m_Rigidbody.velocity = Vector3.zero;
        }

        public void PauseBall()
        {
            m_Rigidbody.useGravity = false;
            m_Rigidbody.mass = 0;
            m_Rigidbody.velocity = Vector3.zero;
        }

        public void ResumeBall()
        {
            m_Rigidbody.useGravity = true;
            m_Rigidbody.mass = m_Data.Mass;
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Collider = GetComponent<SphereCollider>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Hight = 0.5f;
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
            if (m_Rigidbody != null)
            {
                m_Rigidbody.mass = m_Data.Mass;
                m_Rigidbody.drag = m_Data.Drag;
                Log.Info("Modify BinBall Rigidbody.");
            }
            Score = 0;
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
            //float vy = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y * m_Hight * 2));
            ContactPoint contactPoint = other.contacts[0];

            Vector3 newDir = Vector3.Reflect(m_PreVelocity.normalized, contactPoint.normal);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
            transform.rotation = rotation;
            m_Rigidbody.velocity = newDir.normalized * m_PreVelocity.y / m_PreVelocity.normalized.y * 0.65f;
            Log.Info($"{m_Rigidbody.velocity.ToString()}");
            if (other.gameObject.tag == "Wall")
            {
                Score += 10;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EndPoint")
            {
                GameEntry.Entity.HideEntity(this);
                Log.Info($"End Game.");
            }
        }
    }
}