using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeCheckClicked : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool clicked = false;
    public bool hovered = false;
    public GameObject clickedOutline;
    RecipeUnlockCheck recipeScript;

    public string layout;
    public GameObject[] itemsNecessary;
    public GameObject[] itemsResult;

    void Start()
    {
        recipeScript = GameObject.FindGameObjectWithTag("Recipes UI").GetComponent<RecipeUnlockCheck>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        recipeScript.UpdateChildren(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    void Update()
    {
        clickedOutline.SetActive(clicked || hovered);
    }
}
