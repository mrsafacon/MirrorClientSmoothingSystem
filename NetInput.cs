using UnityEngine;
using Mirror;
using System;

public class NetInput {
    
    public int Hor;
    public int Vert;

    public int NetworkRot;
    public float Rot;

    public byte DirectionByte() {
        int value = 0;
        if (Vert == 0) {
            if (Hor == 0) value = 0;
            else if (Hor > 0) value = 3;
            else value = 7;
        } else if (Vert > 0) {
            if (Hor == 0) value = 1;
            else if (Hor > 0) value = 2;
            else value = 8;
        } else {
            if (Hor == 0) value = 5;
            else if (Hor > 0) value = 4;
            else value = 6;
        }
        return Convert.ToByte(value);
    }

    public NetInput(byte dir, int netR) {
        NetworkRot = netR;
        Rot = NetworkRot / 100f;
        if(dir == 0) {
            Hor = 0;
            Vert = 0;
        } else if (dir == 1) {
            Hor = 0;
            Vert = 1;
        } else if (dir == 2) {
            Hor = 1;
            Vert = 1;
        } else if (dir == 3) {
            Hor = 1;
            Vert = 0;
        } else if (dir == 4) {
            Hor = 1;
            Vert = -1;
        } else if (dir == 5) {
            Hor = 0;
            Vert = -1;
        } else if (dir == 6) {
            Hor = -1;
            Vert = -1;
        } else if (dir == 7) {
            Hor = -1;
            Vert = 0;
        } else if (dir == 8) {
            Hor = -1;
            Vert = 1;
        }
    }

    public NetInput(int h, int v, float r) {        
        Hor = h;
        Vert = v;
        NetworkRot = Mathf.RoundToInt(r * 100);
        Rot = NetworkRot / 100f;
    }

    public NetInput(int h, int v, int netR) {
        Hor = h;
        Vert = v;
        NetworkRot = netR;
        Rot = NetworkRot / 100f;
    }

    public override string ToString() {
        return String.Format("Hor: {0} | Vert {1} | Rot {2}", Hor, Vert, Rot);
    }
}


public static class NetInputSerializer {

    public static void WriteItem(this NetworkWriter writer, NetInput netInput) {
        writer.WriteByte(netInput.DirectionByte());
        writer.WriteUInt16((ushort)netInput.NetworkRot);
    }

    public static NetInput ReadItem(this NetworkReader reader) {
        byte dirByte = reader.ReadByte();
        int netRot = reader.ReadUInt16();
        return new NetInput(dirByte, netRot);
    }


}