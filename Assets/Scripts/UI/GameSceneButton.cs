using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneButton : BaseButton
{
    public TMP_InputField idInput;
    public TMP_InputField majorInput;
    public TMP_InputField nameInput;

    public override void OnClicked()
    {
        base.OnClicked();

        if (string.IsNullOrEmpty(idInput.text) || string.IsNullOrEmpty(majorInput.text) || string.IsNullOrEmpty(nameInput.text))
            return;

        //TODO : �÷��� Ƚ�� 3������ Ȯ��, 3���̻��̸� �÷��� �Ұ�

        Managers.Data.SetPlayerInfo(idInput.text, majorInput.text, nameInput.text);
        Managers.Scene.LoadScene(Scene.Game);
    }
}
