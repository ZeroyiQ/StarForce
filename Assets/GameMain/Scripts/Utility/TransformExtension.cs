using UnityEngine;

namespace BinBall
{
    public static class TransformExtension
    {
        /// <summary>
        /// 增加相对旋转的 x 分量
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="deltaValue"></param>
        public static void AddLocalRoationX(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localEulerAngles;
            v.x += deltaValue;
            transform.localEulerAngles = v;
        }

        /// <summary>
        /// 增加相对旋转的 x 分量
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="deltaValue"></param>
        public static void AddLocalRoationY(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localEulerAngles;
            v.y += deltaValue;
            transform.localEulerAngles = v;
        }

        /// <summary>
        /// 增加相对旋转的 x 分量
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="deltaValue"></param>
        public static void AddLocalRoationZ(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localEulerAngles;
            v.z += deltaValue;
            transform.localEulerAngles = v;
        }
    }
}