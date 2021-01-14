using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.InputSystem.OnScreen
{
    /// <summary>
    /// A stick control displayed on screen and moved around by touch or other pointer
    /// input.
    /// </summary>
    public class Joystick_LouieWilliamson : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public void OnPointerDown(PointerEventData q)
        {
            if (q == null)
                throw new System.ArgumentNullException(nameof(q));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), q.position, q.pressEventCamera, out l);
        }

        public void OnDrag(PointerEventData w)
        {
            if (w == null)
                throw new System.ArgumentNullException(nameof(w));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), w.position, w.pressEventCamera, out var p);
            var d = p - l;

            d = Vector2.ClampMagnitude(d, movementRange);
            ((RectTransform)transform).anchoredPosition = k + (Vector3)d;

            var h = new Vector2(d.x / movementRange, d.y / movementRange);
            SendValueToControl(h);

            m = h;
        }

        public void OnPointerUp(PointerEventData d)
        {
            ((RectTransform)transform).anchoredPosition = k;
            SendValueToControl(Vector2.zero);

            m = Vector2.zero;
        }

         void Start()
        {
            k = ((RectTransform)transform).anchoredPosition;
            n = GameObject.Find("Player").GetComponent<PlayerMovement_MarioFernandes>();
            m = Vector2.zero;
        }

        public float movementRange
        {
            get => h;
            set => h = value;
        }

        [SerializeField]
         float h = 50;

         string j;

         Vector3 k;
         Vector2 l;

         Vector2 m;
         PlayerMovement_MarioFernandes n;

        protected override string controlPathInternal
        {
            get => j;
            set => j = value;
        }
         void FixedUpdate()
        {
            n.RecieveVector(m);
        }
    }
}
