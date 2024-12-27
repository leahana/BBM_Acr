using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

namespace BBM.MCH;

public class MchRotationEventHandler:IRotationEventHandler
{
    public Task OnPreCombat()
    {
        throw new NotImplementedException();
    }

    public void OnResetBattle()
    {
        throw new NotImplementedException();
    }

    public Task OnNoTarget()
    {
        throw new NotImplementedException();
    }

    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
        throw new NotImplementedException();
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        throw new NotImplementedException();
    }

    public void OnBattleUpdate(int currTimeInMs)
    {
        throw new NotImplementedException();
    }

    public void OnEnterRotation()
    {
        throw new NotImplementedException();
    }

    public void OnExitRotation()
    {
        throw new NotImplementedException();
    }

    public void OnTerritoryChanged()
    {
        throw new NotImplementedException();
    }
}