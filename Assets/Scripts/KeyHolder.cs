using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Key.KeyType> keyList;

    private void Awake()
    {
    	keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType keyType)
    {
    	Debug.Log("Added Key: " + keyType);
    	keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
    	keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
    	return keyList.Contains(keyType);
    }

    private void OnCollisionEnter(Collision collision)
    {
    	Key key = collision.gameObject.GetComponent<Key>();
    	if(key != null)
    	{
    		AddKey(key.GetKeyType());
    		Destroy(key.gameObject);
    	}
    }

}
