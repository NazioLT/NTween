#if UNITY_EDITOR
using UnityEngine;
using Nazio_LT.Tools.Core.Internal;
using UnityEditor;

namespace Nazio_LT.Tools.NTween.Editor
{
    [CustomPropertyDrawer(typeof(NTweenerHandler))]
    public class NTweenerHandlerEditor : NPropertyDrawer
    {
        private SerializedProperty m_open_prop;

        private SerializedProperty m_type_prop;
        private SerializedProperty m_transform_prop, m_target_prop, m_delta_prop, m_duration_prop;
        private SerializedProperty m_otherPropsOpen_prop;
        private SerializedProperty m_loop_prop, m_pingpong_prop;

        private SerializedProperty[] m_persistantsProps;
        private SerializedProperty[] m_otherProps;

        private SerializedProperty[] m_ntMoveToProps;
        private SerializedProperty[] m_ntMoveProps;
        private SerializedProperty[] m_ntRotateToProps;
        private SerializedProperty[] m_ntRotateProps;
        private SerializedProperty[] m_ntScaleToProps;

        protected override void DefineProps(SerializedProperty property)
        {
            m_open_prop = property.FindPropertyRelative("m_open");

            if (!m_open_prop.boolValue) return;

            m_type_prop = property.FindPropertyRelative("m_type");

            m_transform_prop = property.FindPropertyRelative("m_transform");
            m_target_prop = property.FindPropertyRelative("m_target");
            m_delta_prop = property.FindPropertyRelative("m_delta");
            m_duration_prop = property.FindPropertyRelative("m_duration");

            m_otherPropsOpen_prop = property.FindPropertyRelative("m_otherPropsOpen");
            m_loop_prop = property.FindPropertyRelative("m_loop");
            m_pingpong_prop = property.FindPropertyRelative("m_pingpong");

            DefinePropGroups();
        }

        protected override void DrawGUI(Rect position, SerializedProperty property, GUIContent label, ref float propertyHeight, ref Rect baseRect)
        {
            bool initialOpenValue = m_open_prop.boolValue;
            m_open_prop.boolValue = EditorGUI.BeginFoldoutHeaderGroup(baseRect, m_open_prop.boolValue, property.displayName);
            NEditor.AdaptGUILine(ref baseRect, ref propertyHeight, 1);

            if (!initialOpenValue) return;

            //Main props
            NEditor.DrawMultipleGUIClassic(baseRect, NEditor.SINGLE_LINE, m_persistantsProps);
            NEditor.AdaptGUILine(ref baseRect, ref propertyHeight, 3);

            NTweenType selectedType = (NTweenType)m_type_prop.intValue;

            SerializedProperty[] propsToDisplay = NTweenTypeFactory(selectedType);

            NEditor.DrawMultipleGUIClassic(baseRect, NEditor.SINGLE_LINE, propsToDisplay);
            NEditor.AdaptGUILine(ref baseRect, ref propertyHeight, propsToDisplay.Length);

            //Other props
            m_otherPropsOpen_prop.boolValue = EditorGUI.Foldout(baseRect, m_otherPropsOpen_prop.boolValue, "Other properties");
            NEditor.AdaptGUILine(ref baseRect, ref propertyHeight, 1);

            if(!m_otherPropsOpen_prop.boolValue) return;

            NEditor.DrawMultipleGUIClassic(baseRect, NEditor.SINGLE_LINE, m_otherProps);
            NEditor.AdaptGUILine(ref baseRect, ref propertyHeight, m_otherProps.Length);
        }

        private SerializedProperty[] NTweenTypeFactory(NTweenType selectedType)
        {
            switch (selectedType)
            {
                case NTweenType.NTMoveTo:
                    return m_ntMoveToProps;

                case NTweenType.NTMove:
                    return m_ntMoveProps;

                case NTweenType.NTRotateTo:
                    return m_ntRotateToProps;

                case NTweenType.NTRotate:
                    return m_ntRotateProps;

                case NTweenType.NTScaleTo:
                    return m_ntScaleToProps;
            }

            return new SerializedProperty[0];
        }

        private void DefinePropGroups()
        {
            m_persistantsProps = new SerializedProperty[]
            {
                m_type_prop, m_duration_prop
            };

            m_otherProps = new SerializedProperty[]
            {
                m_loop_prop, m_pingpong_prop
            };

            m_ntMoveToProps = new SerializedProperty[]
            {
                m_transform_prop, m_target_prop
            };

            m_ntMoveProps = new SerializedProperty[]
            {
                m_transform_prop, m_delta_prop
            };

            m_ntRotateToProps = new SerializedProperty[]
            {
                m_transform_prop, m_target_prop
            };

            m_ntRotateProps = new SerializedProperty[]
            {
                m_transform_prop, m_delta_prop
            };

            m_ntScaleToProps = new SerializedProperty[]
            {
                m_transform_prop, m_target_prop
            };
        }
    }
}
#endif