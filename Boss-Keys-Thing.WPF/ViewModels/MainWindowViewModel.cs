using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BossKeysThing.WPF.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
	[Reactive] public GraphViewModel? Graph { get; set; }
}