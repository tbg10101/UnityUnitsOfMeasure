using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Software10101.Units.Editor {
    [CustomPropertyDrawer(typeof(Area))]
    public class AreaPropertyDrawer : PropertyDrawer {
        private static readonly Tuple<string, Area>[] Units = {
            new Tuple<string, Area>("km²", Area.SquareKilometer)
        };

        private static readonly string[] UnitPopupOptions = Units.Select(element => element.Item1).ToArray();

        private static int _unitIndex = 0; // note that this will affect all visible inspectors

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(rect, label, property);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);

            SerializedProperty kmSquaredProperty = property.FindPropertyRelative("_kmSquared");

            Area currentValue = Area.From(kmSquaredProperty.doubleValue, Area.SquareKilometer);

            (double newDouble, int newIndex) = PropertyDrawerUtils.DrawProperty(
                rect,
                currentValue.To(Units[_unitIndex].Item2),
                _unitIndex,
                UnitPopupOptions);

            kmSquaredProperty.doubleValue = Area.From(newDouble, Units[_unitIndex].Item2).To(Area.SquareKilometer);
            _unitIndex = newIndex;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
