# RealMotionVR
RealMotionVR is a physics-based player controller designed for use in virtual reality applications with the Unity game engine. It aims at approximating real life movement and allows for a full range of motion. It is built around a “hexabody” collision model, using rigidbodies and configurable joints. It also features an IK avatar model for visual immersion.


## Walking and Running
![image](https://pouch.jumpshare.com/preview/en6LQRFXZbAuuxsfGJ02jJuL5UouW8poT9mPJpyG00k68qBNHWSDbbgChE7Njv9_CV2uIyf24yIplrINKgus35rMMUYiVupDU-EHw9FltvE)

RealMotionVR's uses a locomotion sphere that allows the user to walk and run over different types of terrain smoothly. The upper body is mounted using a spring-based spine that dampens shocks and vibrations, and ensures that the locomotion sphere is in contact with the ground. The user's real life movement, controller input, and in game external forces are seemlessly blended together using physics and a PID system.


## Crouching and Tiptoeing
![image](https://pouch.jumpshare.com/preview/hTRcyGzd_TH7fPJXsIvxd20To0_6OyH_MI5DHh84Cqw_K26RKZfABLmL883TLsZwC3pIJ0FFUDvLtCHOSXio7SH7vFdAhzEt8CeAsOSmjUY)

RealMotionVR’s height follows the actual height of the user's head. This allows the user to physically crouch, crawl and peek over objects by tiptoeing. The user can also virtually perform these actions using a controller input.


## Jumping
![image](https://pouch.jumpshare.com/preview/xxhv8IR8-9cwqsgpz2QlICAcIwR3pN_oz5HzZMwzq4tOH5ID3cLFKjKnje63MWTAC3pIJ0FFUDvLtCHOSXio7YCfa8R5l2Lk0QiflXVoIIQ)

The longer the user holds the jump button, the stronger their jump will be. This is achieved by compressing its spine based on how long the button was held (just like crouching before a jump). The user can also swing their arms while jumping to influence the motion and add height to the jump. The legs (or locomotion sphere here) are lifted automatically depending on the height of the jump to get over obstacles.


## Object Interactions
![image](https://pouch.jumpshare.com/preview/inOUcH75AMJatk-ok0zEtXrnIAc2F3gvkbOyWRVd7V5wy9bs7gMIW48YLmSJ1fHIFHPZZlTVSrkbjIVF2QUmEX0-bSTjtTK0TKyeJ1JKGyQ)

The user can physically interact and grab objects with one or both hands in a realistic manner. The physical properties of the objects (mass, drag, etc...) influence the dynamics of these interactions.


## Climbing
![image](https://pouch.jumpshare.com/preview/-LDbKmzOfZc2bZr-591gZZi6Nw010KVP0L145iv27aVdcHWjClcEsTGdWB8N_UsxTHgF7TiOSnnK2ulQgoIxFf_dub0UcmRJlkPcPJK68IQ)

The grabbing system allows the user to climb objects using their hands. Grabbable objects can be fixed in space to create hand holds that the player can use to physically pull themselves up.


## Configurable
![image](https://pouch.jumpshare.com/preview/s-nq65pXlSenJcHg7_59Oov4cZs1R1YGJIDwSHvhb91N3z95vN80o91PH8HEpwkHymPQTS6pxs0rFpE-HjAQQUpCI_vwjyB6Zc_WOk_modk)

RealMotionVR's properties such as movement speed, mass and jump strength can be manually tweaked to suit any use case. The user's real height is also automatically accounted for with no calibration required.


## IK Rig
![image](https://pouch.jumpshare.com/preview/TLk0FWbvzZ9yeAr52X_s4E3nv2zGXKegUx0aGCxg-gw1ZeH-6TEWZJRa87Cb3IIxjaP90Dnm593LnoHuKEldth8PajTUcvtj5v54eJvSQTI)

![image](https://pouch.jumpshare.com/preview/nU5Y13rBA8m1FGxYpcc7Ynmee9aBjbLLlWg9K9AraDr4RSDmuC4NC4rz-P5NPhOEHHLurDkIjAfub6ZUHHlMfXtJwRRykRjWZk7cZbjgBIE)

![image](https://pouch.jumpshare.com/preview/VPPOh1lrQ4kp0eDWkB6ctZAZFw6WiNrNF57MDsSa2rBX3S2KRJbGWdlodz_oHL3GHHLurDkIjAfub6ZUHHlMfUWP81PAkQQqeCzOQPqyXHA) 

RealMotionVR also features a simple IK avatar model for added visual immersion. The avatar’s head, arms and legs are animated procedurally based off of the physics’s rig movement. This IK Rig is also scaled to match the user’s real height.

## Dependencies
RealMotionVR requires your Unity project to be using the new Input Action system, XR plugin Managment and XR Interaction Toolkit packages. It also requires the Animation Rigging package for the IK Rig.

## Additional Demos
![image](https://pouch.jumpshare.com/preview/QgC4QOAXSiKfAaRLUGtqiJIqP7xra3KueFugSneW4a_va8BfueuUJvtdgN_5aRCygg5KAC76urfLmL5pc3D4xZBop1nO34SKAZZAbjMh7wY)

![image](https://pouch.jumpshare.com/preview/PbDOFqz4o5WO2TzZ0-bXqpw70tFAUOlLHordNCgw3QipNWEBpy49BqiukldgwA90gg5KAC76urfLmL5pc3D4xXdJzJ8HzzkbOdTim5Xuhvg)

![image](https://pouch.jumpshare.com/preview/PdugAx0JfHWZL5H6ilS4UN7gdWep0zGIOmR5haIBewGcvYj25qew2mqGbiwH-2u-gg5KAC76urfLmL5pc3D4xSXu4DUqlp4Owv51xUVLIKs)

![image](https://pouch.jumpshare.com/preview/JAECOsz0zWhJXEq4EYxO_JliZDY1vQVT_ikrEbVGsyVlP3hJk-Twz0x7umn2ELMVgg5KAC76urfLmL5pc3D4xbEHcY5Bai5DHWkY3rEmN_k)
