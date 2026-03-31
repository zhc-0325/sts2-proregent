using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Rooms;

namespace ProRegent.Scripts.Relics;

public class StarBlade : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public override string PackedIconPath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string PackedIconOutlinePath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string BigIconPath => $"res://ProRegent/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    private int CurrentActIndex 
{
    get
    {
        if (base.Owner == null || base.Owner.RunState == null)
        {
            return 1;
        }
        return base.Owner.RunState.CurrentActIndex + 1;
    }
}

     protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {

            yield return new DynamicVar("CurrentActIndex", 1m);
        }
    }
    public override Task AfterRoomEntered(AbstractRoom enteredRoom)
{
    if (base.Owner != null)
    {
        base.DynamicVars["CurrentActIndex"].BaseValue = (decimal)CurrentActIndex;
        InvokeDisplayAmountChanged();
        Flash();
    }
    return Task.CompletedTask;
}

    public override async Task AfterStarsSpent(int amount, Player spender)
{
    if (spender != base.Owner || amount <= 0)
    {
        return;
    }
    Flash(); 

    await ForgeCmd.Forge(amount * CurrentActIndex, base.Owner, this);

    if (amount >= 3)
    {
        await UpgradeAllSovereignBlade(spender);
    }
}
    private async Task UpgradeAllSovereignBlade(Player player)
    {
        if (player?.PlayerCombatState?.AllCards == null)
        {
            return;
        }
        foreach (CardModel card in player.PlayerCombatState.AllCards)
        {
            if (card is SovereignBlade && card.IsUpgradable)
            {
                CardCmd.Upgrade(card);
                Flash();
            }
        }
    }
}