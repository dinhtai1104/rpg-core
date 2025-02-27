using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Camera extensions. </summary>
    public static class CameraExtensions
    {
        /// <summary> GUI position to world position. </summary>
        /// <param name="guiPosition"> GUI space position. </param>
        /// <returns>World position.</returns>
        public static Vector3 GUIPositionToWorldPosition(this Camera self, Vector2 guiPosition) =>
            self.ScreenPointToRay(guiPosition).GetPoint(0.0f);

        /// <summary> GUI offset to world position. </summary>
        /// <param name="guiDelta"> GUI delta position. </param>
        /// <returns>World position.</returns>
        public static Vector3 GUIDeltaToWorldDelta(this Camera self, Vector2 guiDelta)
        {
            Vector3 screenDelta = GUIUtility.GUIToScreenPoint(guiDelta);
            Ray worldRay = self.ScreenPointToRay(screenDelta);

            Vector3 worldDelta = worldRay.GetPoint(0.0f);
            worldDelta -= self.ScreenPointToRay(Vector3.zero).GetPoint(0.0f);

            return worldDelta;
        }

        /// <summary> Is object visible? </summary>
        /// <param name="renderer">Renderer object.</param>
        /// <returns> True or false. </returns>
        public static bool IsObjectVisible(this Camera self, Renderer renderer) =>
            GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(self), renderer.bounds);

        /// <summary> Is point visible? </summary>
        /// <param name="point">Point.</param>
        /// <returns> True or false. </returns>
        public static bool IsPointVisible(this Camera self, Vector3 point)
        {
            Vector3 p = self.WorldToViewportPoint(point);

            return p.x >= 0.0f && p.x <= 1.0f && p.y >= 0.0f && p.y <= 1.0f;
        }

        /// <summary> Visible rectangle. </summary>
        /// <returns>Orthographic rect.</returns>
        public static Rect OrthographicVisibleRect(this Camera self)
        {
            return new Rect(self.transform.position - new Vector3(self.aspect * self.orthographicSize, self.orthographicSize),
                new Vector2
                (
                    self.aspect * self.orthographicSize * 2.0f,
                    self.orthographicSize * 2.0f
                ));
        }
    }
}