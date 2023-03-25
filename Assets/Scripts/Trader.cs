using System.Collections;
using Cinemachine;
using UnityEngine;

public class Trader : MonoBehaviour
{
    [SerializeField] private float highlightDuration = 3;
    private CinemachineVirtualCamera _camera;
    public GameObject E;

    public TraderData data;
    public bool itemCollected;

    private void Start()
    {
        _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        data.OnSpeakingChanged += () =>
        {
            StartCoroutine(Highlight());
        };
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (data.IsSpeakingGibberish || itemCollected) return;
        
        E.SetActive(true);
        col.GetComponent<Inventory>().currentTrader = this;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || itemCollected) return;

        E.SetActive(false);
        col.GetComponent<Inventory>().currentTrader = null;
    }

    private IEnumerator Highlight()
    {
        _camera.Priority = 100;
        yield return new WaitForSeconds(highlightDuration);
        _camera.Priority = 1;
    }
}
