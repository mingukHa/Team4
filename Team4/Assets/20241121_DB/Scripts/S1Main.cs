using UnityEngine;
using UnityEngine.UI;

public class S1Main : MonoBehaviour
{
    [SerializeField]
    private Button Jclosebt = null;
    [SerializeField]
    private Button Eclosebt = null;
    [SerializeField]
    private Button joinbt = null;
    [SerializeField]
    private Button exitbt = null;
    [SerializeField]
    private GameObject joinUI = null;
    [SerializeField]
    private GameObject exitUI = null;
    
    

    private void Start()
    {
        joinbt.onClick.AddListener(() => OnjoinUI(true));
        exitbt.onClick.AddListener(() => OnExitUI(true));
        Jclosebt.onClick.AddListener(() => Jclose(false));
        Eclosebt.onClick.AddListener(() => Eclose(false));
    }

    private void Jclose(bool close)
    {
        joinUI.SetActive(close);
    }
    private void Eclose(bool close)
    {
        exitUI.SetActive(close);
    }
    private void EnjoinUI(bool close)
    {
        exitUI.SetActive(close);
    }

    private void OnjoinUI(bool join)
    {
        joinUI.SetActive(join);
    }

    private void OnExitUI(bool exit)
    {
        exitUI.SetActive(exit);
    }
}

