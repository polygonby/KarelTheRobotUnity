using UnityEngine;
using UnityEngine.Assertions;

public class SingletonScriptableObject<T> : ScriptableObject where T: SingletonScriptableObject<T>
{
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                var instances = Resources.FindObjectsOfTypeAll<T>();
                
                Assert.IsFalse(instances.Length == 0, 
                    $"No SingletonScriptableObject instances of type {typeof(T)} is presented in Resources");
                
                s_instance = instances[0];
            }

            return s_instance;
        }
    }

    private static T s_instance = null;
}
