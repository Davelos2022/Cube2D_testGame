using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CubeController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;

    private RectTransform _rectTransformCube;
    private CanvasGroup _canvasGroup;

    private Vector3 _currentPosition;
    private RectTransform _currentParent;

    private Vector3 _defaultsefScale = new Vector3(1, 1, 1);
    private float _sizeConversionSpeed = 1.3f;

    private void Awake()
    {
        _rectTransformCube = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    //Start dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        _currentPosition = transform.localPosition;

        _currentParent = transform.parent.GetComponent<RectTransform>();
        _currentParent.SetAsLastSibling();

        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    //At the moment of dragging
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransformCube.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    //End dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _currentParent)
            _rectTransformCube.anchoredPosition = _currentPosition;
        else
            StartCoroutine(ScaleAnim());
    }

    //Animation when hovering over a slot
    private IEnumerator ScaleAnim()
    {
        transform.localScale /= 2;

        while (transform.localScale != _defaultsefScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _defaultsefScale, _sizeConversionSpeed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    private void OnEnable()
    {
        transform.localScale = _defaultsefScale;
    }
}
