﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets.Factory;
using NebulaModel.Packets.Processors;
using NebulaWorld.Factory;

namespace NebulaClient.PacketProcessors.Factory
{
    // Processes pasting settings (e.g. item to make in an assembler) onto buildings events
    [RegisterPacketProcessor]
    class PasteBuildingSettingUpdateProcessor : IPacketProcessor<PasteBuildingSettingUpdate>
    {
        public void ProcessPacket(PasteBuildingSettingUpdate packet, NebulaConnection conn)
        {
            if (GameMain.galaxy.PlanetById(packet.PlanetId)?.factory != null)
            {
                BuildingParameters backup = BuildingParameters.clipboard;
                BuildingParameters.clipboard = packet.GetBuildingSettings();
                using (FactoryManager.EventFromServer.On())
                {
                    GameMain.galaxy.PlanetById(packet.PlanetId).factory.PasteBuildingSetting(packet.ObjectId);
                }
                BuildingParameters.clipboard = backup;
            }
        }
    }
}