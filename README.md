## Extrapolation and Interpolation scripts for a smooth real-time client experience using the Mirror Networking System(Unity 3D).


 - **1st or 3rd Person**
 - **Medium to fast paced real-time**
 - **Server Authoritative**
 - **Any Character Controller system**


### Setup
 
Your input script should:
- Monitor for changes in horizontal and vertical directions (i.e. WASD movement press or release) and character rotation (i.e. mouselook).
- Send full NetInput (see NetInput.cs) upon any changes.
    - To Server using a Command
    - To Local ClientExtrapolation.cs
- Apply Rotation immediately.


Your Character Controller script should:
- Take in NetInput to apply movement and rotation
- Behave the same for client + server EXCEPT on the client side it should only use the passed in rotation to calculate movement and NOT to apply rotation *(for a smooth player experience rotation should be handled at time of input, not after the delay provided by the extrapolation script)*. While the server-side Character Controller script SHOULD apply rotation to the character.

Your NetworkBehavior script should:
- Use a syncvar to stream the position to the client(controlling client doesn't need rotaion streamed to them)
- Use a hook to keep ClientInterpolation.cs notified of the syncvar's changes *(ClientInterpolation.UpdateNetPosition())*

## ClientExtrapolation.cs
Takes a stream of input changes and approximates the time to apply the input based on recent network conditions. Ideally it will be applying the input commands with just enough delay to be processing the input at the same time as the server. Allowing the client to predict where the server will say that it is.

## ClientInterpolation.cs
A simple interpolation implementation meant to be used alongside ClientExtrapolation.cs. Intended for the controlling client.

**You will need another Interpolation solution to handle a player's perspective of other characters** This script doesn't handle rotation or have need for advanced interpolation techniques such as interpolation backtime.