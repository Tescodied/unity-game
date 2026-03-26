using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingOverlayClick : MonoBehaviour, IPointerClickHandler
{
    CraftingTableLogic uiToggle;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        uiToggle = player.GetComponent<CraftingTableLogic>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiToggle.clickOff = true;
    }
}