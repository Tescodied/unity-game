using UnityEngine;
using UnityEngine.UI;

public class CheckboxCraftingUI : MonoBehaviour
{
    public GameObject tick;
    public GameObject fillBg;
    Image fillCol;

    private readonly Color defaultColor = new Color32(0xAA, 0x9F, 0x92, 0xFF); // #AA9F92
    private readonly Color checkedColor = new Color32(0xB6, 0xFF, 0x86, 0xFF); // #B6FF86

    void Start()
    {
        fillCol = fillBg.GetComponent<Image>();   
    }

    public void CheckTickbox()
    {
        fillCol.color = checkedColor;
        tick.SetActive(true);
    }

    public void UncheckTickbox()
    {
        fillCol.color = defaultColor;
        tick.SetActive(false);
    }
}
