﻿using System.Collections.Generic;
using GTA.Math;
using ProtoBuf;

namespace GTANetworkShared
{
    public enum PacketType
    {
        VehiclePositionData = 0,
        ChatData = 1,
        PlayerDisconnect = 2,
        PedPositionData = 3,
        NpcVehPositionData = 4,
        NpcPedPositionData = 5,
        WorldSharingStop = 6,
        DiscoveryResponse = 7,
        ConnectionRequest = 8,
        NativeCall = 9,
        NativeResponse = 10,
        PlayerRespawned = 11,
        NativeTick = 12,
        NativeTickRecall = 13,
        NativeOnDisconnect = 14,
        NativeOnDisconnectRecall = 15,
        CreateEntity = 16,
        DeleteEntity = 17,
        ScriptEventTrigger = 18,
        SyncEvent = 19,
        FileTransferTick = 20,
        FileTransferRequest = 21,
        FileTransferComplete = 22,
        ConnectionConfirmed = 23,
        PlayerKilled = 24,
        StopResource = 25,
        UpdateMarkerProperties = 26,
        FileAcceptDeny = 27,
    }

    public enum ScriptVersion
    {
        Unknown = 0,
        VERSION_0_6 = 1,
        VERSION_0_6_1 = 2,
        VERSION_0_7 = 3,
        VERSION_0_8_1 = 4,
        VERSION_0_9 = 5,
    }

    public enum EntityType
    {
        Vehicle = 1,
        Prop = 2,
        Blip = 3,
        Marker = 4,
        Pickup = 5,
    }

    public enum FileType
    {
        Normal = 0,
        Map = 1,
        Script = 2,
    }

    public enum SyncEventType
    {
        LandingGearChange = 0,
        DoorStateChange = 1,
        BooleanLights = 2,
        TrailerDeTach = 3,
        TireBurst = 4,
        RadioChange = 5,
        PickupPickedUp = 6,
    }

    public enum Lights
    {
        NormalLights = 0,
        Highbeams = 1,
    }

    public struct LocalHandle
    {
        public LocalHandle(int handle)
        {
            Value = handle;
        }

        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            return (obj as NetHandle?)?.Value == Value;
        }

        public static bool operator ==(LocalHandle left, LocalHandle right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(LocalHandle left, LocalHandle right)
        {
            return left.Value != right.Value;
        }

        public bool IsNull { get { return Value == 0; } }
    }

    public struct NetHandle
    {
        public NetHandle(int handle)
        {
            Value = handle;
        }

        public override bool Equals(object obj)
        {
            return (obj as NetHandle?)?.Value == Value;
        }

        public static bool operator == (NetHandle left, NetHandle right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(NetHandle left, NetHandle right)
        {
            return left.Value != right.Value;
        }

        public bool IsNull { get { return Value == 0; } }

        public int Value { get; set; }
    }

    [ProtoContract]
    [ProtoInclude(5, typeof(VehicleProperties))]
    [ProtoInclude(6, typeof(BlipProperties))]
    [ProtoInclude(7, typeof(MarkerProperties))]
    [ProtoInclude(8, typeof(PickupProperties))]
    public class EntityProperties
    {
        [ProtoMember(1)]
        public Vector3 Position { get; set; }

        [ProtoMember(2)]
        public Vector3 Rotation { get; set; }

        [ProtoMember(3)]
        public int ModelHash { get; set; }

        [ProtoMember(4)]
        public byte EntityType { get; set; }
    }

    [ProtoContract]
    public class VehicleProperties : EntityProperties
    {
        public VehicleProperties()
        {
            Mods = new int[50];
            Health = 1000;
            Doors = new bool[7];
            Tires = new bool[8];
        }

        [ProtoMember(1)]
        public int PrimaryColor { get; set; }

        [ProtoMember(2)]
        public int SecondaryColor { get; set; }

        [ProtoMember(3)]
        public float Health { get; set; }

        [ProtoMember(4)]
        public bool IsDead { get; set; }

        [ProtoMember(5)]
        public int[] Mods { get; set; }

        [ProtoMember(6)]
        public bool Siren { get; set; }

        [ProtoMember(7)]
        public bool[] Doors { get; set; }

        [ProtoMember(8)]
        public int Trailer { get; set; }

        [ProtoMember(9)]
        public bool[] Tires { get; set; }
    }

