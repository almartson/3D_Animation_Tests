using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorPanel : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Button _button;

    void Start()
    {
        foreach (var parameter in _animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
                AddButton(parameter);
        }
    }

    void AddButton(AnimatorControllerParameter parameter)
    {
        var button = Instantiate(_button, transform);
        foreach(var tmpText in button.GetComponentsInChildren<TMP_Text>())
            tmpText.text = parameter.name;
        
        button.onClick.AddListener(() => _animator.SetTrigger(parameter.nameHash));
    }
}