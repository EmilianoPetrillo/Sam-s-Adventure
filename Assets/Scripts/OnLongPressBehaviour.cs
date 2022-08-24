using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnLongPressBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public static OnLongPressBehaviour Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    public float holdTime;

    //private bool held = false;
    //public UnityEvent onClick = new UnityEvent();

    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onReleaseLongPress = new UnityEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        //held = false;
        Invoke("OnLongPress", holdTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
        Invoke("OnReleaseLongPress", 0.01f);

        //if (!held)
        //    onClick.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
        Invoke("OnReleaseLongPress", 0.01f);
    }

    void OnLongPress()
    {
        //held = true;
        onLongPress.Invoke();
    }

    void OnReleaseLongPress()
    {
        //held = true;
        if(onReleaseLongPress!=null)
            onReleaseLongPress.Invoke();
    }
}