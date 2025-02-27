using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    public static class Settings
    {
        /// <summary> Console messages. </summary>
        public static class Log
        {
            public static readonly LogLevel DefaultLevel = LogLevel.Info;

            public static readonly Color InfoColor = new Color(0.2f, 0.8f, 0.2f);
            public static readonly Color WarningColor = new Color(1.0f, 0.52f, 0.29f);
            public static readonly Color ErrorColor = new Color(1.0f, 0.2f, 0.47f);
        }

        /// <summary> FPS calculation. </summary>
        public static class FPS
        {
            public static readonly float WaitingToStartCounting = 2.0f;
            public static readonly int UpdatePerSecond = 2;
            public static readonly int HistoryFrames = 100;

            public static readonly int WarningFPS = 30;
            public static readonly int BadFPS = 20;

            public static readonly Color GoodColor = new Color(0.2f, 0.8f, 0.2f);
            public static readonly Color WarningColor = new Color(0.4f, 0.4f, 0.2f);
            public static readonly Color BadColor = new Color(0.8f, 0.2f, 0.2f);
        }

        /// <summary> Debug Draw. </summary>
        public static class DebugDraw
        {
            public static readonly bool DrawInSceneView = true;
            public static readonly bool DrawInGameView = true;

            public static readonly int Capacity = 5000;
            public static readonly string Profiler = "Abstractions.Shared.Foundation.DebugDraw";
            public static readonly int TextCapacity = 100;
            public static readonly float Transparency = 0.7f;
            public static readonly float OccludedColor = 0.1f;
            public static readonly float PointSize = 0.5f;
            public static readonly int Divisions = 64;
            public static readonly Color AxisXColor = Color.red;
            public static readonly Color AxisYColor = Color.green;
            public static readonly Color AxisZColor = Color.blue;
            public static readonly Color LineColor = new Color(0.941f, 0.796f, 0.788f);

            public static readonly float LineGapSize = 0.05f;
            public static readonly Color ArrowColor = new Color(0.043f, 0.561f, 0.988f);
            public static readonly float ArrowTipSize = 0.25f;
            public static readonly float ArrowWidth = 0.5f;
            public static readonly Color RayColor = "#a94241".FromHex();
            public static readonly float RayLength = 1000.0f;
            public static readonly Color HitColor = "#C91211".FromHex();
            public static readonly float HitRadius = 0.1f;
            public static readonly float HitLength = 0.25f;
            public static readonly Color CircleColor = "#affff1".FromHex();
            public static readonly Color CubeColor = "#bee6e4".FromHex();
            public static readonly Color SphereColor = "#f5df8c".FromHex();
            public static readonly Color ArcColor = "#90d3be".FromHex();
            public static readonly Color DiamondColor = "#f5d59a".FromHex();
            public static readonly float DiamondSize = 0.5f;
            public static readonly Color ConeColor = "#f3ed80".FromHex();
            public static readonly Color BoundsColor = "#f1c59c".FromHex();
        }
#if UNITY_EDITOR
        /// <summary> Editor styles. </summary>
        public static class Editor
        {
            public static readonly Color ErrorColor = Color.red;
            public static readonly float FileButtonWidth = 20.0f;
            public static readonly float FileButtonPadding = 4.0f;
            public static readonly float MinMaxFieldWidth = 30.0f;
            public static readonly string MessageBoxInfoIcon = "console.infoicon";
            public static readonly string MessageBoxWarningIcon = "console.warnicon";
            public static readonly string MessageBoxErrorIcon = "console.erroricon";
            public static readonly int TitleSpaceBeforeTitle = 8;
            public static readonly int TitleSpaceBeforeLine = 2;
            public static readonly int TitleLineHeight = 2;
            public static readonly int TitleSpaceBeforeContent = 3;
            public static readonly string RefreshIcon = "d_Refresh";

            public const float SpaceSeparation = 5.0f;
        }
#endif
    }
}