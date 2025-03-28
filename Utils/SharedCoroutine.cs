using System.Collections;
using UnityEngine;

namespace FreeThinkers.Utils;

public class SharedCoroutineStarter : MonoBehaviour {

    private static SharedCoroutineStarter? _instance;

    public static new Coroutine StartCoroutine(IEnumerator routine) {

        if (_instance == null) {
            _instance = new GameObject("Shared Coroutine Starter").AddComponent<SharedCoroutineStarter>();
            DontDestroyOnLoad(_instance);
        }
        return ((MonoBehaviour)_instance).StartCoroutine(routine);

    }

}