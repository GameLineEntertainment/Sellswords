using Sellswords;
using UnityEditor;
using System.Linq;
using System.Reflection;

namespace UnityEngine
{
    [CustomEditor(typeof(StatusObject))]
    public class StatusObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var obj = serializedObject.GetIterator();
            var enumField = target.GetType().GetField("StatusType");
            var currentStatusType = enumField.GetValue(target);

            if (obj.NextVisible(true))
            {
                do
                {
                    var shouldBeVisible = true;
                    var targetType = typeof(StatusObject);
                    var fields = targetType.GetFields();

                    foreach (var field in fields)
                    {
                        if (field.Name == obj.name)
                        {
                            var attributes = field.GetCustomAttributes<ShowInInspectorByStatusAttribute>(false).ToList();

                            if (attributes.Any(attribute => currentStatusType.ToString() == attribute.StatusType.ToString()))
                            {
                                shouldBeVisible = true;
                            }
                            else if (attributes.Any() && attributes.All(attribute =>
                                currentStatusType.ToString() != attribute.StatusType.ToString()))
                            {
                                shouldBeVisible = false;
                            }

                            if (shouldBeVisible)
                            {
                                EditorGUILayout.PropertyField(obj, true);
                            }
                        }
                    }
                } while (obj.NextVisible(false));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}