using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class Beeper : MonoBehaviour
    {
        public Vector2Int Position { get; set; }

        [SerializeField]
        private LightBlinker _lightBlinker = null;

        public void SetColor(Color color)
        {
            _lightBlinker.SetLightColor(color);

            foreach (var material in GetComponentsInChildren<Renderer>().Select(r => r.material))
            {
                material.color = color;
            }
        }

        public Color GetColor()
        {
            return _lightBlinker.GetLightColor();
        }
    }
}
