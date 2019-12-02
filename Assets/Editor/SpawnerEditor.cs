using UnityEditor;
using UnityEngine;

namespace Utils {
    /// <summary>
    /// Editor Class specifically for the spawner MonoBehaviour
    /// Ensure that the spawner inspector UI limits the probabilities
    /// to sum up to 100.
    /// </summary>
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor {

        private Spawner _script;


        void OnEnable() { _script = (Spawner) target; }


        public override void OnInspectorGUI() {
            serializedObject.Update();
            base.OnInspectorGUI();

            float runningSum = 0f;
            for ( int i = 0; i < _script.spawnables.Count; i++ ) {
                Spawnable testing = _script.spawnables[i];
                runningSum += testing.probability;
            }

            // Make sure it adds up to 100
            if ( runningSum < 100 ) {
                // Increasing the first one that can be increase
                float diff = 100 - runningSum;
                for ( int i = 0; i < _script.spawnables.Count; i++ ) {
                    if ( _script.spawnables[i].probability + diff < 100 ) {
                        Spawnable current = _script.spawnables[i];
                        current.probability += diff;
                        current.probability = Mathf.Clamp(current.probability,0f, 100f);
                        _script.spawnables.RemoveAt(i);
                        _script.spawnables.Insert(i, current);
                        break;
                    }
                }
            }

            if ( runningSum > 100 ) {
                float diff = runningSum - 100;
                for ( int i = 0; i < _script.spawnables.Count; i++ ) {
                    if ( _script.spawnables[i].probability - diff > 0 ) {
                        Spawnable current = _script.spawnables[i];
                        current.probability -= diff;
                        current.probability = Mathf.Clamp(current.probability, 0f, 100f);
                        _script.spawnables.RemoveAt(i);
                        _script.spawnables.Insert(i, current);
                        break;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}