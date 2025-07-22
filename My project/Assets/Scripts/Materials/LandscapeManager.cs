using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LandscapeManager : MonoBehaviour
{
	[Header("Landscale Materials amount")]
	[SerializeField] private int maxResources;
	[SerializeField] private int minResources;
	[SerializeField] private int actualResources;
	[Header("materials")]
	[SerializeField] private List<GameObject> materialsprefabs;
	[SerializeField] private float materialSpawnDelay;
	[SerializeField] private bool isCreating;
	
	void Start()
	{
		isCreating = false;	
	}

	private void Update()
	{
		if (actualResources < minResources && !isCreating)
		{
			isCreating = true;
			StartCoroutine("cretaeResource");
		}
	}

	IEnumerator cretaeResource()
	{
		if (materialsprefabs.Count > 0)
		{
			MeshFilter meshFilter = GetComponent<MeshFilter>();
			if (meshFilter != null && meshFilter.mesh != null)
			{
				Mesh mesh = meshFilter.mesh;
				Vector3[] vertices = mesh.vertices;
				if (vertices.Length > 0)
				{
					Vector3 localPoint = vertices[Random.Range(0, vertices.Length)];
					Vector3 worldPoint = transform.TransformPoint(localPoint);
					int randomMaterial;
					if (materialsprefabs.Count == 1) randomMaterial = 0;
					else randomMaterial = Random.Range(0, materialsprefabs.Count);
					GameObject _resourceTemp = materialsprefabs[randomMaterial];
					_resourceTemp.GetComponent<MaterialManager>().OnCreateMaterial(this.gameObject);
					Instantiate(_resourceTemp, worldPoint, transform.rotation);
					actualResources++;
				}
			}
		}
		yield return new WaitForSeconds(materialSpawnDelay);
		if (actualResources < minResources)
		{
			StartCoroutine("cretaeResource");
		}
		else
		{
			isCreating = false;
		}
	}
	public void lessResources()
	{
		actualResources--;
	}
}
