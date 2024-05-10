using BSL.v41.Logic.Dynamic;
using BSL.v41.Titan.Utilities;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland;

public class OwnMatchmakingManager
{
    public Dictionary<int, MatchmakingManager> MatchmakingManagers { get; } = new();

    public void Initialize()
    {
        foreach (var variable in DynamicServerParameters.Event1DataMassive)
        {
            var mm = new MatchmakingManager();
            {
                mm.Initialize(variable.Value.GetSlot(),
                    LogicGamePlayUtil.GetPlayerCountWithGameModeVariation(
                        LogicDataTables
                            .GetGameModeVariationByName(
                                ((LogicLocationData)LogicDataTables.GetDataById(variable.Value.GetLocation()))
                                .GetGameModeVariation()).GetVariation()));
            }

            MatchmakingManagers.Add(variable.Value.GetSlot(), mm);
        }
    }
}