using System;
using UnityEngine;

public class InteractZoneTrigger : MonoBehaviour
{
   [SerializeField] private GameObject parentObject;
   private void OnTriggerEnter(Collider other)
   {
      
      if (other.tag == "HarvestMaterial")
      {
         other.gameObject.GetComponent<MaterialManager>().TakeAHit(parentObject.GetComponent<PlayerBehabeour>().getPlayerDamage());
      }
   }
}