    [ProtoContract]
    public class BlipProperties : EntityProperties
    {
        public BlipProperties()
        {
            Sprite = 0;
            Scale = 1f;
            Alpha = 255;
        }

        [ProtoMember(1)]
        public int Sprite { get; set; }

        [ProtoMember(2)]
        public float Scale { get; set; }

        [ProtoMember(3)]
        public int Color { get; set; }

        [ProtoMember(4)]
        public bool IsShortRange { get; set; }

        [ProtoMember(5)]
        public int Alpha { get; set; }
    }

    [ProtoContract]
    public class MarkerProperties : EntityProperties
    {
        [ProtoMember(1)]
        public Vector3 Direction { get; set; }

        [ProtoMember(2)]
        public int MarkerType { get; set; }

        [ProtoMember(3)]
        public int Red { get; set; }

        [ProtoMember(4)]
        public int Green { get; set; }

        [ProtoMember(5)]
        public int Blue { get; set; }

        [ProtoMember(6)]
        public int Alpha { get; set; }

        [ProtoMember(7)]
        public Vector3 Scale { get; set; }
    }

    [ProtoContract]
    public class PickupProperties : EntityProperties
    {
        [ProtoMember(1)]
        public int Amount { get; set; }

        [ProtoMember(2)]
        public bool PickedUp { get; set; }
    }

    [ProtoContract]
    public class ConnectionResponse
    {
        [ProtoMember(1)]
        public byte AssignedChannel { get; set; }

        [ProtoMember(2)]
        public int CharacterHandle { get; set; }
    }

    [ProtoContract]
    public class ServerMap
    {
        [ProtoMember(1)]
        public Dictionary<int, EntityProperties> Objects { get; set; }

        [ProtoMember(2)]
        public Dictionary<int, VehicleProperties> Vehicles { get; set; }

        [ProtoMember(3)]
        public Dictionary<int, BlipProperties> Blips { get; set; }

        [ProtoMember(4)]
        public Dictionary<int, MarkerProperties> Markers { get; set; }

        [ProtoMember(5)]
        public Dictionary<int, PickupProperties> Pickups { get; set; }
    }

    [ProtoContract]
    public class ScriptCollection
    {
        [ProtoMember(1)]
        public List<ClientsideScript> ClientsideScripts { get; set; }
    }

    [ProtoContract]
    public class ClientsideScript
    {
        [ProtoMember(1)]
        public string ResourceParent { get; set; }

        [ProtoMember(2)]
        public string Script { get; set; }
    }


    [ProtoContract]
    public class DataDownloadStart
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public byte FileType { get; set; }

        [ProtoMember(3)]
        public string FileName { get; set; }

        [ProtoMember(4)]
        public string ResourceParent { get; set; }

        [ProtoMember(5)]
        public int Length { get; set; }

        [ProtoMember(6)]
        public string Md5Hash { get; set; }
    }

    [ProtoContract]
    public class DeleteEntity
    {
        [ProtoMember(1)]
        public int NetHandle { get; set; }
    }
    
    [ProtoContract]
    public class CreateEntity
    {
        [ProtoMember(1)]
        public int NetHandle { get; set; }

        [ProtoMember(2)]
        public byte EntityType { get; set; }

        [ProtoMember(3)]
        public EntityProperties Properties { get; set; }
    }

    [ProtoContract]
    public class SyncEvent
    {
        [ProtoMember(1)]
        public byte EventType { get; set; }

        [ProtoMember(2)]
        public List<NativeArgument> Arguments { get; set; }
    }

    [ProtoContract]
    public class DiscoveryResponse
    {
        [ProtoMember(1)]
        public string ServerName { get; set; }
        [ProtoMember(2)]
        public short MaxPlayers { get; set; }
        [ProtoMember(3)]
        public short PlayerCount { get; set; }
        [ProtoMember(4)]
        public bool PasswordProtected { get; set; }
        [ProtoMember(5)]
        public int Port { get; set; }
        [ProtoMember(6)]
        public string Gamemode { get; set; }
        [ProtoMember(7)]
        public bool LAN { get; set; }
    }

    [ProtoContract]
    public class ConnectionRequest
    {
        [ProtoMember(1)]
        public string SocialClubName { get; set; }

        [ProtoMember(2)]
        public string Password { get; set; }

        [ProtoMember(3)]
        public string DisplayName { get; set; }

        [ProtoMember(4)]
        public byte GameVersion { get; set; }

        [ProtoMember(5)]
        public byte ScriptVersion { get; set; }
    }

