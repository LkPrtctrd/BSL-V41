using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Objects.Laser;

public class LogicItemServer : LogicGameObjectServer
{
    private readonly LogicBattleModeServer _logicBattleModeServer;
    private readonly LogicItemData _logicItemData;
    private int _gameModeVariation;
    private bool _isPicked;
    private LogicCharacterServer _pickerUp = null!;
    private int _pickTicks;
    private int _ticks;

    public LogicItemServer(LogicBattleModeServer logicBattleModeServer, int classId, int instanceId, int index) :
        base(logicBattleModeServer, classId, instanceId, 0, index)
    {
        _logicBattleModeServer = logicBattleModeServer;
        _pickTicks = 0;
        _ticks = 0;
        _isPicked = false;
        _gameModeVariation = LogicDataTables.GetGameModeVariationByName(
            ((LogicLocationData)LogicDataTables.GetDataById(_logicBattleModeServer.GetLocation() < 1000000
                ? GlobalId.CreateGlobalId(CsvHelperTable.Locations.GetId(), _logicBattleModeServer.GetLocation())
                : _logicBattleModeServer.GetLocation())).GetGameModeVariation()).GetVariation();
        _logicItemData = (LogicItemData)LogicDataTables.GetDataById(GlobalId.CreateGlobalId(classId, instanceId));
    }

    public override void Tick()
    {
        _ticks++;

        if (_logicItemData.GetValue() != 0)
            switch (_ticks - 40)
            {
                case <= 10 and >= 1:
                {
                    SetPosition(GetX() - 1, GetY() + -1, GetZ() + (_ticks - 40) * 2);
                    break;
                }

                case <= 20 and > 12:
                {
                    SetPosition(GetX() + 1, GetY() - -1, GetZ() - (_ticks - 40) * 2);
                    SetPosition(GetX(), GetY(), GetZ() <= 16 ? 0 : GetZ());
                    break;
                }
            }

        if (_ticks - 40 > 40) _ticks = 0;

        if (!_isPicked) return;
        _pickTicks++;

        SetPosition(GetX(), GetY(), GetZ() + 120 / 3);

        if (_pickTicks != 3) return;
        //_pickerUp.PickedUpItemThisTick(this);
        GetLogicGameObjectManager().RemoveGameObjectReferences(this);
    }

    public LogicItemData GetItemData()
    {
        return _logicItemData;
    }

    public void SetPickedUpBy(LogicCharacterServer logicCharacterServer)
    {
        _pickerUp = logicCharacterServer;
        _isPicked = true;
    }

    public override void Encode(BitStream bitStream, bool isOwnObject, int visionIndex, int visionTeam)
    {
        base.Encode(bitStream, isOwnObject, visionIndex, visionTeam);

        if (_logicItemData.GetInstanceId() == 5 || _logicItemData.GetInstanceId() == 24)
        {
            bitStream.WritePositiveIntMax16383(170);
            bitStream.WritePositiveIntMax16383(30);

            if (_logicItemData.GetInstanceId() == 24) bitStream.WritePositiveIntMax16383(0);
        }
        else
        {
            var v7 = _logicItemData.GetInstanceId();

            if (v7 <= 0x19 && ((1 << v7) & 0x2005040) != 0)
            {
                bitStream.WritePositiveIntMax63(0);
                bitStream.WritePositiveIntMax63(0);
            }
            else
            {
                if ((v7 & 0xFFFFFFF8) != 16) return;
                bitStream.WritePositiveIntMax3(0);
                bitStream.WritePositiveIntMax63(0);
            }
        }
    }

    public override int GetType()
    {
        return ObjectTypeHelperTable.Item.GetObjectType();
    }
}