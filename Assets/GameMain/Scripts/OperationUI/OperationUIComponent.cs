using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class OperationUIComponent : GameFrameworkComponent
    {
        [SerializeField]
        private RotationUI m_RotationOpUI;

        [SerializeField]
        private Transform m_InstanceRoot = null;

        [SerializeField]
        private int m_InstancePoolCapacity = 4;

        private IObjectPool<OperationUIObject> m_OperationPool = null;
        private List<OperationUI> m_ActiveOperation = null;
        private Canvas m_CachedCanvas = null;

        public void ShowRotationUI(Entity entity)
        {
            RotationUI ui = GetActiveOperation(entity) as RotationUI;
            if (ui == null)
            {
                ui = CreateRotationUI(entity);
                m_ActiveOperation.Add(ui);
            }
            ui.Init(entity, m_CachedCanvas);
        }

        public void HideRotationUI(RotationUI ui)
        {
            HideOperationUI(ui);
        }

        public void HideOperationUI(OperationUI ui)
        {
            ui.Reset();
            m_ActiveOperation.Remove(ui);
            m_OperationPool.Unspawn(ui);
        }

        #region cycle life

        private void Start()
        {
            m_CachedCanvas = m_InstanceRoot.GetComponent<Canvas>();
            m_OperationPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<OperationUIObject>("OperationUI", m_InstancePoolCapacity);
            m_ActiveOperation = new List<OperationUI>();
        }

        private void Update()
        {
            for (int i = m_ActiveOperation.Count - 1; i >= 0; i--)
            {
                OperationUI ui = m_ActiveOperation[i];
                if (ui.Refresh())
                {
                    continue;
                }

                HideOperationUI(ui);
            }
        }

        #endregion cycle life

        #region private

        private OperationUI GetActiveOperation(Entity entity)
        {
            if (entity == null)
            {
                return null;
            }

            for (int i = 0; i < m_ActiveOperation.Count; i++)
            {
                if (m_ActiveOperation[i].Owner == entity)
                {
                    return m_ActiveOperation[i];
                }
            }

            return null;
        }

        private RotationUI CreateRotationUI(Entity entity)
        {
            RotationUI ui = null;
            OperationUIObject obj = m_OperationPool.Spawn();
            if (obj != null)
            {
                ui = obj.Target as RotationUI;
            }
            else
            {
                ui = Instantiate(m_RotationOpUI);
                Transform transform = ui.GetComponent<Transform>();
                transform.SetParent(m_InstanceRoot);
                transform.localScale = Vector3.one;
                m_OperationPool.Register(OperationUIObject.Create(ui), true);
            }
            return ui;
        }

        #endregion private
    }
}