    [ProtoContract]
    public class VehicleData
    {
        [ProtoMember(1)]
        public long Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public int VehicleModelHash { get; set; }
        [ProtoMember(4)]
        public int PedModelHash { get; set; }
        [ProtoMember(5)]
        public int WeaponHash { get; set; }
        [ProtoMember(6)]
        public bool IsShooting { get; set; }

        [ProtoMember(7)]
        public Vector3 Position { get; set; }
        [ProtoMember(8)]
        public Vector3 Quaternion { get; set; }

        [ProtoMember(9)]
        public int VehicleSeat { get; set; }

        [ProtoMember(10)]
        public float VehicleHealth { get; set; }

        [ProtoMember(11)]
        public int PlayerHealth { get; set; }

        [ProtoMember(12)]
        public float Latency { get; set; }

        [ProtoMember(13)]
        public bool IsPressingHorn { get; set; }

        [ProtoMember(14)]
        public bool IsSirenActive { get; set; }

        [ProtoMember(15)]
        public float Speed { get; set; }

        [ProtoMember(16)]
        public int VehicleHandle { get; set; }

        [ProtoMember(17)]
        public int NetHandle { get; set; }

        [ProtoMember(18)]
        public Vector3 Velocity { get; set; }

        [ProtoMember(19)]
        public int PedArmor { get; set; }

        [ProtoMember(20)]
        public bool IsVehicleDead { get; set; }

        [ProtoMember(21)]
        public Vector3 AimCoords { get; set; }
    }

    [ProtoContract]
    public class PedData
    {
        [ProtoMember(1)]
        public long Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public int PedModelHash { get; set; }

        [ProtoMember(4)]
        public Vector3 Position { get; set; }

        [ProtoMember(5)]
        public Vector3 Quaternion { get; set; }

        [ProtoMember(6)]
        public Vector3 AimCoords { get; set; }

        [ProtoMember(7)]
        public int WeaponHash { get; set; }

        [ProtoMember(8)]
        public int PlayerHealth { get; set; }

        [ProtoMember(9)]
        public float Latency { get; set; }
        
        [ProtoMember(10)]
        public int NetHandle { get; set; }

        [ProtoMember(11)]
        public float Speed { get; set; }

        [ProtoMember(12)]
        public int PedArmor { get; set; }

        [ProtoMember(13)]
        public bool IsJumping { get; set; }
        [ProtoMember(14)]
        public bool IsShooting { get; set; }

        [ProtoMember(15)]
        public bool IsAiming { get; set; }

        [ProtoMember(16)]
        public bool IsParachuteOpen { get; set; }

        [ProtoMember(17)]
        public bool IsRagdoll { get; set; }

        [ProtoMember(18)]
        public bool IsInMeleeCombat { get; set; }

        [ProtoMember(19)]
        public bool IsFreefallingWithChute { get; set; }
    }

    [ProtoContract]
    public class PlayerDisconnect
    {
        [ProtoMember(1)]
        public long Id { get; set; }
    }
    
    [ProtoContract]
    public class Vector3
    {
        [ProtoMember(1)]
        public float X { get; set; }
        [ProtoMember(2)]
        public float Y { get; set; }
        [ProtoMember(3)]
        public float Z { get; set; }

        public GTA.Math.Vector3 ToVector()
        {
            return new GTA.Math.Vector3(X, Y, Z);
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(double x, double y, double z)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
        }

        public Vector3()
        {
            
        }
    }
    
    [ProtoContract]
    public class Quaternion
    {
        [ProtoMember(1)]
        public float X { get; set; }
        [ProtoMember(2)]
        public float Y { get; set; }
        [ProtoMember(3)]
        public float Z { get; set; }
        [ProtoMember(4)]
        public float W { get; set; }

        public GTA.Math.Quaternion ToQuaternion()
        {
            return new GTA.Math.Quaternion(X, Y, Z, W);
        }
    }

    public static class VectorExtensions
    {
        public static Vector3 ToLVector(this GTA.Math.Vector3 vec)
        {
            return new Vector3()
            {
                X = vec.X,
                Y = vec.Y,
                Z = vec.Z,
            };
        }

        public static Quaternion ToLQuaternion(this GTA.Math.Quaternion vec)
        {
            return new Quaternion()
            {
                X = vec.X,
                Y = vec.Y,
                Z = vec.Z,
                W = vec.W,
            };
        }
    }
}