using UnityEngine;
using UnityEngine.EventSystems;


public class DropDown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public RectTransform container;
	public bool isOpen;

    // Use this for initialization
    void Awake () {
		//Find all the sub menus and hide them 
		container = transform.FindChild ("Container").GetComponent <RectTransform> ();
		isOpen = false;
	}
	
	void Update () {
			//Animate the container moving 
			Vector3 scale = container.localScale;
			scale.y = Mathf.Lerp (scale.y, isOpen? 1 : 0, Time.deltaTime * 12); //will not move if isOpen = false as scale will be zero 
			container.localScale = scale;

	}

	//On hover show submenu
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;  }


	//On exit event hide submenu
    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;  }
}
