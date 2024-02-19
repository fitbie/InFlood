using System.Collections.Generic;
using UnityEngine;


// .................DELETED BECAUSE STUPID. USE LIST<DamageResist> INSTEAD.................
/// <summary>
/// Serializable dictionary with resists for every damage type.
/// WARNING! There is a problem: you can't add new element to dictionary via inspector, because it will
/// automatically copy previous, and dictionary is not able to contain same keys. To fix this - initialize
/// dictionary keyvalues through script.
/// </summary>
//
// [System.Serializable]
// public class DamageResistsDictionary : SerializableDictionary<DamageType, float> {}



namespace InFlood.Entities.ActionSystem
{

/// <summary>
/// Wrapper class, containing resist value from 0 to 1 for every specified DamageType.
/// </summary>
[System.Serializable]
public class DamageResist
{
    [field:SerializeField] public DamageType DamageType { get; private set; }

    [field:SerializeField]
    [Range(-1, 1)]
    public float ResistValue;

    public DamageResist(DamageType damageType, float resistValue)
    {
        DamageType = damageType;
        ResistValue = resistValue;
    }
}


public interface IHaveResists
{
    public List<DamageResist> DamageResists { get; set; }

    public void ChangeResist(DamageType damageType, float value)
    {
        var required = DamageResists.Find((d) => d.DamageType == damageType);

        if (required != null) { required.ResistValue = Mathf.Clamp(required.ResistValue + value, -1, 1); }
        else
        {
            DamageResists.Add(new DamageResist(damageType, Mathf.Clamp(value, -1, 1)));
        }
    }


    public float GetResist(DamageType damageType)
    {
        var required = DamageResists.Find((d) => d.DamageType == damageType);

        if (required != null) { return required.ResistValue; }
        
        return 0;
    }

}

}