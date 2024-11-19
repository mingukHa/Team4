using UnityEngine;
using UnityEngine.UI;

public class S1Main : MonoBehaviour
{
    [SerializeField]
    private GameObject joinui = null;
    [SerializeField]
    private GameObject exitui = null;

    private void Awake()
    {
        
    }
    private void Start()
    {
        joinui.gameObject.SetActive(false);
        exitui.gameObject.SetActive(false);
    }

}
