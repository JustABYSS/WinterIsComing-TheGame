using UnityEngine;

public class BackMusic : MonoBehaviour
{
    void Awake()
    {
        int musicObjects = FindObjectsOfType<BackMusic>().Length;
        if (musicObjects > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
