using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientExtrapolation : MonoBehaviour
{
    
    LinkedList<StoredInput> inputStream = new LinkedList<StoredInput>(); //list of inputs with timestamp

    public NetInput estimatedInput = new NetInput(0,0,0); //value to be used by the client's character controller. ideally this will be the same as what the server processed 1 pong ago.

    public int COUNT = 0;

    void Update() {
        COUNT = inputStream.Count;

        if (NetworkClient.isConnected) {          
            double checkTime = 0;
            double estimatedServerInputTime = NetworkTime.time - (NetworkTime.rtt - NetworkTime.PingWindowSize * .001f);//approximate the input that server used 1 pong ago

            while (checkTime < estimatedServerInputTime) {
                if(inputStream.Count < 1) {
                    checkTime = estimatedServerInputTime + 1; //break;
                } else {
                    StoredInput si = inputStream.First.Value;
                    checkTime = si.time;
                    if (si.time < estimatedServerInputTime) {
                        estimatedInput = si.input;
                        inputStream.RemoveFirst();
                    }
                }
            }
        }
    }

    public void AddInput(NetInput i) {
        inputStream.AddLast(new StoredInput(){
            time = NetworkTime.time + NetworkTime.PingWindowSize * .001f,//store input that server will recive in 1 ping
            input = i
        });
    }

    class StoredInput {
        public double time; //estimated time that the server will handle the input
        public NetInput input;
    }
}
