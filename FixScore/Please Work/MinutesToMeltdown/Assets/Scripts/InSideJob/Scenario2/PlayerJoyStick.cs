using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerJoyStick : Photon.MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Private Variables
    private Image backgroundJoyStickImage;
    private Image joyStickImage;
    #endregion

    #region Public Properties
    public Vector2 inputDirection { set; get; }
    #endregion

    #region Private Methods
    private void Start()
    {
        backgroundJoyStickImage = GetComponent<Image>();
        joyStickImage = transform.GetChild(0).GetComponent<Image>();
        inputDirection = Vector2.zero;
    }

    private void Update()
    {

    }
    #endregion

    #region Public Methods
    /// <summary>
    /// This method is called when the joystick is being dragged. It holds the joystick within its bounderies and.
    /// allows for movement towards the mouse position
    /// </summary>
    public virtual void OnDrag(PointerEventData pointerData)
    {
        Vector2 position = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (backgroundJoyStickImage.rectTransform, pointerData.position,
            pointerData.pressEventCamera, out position))
        {
            //Sets the position of the joystick
            position.x = (position.x / backgroundJoyStickImage.rectTransform.sizeDelta.x);
            position.y = (position.y / backgroundJoyStickImage.rectTransform.sizeDelta.y);


            ///<summary>
            /// If the joystick is positioned on the right then times the postion and add,
            /// if its on the left then times 2 and minus 1
            /// </summary>
            float x = (backgroundJoyStickImage.rectTransform.pivot.x == 1) ? position.x * 2 + 1 : position.x * 2 - 1;
            float y = (backgroundJoyStickImage.rectTransform.pivot.y == 1) ? position.y * 2 + 1 : position.y * 2 - 1;

            inputDirection = new Vector2(x, y);

            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            joyStickImage.rectTransform.anchoredPosition = new Vector2(inputDirection.x * (backgroundJoyStickImage.rectTransform.sizeDelta.x / 3), inputDirection.y * (backgroundJoyStickImage.rectTransform.sizeDelta.y / 3));
        }
    }

    /// <summary>
    /// When the joystick is clicked, basically held down - call the drag method so the joystick will move towrds the mouse position
    /// </summary>
    public virtual void OnPointerDown(PointerEventData pointerData)
    {
        OnDrag(pointerData);
    }

    /// <summary>
    /// When the joystick is clicked and came off then reset joystick position back to centre
    /// </summary>
    public virtual void OnPointerUp(PointerEventData pointerData)
    {
        inputDirection = Vector2.zero;
        joyStickImage.rectTransform.anchoredPosition = Vector2.zero;
    }
    #endregion
}
