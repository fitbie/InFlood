using UnityEngine;



[CreateAssetMenu(fileName = "RarityColors", menuName = "Inventory/Rarity Colors")]
public class RarityColors : ScriptableObject
{
    [SerializeField]
    private Color commonColor;

    [SerializeField]
    private Color rareColor;

    [SerializeField]
    private Color epicColor;



    public Color GetColorRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return commonColor;
            case Rarity.Rare:
                return rareColor;
            case Rarity.Epic:
                return epicColor;
            default:
                
                throw new System.Exception("Incorrect Rarity type when trying get RarityColoe!");
        }
    }
}