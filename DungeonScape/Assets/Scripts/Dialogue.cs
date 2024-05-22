using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    [SerializeField] private GameObject marquita;

    // Update is called once per frame
    void Update()
    {
        // Aquí puedes agregar lógica que dependa de isPlayerInRange si es necesario
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            marquita.SetActive(true);
            Debug.Log("Pepe");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            marquita.SetActive(false);
            isPlayerInRange = false;
            Debug.Log("NoPepe");
        }
    }
}
