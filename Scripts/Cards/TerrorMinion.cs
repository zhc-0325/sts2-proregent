using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
namespace ProRegent.Scripts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

public class TerrorMinion : CardModel
{
     public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> { CardKeyword.Exhaust };
    public override bool HasStarCostX => true;
    protected override bool HasEnergyCostX => true;
    private const int energyCost = 0;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    private const TargetType targetType = TargetType.AnyEnemy;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://ProRegent/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {

            yield return new DynamicVar("Denominator", 20m);
        }
    }
    
    public TerrorMinion() : base(energyCost, type, CardRarity.Rare, targetType, shouldShowInCardLibrary)
    {
        
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        int StarsXValue = ResolveStarXValue();
        int EnergyXValue = ResolveEnergyXValue();
        int enemyCurrentHp=cardPlay.Target.CurrentHp;
        decimal divisionResult = (decimal)enemyCurrentHp / base.DynamicVars["Denominator"].IntValue;
        decimal ceilingResult = Math.Ceiling(divisionResult);
        int doomAmount = (int)ceilingResult * StarsXValue;
        await PowerCmd.Apply<DoomPower>(
            cardPlay.Target,
            doomAmount,
            base.Owner.Creature,
            this
        );
        await PlayerCmd.GainStars(EnergyXValue*StarsXValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Denominator"].UpgradeValueBy(-10m);
    }
}