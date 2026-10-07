using UnityEngine;

public class FinishCoin : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            CoinManager.instance.AddCoin();
            CoinManager.instance.FinishRun();

            gameObject.SetActive(false);
        }
    }
}