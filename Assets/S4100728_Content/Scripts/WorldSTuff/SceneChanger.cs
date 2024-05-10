using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private int sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
