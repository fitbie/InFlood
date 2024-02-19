using System;
using System.Collections.Generic;
using UnityEngine;

namespace InFlood.Utils
{
/// <summary>
/// Serializable Dictionary. Need to be derived with type arguments.
/// Show up in inspector with List of SerializableKeyValuePair.
/// When using derived from this dictionary you shold initialize all dictionary keyvalue pairs manuallty in script,
/// because they can't be added via inspector. Also do not change key values in inspector.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[Serializable]
	public class SerializableKeyValuePair
	{
		[field: SerializeField] public TKey Key { get; set; }
		[field: SerializeField] public TValue Value { get; set; }
		public SerializableKeyValuePair(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}
	}

	[SerializeField] private List<SerializableKeyValuePair> keyValuePairs = new();
	
	// Save the dictionary to lists
	public void OnBeforeSerialize()
	{
		keyValuePairs.Clear();

		foreach(KeyValuePair<TKey, TValue> pair in this)
		{
			keyValuePairs.Add(new SerializableKeyValuePair(pair.Key, pair.Value));
		}
	}
	
	// Load dictionary from lists
	public void OnAfterDeserialize()
	{
		Clear();

		foreach (var pair in keyValuePairs)
		{
			Add(pair.Key, pair.Value);
		}
    }

}

}