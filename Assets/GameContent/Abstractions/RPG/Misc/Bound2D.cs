using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.RPG.Misc
{
    [System.Serializable]
    public class Bound2D
    {
        public static readonly Bound2D zero = new Bound2D();

        public float XLow;
        public float XHigh;
        public float YLow;
        public float YHigh;
        public float XOffset;
        public float YOffset;

        public float Width
        {
            get { return XHigh - XLow; }
        }

        public float Height
        {
            get { return YHigh - YLow; }
        }

        public Vector2 Center
        {
            get { return new Vector2((XLow + XHigh + XOffset) / 2f, (YLow + YHigh + YOffset) / 2f); }
        }

        public float CenterX => (XLow + XHigh + XOffset) / 2f;
        public float CenterY => (YLow + YHigh + YOffset) / 2f;
        public float MidRightX => CenterX + Width / 2f;
        public float MidLeftX => CenterX - Width / 2f;
        public float MidTopX => CenterX;
        public float MidBotX => CenterX;
        public float MidRightY => CenterY;
        public float MidLeftY => CenterY;
        public float MidTopY => CenterY + Height / 2f;
        public float MidBotY => CenterY - Height / 2f;

        public Bound2D()
        {
            XLow = 0f;
            XHigh = 0f;
            YLow = 0f;
            YHigh = 0f;
            XOffset = 0f;
            YOffset = 0f;
        }

        public Bound2D(float xLow, float xHigh, float yLow, float yHigh, float xOffset, float yOffset)
        {
            XLow = xLow;
            XHigh = xHigh;
            YLow = yLow;
            YHigh = yHigh;
            XOffset = xOffset;
            YOffset = yOffset;
        }

        public void Set(float xLow, float xHigh, float yLow, float yHigh, float xOffset, float yOffset)
        {
            XLow = xLow;
            XHigh = xHigh;
            YLow = yLow;
            YHigh = yHigh;
            XOffset = xOffset;
            YOffset = yOffset;
        }

        public bool Contains(Vector3 position)
        {
            return position.x >= XLow && position.x <= XHigh && position.y >= YLow && position.y <= YHigh;
        }

        public Vector3 RandomPointInBound2D()
        {
            Vector3 v = Vector3.zero;
            float randX = UnityEngine.Random.Range(XLow, XHigh);
            float randY = UnityEngine.Random.Range(YLow, YHigh);
            v.x = CenterX + randX;
            v.y = CenterY + randY;
            return v;
        }

        public void Copy(Bound2D bounds)
        {
            XLow = bounds.XLow;
            XHigh = bounds.XHigh;
            YLow = bounds.YLow;
            YHigh = bounds.YHigh;
            XOffset = bounds.XOffset;
            YOffset = bounds.YOffset;
        }

#if UNITY_EDITOR
        public void DrawBounds(Color color, float delay = 0)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(Center, new Vector3(Width, Height, 0));
        }
#endif
    }
}
