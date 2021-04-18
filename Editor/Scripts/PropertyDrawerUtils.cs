using UnityEditor;
using UnityEngine;

namespace Software10101.Units.Editor {
    public static class PropertyDrawerUtils {
        private const float UnitWidth = 60.0f;

        internal static (double, int) DrawProperty(Rect rect, double currentValue, int currentUnitIndex, string[] unitOptions) {
            Rect fieldRect = new Rect(
                rect.x,
                rect.y,
                rect.width - PropertyDrawerUtils.UnitWidth - 1.0f,
                rect.height);
            double newValue = EditorGUI.DoubleField(fieldRect, currentValue);

            Rect unitRect = new Rect(
                rect.x + rect.width - PropertyDrawerUtils.UnitWidth + 1.0f,
                rect.y,
                PropertyDrawerUtils.UnitWidth,
                rect.height);
            int newUnitIndex = EditorGUI.Popup(unitRect, currentUnitIndex, unitOptions);

            return (newValue, newUnitIndex);
        }
    }
}
