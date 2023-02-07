using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverScreen : Menu
{
    public event UnityAction RestartButtonClick;

    public override void Close()
    {
        toggle.gameObject.SetActive(false);
        canvasGroup.alpha = 0;
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
        RestartButtonClick?.Invoke();
    }
}
