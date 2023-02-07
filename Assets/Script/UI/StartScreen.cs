using UnityEngine.Events;

public class StartScreen : Menu
{
    public event UnityAction PlayButtonClick;


    public override void Close()
    {
        toggle.gameObject.SetActive(false);
        canvasGroup.alpha = 1;
        button.interactable = false;
    }

    public override void Open()
    {
        toggle.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        button.interactable = true;
    }

    protected override void OnButtonClick()
    {
        button.animator.Play("StartButton");
        PlayButtonClick?.Invoke();
    }
}
