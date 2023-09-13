using Mole;
using Mole.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CarrotHolesContainer))]
    public class CarrotCollectData : UnityEditor.Editor
    {
        private const string CollectLabel = "Collect";
        private const string Clear = "Clear";

        // set holes in container
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CarrotHolesContainer container = (CarrotHolesContainer)target;

            if (GUILayout.Button(CollectLabel))
                foreach (var hole in FindObjectsOfType<CarrotHole>())
                    container.SetHole(hole);

            if (GUILayout.Button(Clear))
                container.Clear();

            EditorUtility.SetDirty(target);
        }
    }

}