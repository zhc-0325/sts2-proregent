using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;


namespace ProRegent.Scripts.Relics;

public class EnLoyal : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    public override string PackedIconPath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string PackedIconOutlinePath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string BigIconPath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";

    public override async Task AfterCardPlayed(PlayerChoiceContext context,CardPlay cardPlay)
	{
		if (cardPlay.Card is SovereignBlade)
		{
			Flash();
			await CardPileCmd.Add(cardPlay.Card, PileType.Draw);
		}
	}
}
