using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandTrackPointerTest : MonoBehaviour, IPointerEnterHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("UIElement OnPointerEnter STANDALONE SCRIPT");
	}
}
