# :movie_camera: 3D Animation Tests

---

## What is this Project?

It is a series of simple experiments, (a work in progress), about *3D Animations*. It is made with Unity3D, just for the fun of it and as a part of my self-learning journey.

---

### 1- Simple Animations & Animation Layers 


#### 1.1- Scene: :video_game: **Scene_1_AnimationsWithLayers_JasonW 1.unity**


This project is an implementation of what *Jason Weimann* teaches us in his Tutorial:   ->    [Animations with Layers in Unity3D - Unity Devs WATCH THIS - YouTube](https://www.youtube.com/watch?v=Qwy3rEDXqxA&list=PLB5_EOMkLx_VpmokLusiftsmI1s9Cy_pO)

---

| ![Simple Animations](./MediaForTheReadme/GIFs/Video3DAnimationsCropped_GIF_1_Maria_SimpleAnimations_1.gif) |
|:--:|
| <b>Simple Animations</b>|

---

| ![Animation Layers](./MediaForTheReadme/GIFs/Video3DAnimationsCropped_GIF_2_JammoRobot_AnimationLayers_2.gif) |
|:--:|
| <b>Animation Layers</b>|

---

##### :bulb: What did I learn by making this scene?

The subject studied in this project consists of two main parts:

1. :low_brightness: **Simple 3D Animations** (i.e.: Animations implemented in the 'Base Layer' of the Animator Controller).
2. :low_brightness: **Animation Layers** (two or more Layers are used in the Animator Controller, which allows us to implement mixed scenarios such as: *Running* and at the same time  *waving with the right hand to say 'Hello'*, *throwing an object*, etc.)

* :low_brightness: **Special remark:** If you use Mixamo Animations, don't forget to set your 3D Model (i.e.: the *FBX file* in the 'Project View', inside the Unity Editor) with the setting *Type of Avatar*   ->   'Humanoid', and create an Avatar from that model with that  specific setting. Then keep in mind that also ALL your Animations (FBX downloaded from Mixamo, or Animations Clips) MUST be of the same type: 'Humanoid'. If you forget this rule, and try and **mix animations** (for instance tow or more animation layers...) based on the *'Generic'* and *'Humanoid'* type of avatar together: you will run into problems: your animations won't play together, and most likely you get just one group of the animation working, in a weird and unexpected way.

* :bulb: **In short**: Set every FBX (3D Model and Animations) as 'Humanoid'... or as a 'Generic', but don't mix the two Types together, or it won't work.

---

##### Tools, Assets & Resources used in this scene

* Main Tutorial I followed   ->   [Animations with Layers in Unity3D - Unity Devs WATCH THIS - YouTube](https://www.youtube.com/watch?v=Qwy3rEDXqxA&list=PLB5_EOMkLx_VpmokLusiftsmI1s9Cy_pO)
* Mixamo Animations Website   ->  [Mixamo](https://www.mixamo.com/)
* :robot: Special mention to the Robot I used (I really liked it, it's a Free Asset by the way : )   ->   
    * [GitHub - mixandjam/Jammo-Character: Official repository for the Jammo character from the Mix and Jam channel!](https://github.com/mixandjam/Jammo-Character)
    * [Unity Asset Store | Jammo Character | Mix and Jam | 3D Characters](https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456)


---


##### Other Specs


* **Made With Unity3D version**: 2020.3.36f1 LTS
* **O.S.**: Xubuntu 20.04.01
* **IDE**: JetBrains Rider version 2022.1


##### Version control specs: 

* Git GUI software used: GitKraken Client: 8.7.0
* This Repository is set with: *Git Large File Storage* (LFS), as part of the learning & experiment.


---

Peace.

AlMartson

********************************

MIT License

Copyright (c) 2022 AlMartson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
