using UnityEngine;
public class AttackZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutMaterial")
        {
            other.gameObject.GetComponent<MaterialManager>().TakeAHit(parentObject.GetComponent<PlayerBehabeour>().getPlayerDamage());
        }

        if (other.tag == "Player" && other.gameObject != parentObject)
        {
            other.gameObject.GetComponent<PlayerBehabeour>(). TakeDamage();
        }
    }
}
