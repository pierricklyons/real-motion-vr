# RealMotionVR
RealMotionVR is a physics-based player controller designed for use in virtual reality applications with the Unity game engine. It aims at approximating real life movement and allows for a full range of motion. It is built around a “hexabody” collision model, using rigidbodies and configurable joints. It also features an IK avatar model for visual immersion.


## Walking and Running
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Walk.gif?raw=true)

RealMotionVR's uses a locomotion sphere that allows the user to walk and run over different types of terrain smoothly. The upper body is mounted using a spring-based spine that dampens shocks and vibrations, and ensures that the locomotion sphere is in contact with the ground. The user's real life movement, controller input, and in game external forces are seemlessly blended together using physics and a PID system.


## Crouching and Tiptoeing
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Crouch.gif?raw=true)

RealMotionVR’s height follows the actual height of the user's head. This allows the user to physically crouch, crawl and peek over objects by tiptoeing. The user can also virtually perform these actions using a controller input.


## Jumping
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Jump.gif?raw=true)

The longer the user holds the jump button, the stronger their jump will be. This is achieved by compressing its spine based on how long the button was held (just like crouching before a jump). The user can also swing their arms while jumping to influence the motion and add height to the jump. The legs (or locomotion sphere here) are lifted automatically depending on the height of the jump to get over obstacles.


## Object Interactions
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Crates.gif?raw=true)

The user can physically interact and grab objects with one or both hands in a realistic manner. The physical properties of the objects (mass, drag, etc...) influence the dynamics of these interactions.


## Climbing
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Climb.gif?raw=true)

The grabbing system allows the user to climb objects using their hands. Grabbable objects can be fixed in space to create hand holds that the player can use to physically pull themselves up.


## Configurable
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Physics%20Rig%20Inspector.png)

RealMotionVR's properties such as movement speed, mass and jump strength can be manually tweaked to suit any use case. The user's real height is also automatically accounted for with no calibration required.


## IK Rig
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/IK%20Arms.gif?raw=true)

![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/IK%20Jump.gif?raw=true)

![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/IK%20Walk.gif?raw=true) 

RealMotionVR also features a simple IK avatar model for added visual immersion. The avatar’s head, arms and legs are animated procedurally based off of the physics’s rig movement. This IK Rig is also scaled to match the user’s real height.


## Dependencies
RealMotionVR requires your Unity project to be using the new Input Action system, XR plugin Managment and XR Interaction Toolkit packages. It also requires the Animation Rigging package for the IK Rig.

## Additional Demos
![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Slopes.gif?raw=true)

![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Steps.gif?raw=true)

![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/Platform.gif?raw=true)

![image](https://github.com/pierricklyons/real-motion-vr/blob/master/README%20Images/IK%20Wave.gif?raw=true)
