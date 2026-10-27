using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUiPanel : MonoBehaviour, IUIPanel
{
    public string PanelName { get => _panelName; set => _panelName = value; }

    public bool isActive => gameObject.activeSelf;

    [SerializeField] private string _panelName;

    Animator animator;

    int hideHash = Animator.StringToHash("Hide");
    int showHash = Animator.StringToHash("Show");
    int IdleHash = Animator.StringToHash("Idle");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Start()
    {

    }

    public void Show()
    { }
    public void Hide() 
    { }

    public IEnumerator HideCoroutine()
    {
        if (animator == null) yield break;

        bool animation = animator.HasState(0, hideHash);

        if (animation)
        {
            animator?.Play(hideHash);
            yield return WaitForAnimation("Hide");
        }
    }

    public IEnumerator ShowCoroutine(bool instant)
    {
        if(animator  == null ) yield break;

        bool animation = animator.HasState(0, instant ? IdleHash : showHash);

        if (animation)
        {
            animator?.Play(instant ? IdleHash : showHash);
            yield return WaitForAnimation(instant  ? "IdleHash" : "ShowHash");
        }

        yield return null;
    }

    public IEnumerator WaitForAnimation(string stateName)
    {
        var clipInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(clipInfo.IsName(stateName) && clipInfo.normalizedTime < 1f)
        {
            yield return null;

            clipInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}
