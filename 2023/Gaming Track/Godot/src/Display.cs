using CommunityToolkit.Mvvm.Messaging;
using Godot;

namespace GodotPaddle.Game;

public partial class Display : CanvasLayer,
	IRecipient<PlayerScoredMessage>
{
    private int _player1Score;
    private int _player2Score;


    [Export]
	public Label Player1Score { get; set; }

	[Export]
	public Label Player2Score { get; set; }

    public override void _EnterTree()
    {
        base._EnterTree();

		StrongReferenceMessenger.Default.RegisterAll(this);
    }

    public override void _ExitTree()
    {
        base._ExitTree();

		StrongReferenceMessenger.Default.UnregisterAll(this);
    }

    public void Receive(PlayerScoredMessage message)
    {
        if (message.Player == 1)
		{
			Player1Score.Text = (++_player1Score).ToString();
		}
		else
		{
			Player2Score.Text = (++_player2Score).ToString();
		}
    }

}
