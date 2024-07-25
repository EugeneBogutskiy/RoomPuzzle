using GameContent.UI.UIScripts.GameMenuView.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour, IGameMenuView
{
    private readonly ReactiveCommand<Unit> _saveCommand = new ReactiveCommand<Unit>();
    private readonly ReactiveCommand<Unit> _loadCommand = new ReactiveCommand<Unit>();
    private readonly ReactiveCommand<Unit> _exitCommand = new ReactiveCommand<Unit>();

    [SerializeField]
    private Button _menuButton;
    [SerializeField]
    private Button _closePanelButton;
    [SerializeField]
    private Button _save;
    [SerializeField]
    private Button _load;
    [SerializeField]
    private Button _exit;
    [SerializeField]
    private Button _blackBack;
    [SerializeField]
    private Transform _menuPanel;

    public IReactiveCommand<Unit> Save => _saveCommand;
    public IReactiveCommand<Unit> Load => _loadCommand;
    public IReactiveCommand<Unit> Exit => _exitCommand;

    private void OnEnable()
    {
        _blackBack.gameObject.SetActive(false);
        _menuPanel.gameObject.SetActive(false);

        _menuButton.OnClickAsObservable().Subscribe(_ => OnMenuButton()).AddTo(this);
        
        _closePanelButton.OnClickAsObservable().Subscribe(_ => OnClosePanelButton()).AddTo(this);

        _save.OnClickAsObservable().Subscribe(_ => OnSaveButton()).AddTo(this);

        _load.OnClickAsObservable().Subscribe(_ => OnLoadButton()).AddTo(this);

        _exit.OnClickAsObservable().Subscribe(_ => OnExit()).AddTo(this);

        _blackBack.OnClickAsObservable().Subscribe(_ => _closePanelButton.onClick.Invoke()).AddTo(this);
    }

    private void OnMenuButton()
    {
        _menuButton.gameObject.SetActive(false);
        _menuPanel.gameObject.SetActive(true);
        _blackBack.gameObject.SetActive(true);
    }

    private void OnClosePanelButton()
    {
        _menuPanel.gameObject.SetActive(false);
        _menuButton.gameObject.SetActive(true);
        _blackBack.gameObject.SetActive(false);
    }

    private void OnSaveButton()
    {
        _saveCommand.Execute(Unit.Default);
    }

    private void OnLoadButton()
    {
        _loadCommand.Execute(Unit.Default);
    }

    private void OnExit()
    {
        _exitCommand.Execute(Unit.Default);
    }
}