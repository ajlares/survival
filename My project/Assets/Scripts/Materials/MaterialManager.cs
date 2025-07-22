using Mirror;
using UnityEngine;

public class MaterialManager : NetworkBehaviour
{
    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private float resourceLife;

    public void OnCreateMaterial(GameObject partent)
    {
        parentPrefab = partent;
    }

    public void TakeAHit(float damage)
    {
        resourceLife -= damage;
        if(resourceLife <= 0) OnDestroyMaterial();
    }
        
    public void OnDestroyMaterial()
    {
        parentPrefab.GetComponent<LandscapeManager>().lessResources();
        Destroy(this.gameObject);
    }
}
