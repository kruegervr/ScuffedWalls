## Functions

Functions are defined by a number followed with a colon and the function name (i.e  5:function). Every line beneath the function will apply to that function. All the available functions that can be used are the following.

- [`TextToWall`](#TextToWall)
- [`ModelToWall`](#ModelToWall)
- [`ImageToWall`](#ImageToWall)
- [`CloneFromWorkspace`](#CloneFromWorkspace)
- [`Blackout`](#Blackout)
- [`AppendToAllWallsBetween`](#AppendToAllWallsBetween)
- [`AppendToAllNotesBetween`](#AppendToAllNotesBetween)
- [`AppendToAllEventsBetween`](#AppendToAllEventsBetween)
- [`Import`](#Import)
- [`Wall`](#Wall)
- [`Note`](#Note)
- [`AnimateTrack`](#AnimateTrack)
- [`AssignPathAnimation`](#AssignPathAnimation)
- [`AssignPlayerToTrack`](#AssignPlayerToTrack)
- [`ParentTrack`](#ParentTrack)
- [`PointDefinition`](#PointDefinition)


## CustomData
generic customdata that can be parsed as a parameter on most functions
"" = put in quotes, ? = optional
- AnimateDefinitePosition: \[x,y,z,t,"e"?]
- DefineAnimateDefinitePosition:string
- AnimatePosition: \[x,y,z,t,"e"?]
- DefineAnimatePosition:string
- Scale: \[x,y?,z?]
- Track: string
- NJSOffset: float
- NJS: float
- AnimateDissolve: \[d,t,"e"?]
- DefineAnimateDissolve:string
- AnimateColor: \[r,g,b,a,t,"e"?]
- DefineAnimateColor:string
- AnimateRotation: \[x,y,z,t,"e"?]
- DefineAnimateRotation:string
- AnimateLocalRotation: \[x,y,z,t,"e"?]
- DefineAnimateLocalRotation:string
- AnimateScale: \[x,y,z,t,"e"?]
- DefineAnimateScale:string
- AnimateInteractable:\[i,t]
- DefineAnimateInteractable:string
- Interactable: bool
- Fake: bool
- Position: \[x,y]
- Rotation: \[x,y,z] or float
- LocalRotation: \[x,y,z]
- CutDirection: float
- NoSpawnEffect: bool

## CustomEvent Data
generic customdata for customevents
"" = put in quotes, ? = optional
- AnimateDefinitePosition: \[x,y,z,t,"e"?]
- DefineAnimateDefinitePosition:string
- AnimatePosition: \[x,y,z,t,"e"?]
- DefineAnimatePosition:string
- Track: string
- AnimateDissolve: \[d,t,"e"?]
- DefineAnimateDissolve:string
- AnimateColor: \[r,g,b,a,t,"e"?]
- DefineAnimateColor:string
- AnimateRotation: \[x,y,z,t,"e"?]
- DefineAnimateRotation:string
- AnimateLocalRotation: \[x,y,z,t,"e"?]
- DefineAnimateLocalRotation:string
- AnimateScale: \[x,y,z,t,"e"?]
- DefineAnimateScale:string
- childTracks:\["str","str"...]
- parentTrack: string
- easing: string

## Chroma CustomData : CustomData
 - CPropID: int
 - CLightID: int
 - CGradientDuration: float
 - CgradientStartColor: \[r,g,b,a?]
 - CgradientEndColor: \[r,g,b,a?]
 - CgradientEasing: string
 - CLockPosition: bool
 - CPreciseSpeed: float
 - CDirection: int
 - CNameFilter: string
 - CReset: bool
 - CStep: float
 - CProp: float
 - CSpeed: float
 - CCounterSpin: bool
 - Color: \[r,g,b,a] (0-1)
 - RGBColor:\[r,g,b,a] (0-255)

# TextToWall
uses imagetowall to procedurally create walltext from text (Constructs text out of walls)

Rizthesnuggies [`Intro to TextToWall`](https://www.youtube.com/watch?v=g49gfMtzETY) function

see [here](https://github.com/thelightdesigner/ScuffedWalls/blob/main/TextToWall.md) for how the program reads font images.

 - path: string
 - fullpath string
 - line: string, the text you want to convert to walls. [this can be repeated](https://github.com/thelightdesigner/ScuffedWalls/blob/main/Readme/linetext.jpg) to add more lines of text.
 - letting: float, the relative space between letters. default: 1
 - leading: float, the relative space between lines. default: 1
 - size: float, scales the text. default: 1 (gigantic)
 - thicc: float, makes the edges of the walls fill more of the center
 - duration: float
 - Position => moves the text by this amount, defaults to \[0,0]
 - all the other imagetowall params if your really interested
 - generic custom data
 
 Example
 ```
 5:TextToWall
   Path:font.png
   line:a line of text!
   line:another line of text?
   letting:2
   leading:-1
   thicc:12
   spreadspawntime:1
   size:0.1
   position:[0,2]
   duration:12
   animatedefiniteposition:[0,0,0,0]
 ```

# ModelToWall
constructs a model out of walls. see [here](https://github.com/thelightdesigner/ScuffedWalls/blob/main/Blender%20Project.md) for more info

Rizthesnuggies [`Intro to ModelToWall`](https://youtu.be/FfHGRbUdV_k) function

 - path: string
 - fullpath string
 - hasAnimation: bool, tells the model parser to read animation. doesnt work with normal yet.
 - duration: float, controls the duration of the model. this affects the length of time it takes to play the model animation.
 - spreadspawntime: float
 - Normal: bool, makes the walls jump in and fly out as normal. essentially 1.0 model to wall when set to true. default: false
 - generic custom data
 
  Example
  ```
 5:ModelToWall
   Path:model.dae
   hasAnimation:false
   spreadspawntime:1
   normal:true
   track:FurryTrack
 ```

# ImageToWall
constructs an image out of walls as pixels

Rizthesnuggies [`Intro to ImageToWall`](https://youtu.be/Cxbc4llIq3k) function

 - path: string
 - fullpath string
 - isBlackEmpty: bool, doesn't add pixel if the pixel color is black. default: false
 - size: float, scales the image. default: 1
 - thicc: float, makes the edges of the walls fill more of the center
 - centered: bool, centers the x position. default: false
 - duration: float
 - spreadspawntime: float. default: 0
 - maxlinelength: int, the max line length. default: +infinity
 - shift: float, the difference in compression priorities between the inversed compression. default: 1
 - compression: float, how much to compress the wall image, Not linear in the slightest. reccomended value(0-0.1) default: 0
 - Position => moves each pixel by this amount, defaults to \[0,0]
 - Alpha: the alpha value
 - generic custom data
 
  Example
  ```
 5:ImageToWall
   Path:image.png
   thicc:12
   size:0.5
   isBlackEmpty: true
   centered: true
   maxlinelength:8
   compression:0.1
   spreadspawntime:1
   position:[0,2]
   duration:12
   animatedefiniteposition:[0,0,0,0]
 ```
 
# CloneFromWorkspace
clones mapobjects from a different workspace by the index or by the name. the time of the function is the beat that starts cloning from.

- Type: int,int,int (defaults to 0,1,2,3) 0 being walls, 1 being notes, 2 being lights, 3 being custom events & NOT point definitions
- Index: int, the index of the workspace you want fo clone from. Its ethier one or the other.
- Name:string, the name of the workspace you want to clone from. Its ethier one or the other.
- addTime: float, shifts the cloned things by this amount.
- toBeat: float, the beat where to stop cloning from.

 Example
```
Workspace:wtf workspace
64:wall

Workspace:hahaball

Workspace
 25:CloneFromWorkspace
   Name:wtf workspace
   Type:0,1,2
   toBeat:125
   addTime:32
 ```
 
# Blackout
adds a single light off event at the beat number. why? because why not.

 Example
  ```
 5:Blackout
 ```

# AppendToAllEventsBetween
adds on custom chroma data to events/lights between the function time and endtime

 - toBeat: float
 - appendTechnique: int(0-2)
 - chroma customdata
 - lighttype: 0, 1, 2, 3; the type of the light
  
 ~special things~
 - converttoprops
 - propfactor
 - converttorainbow
 - rainbowfactor

 Example
```
 5:AppendToAllEventsBetween
   toBeat:10
   appendTechnique:2
   lightType:1,3,0
   converttoprops: true
   propfactor: 1
   converttorainbow: true
   rainbowfactor:1
 ```

# AppendToAllWallsBetween
adds on custom noodle data to walls between the function time and endtime

 - toBeat: float
 - appendTechnique: int(0-2)
 - generic custom data
 
  Example
 ```
 5:AppendToAllWallsBetween
   toBeat:10
   track: FurryTrack
   appendTechnique:1
 ```

# AppendToAllNotesBetween
adds on custom noodle data to notes between the function time and endtime

 - toBeat: float
 - type: int,int,int (defaults to 0,1,2,3,4,5,6,7,8)
 - appendTechnique: int(0-2)
 - generic custom data
 
  Example
  ```
 5:AppendToAllNotesBetween
   toBeat:10
   track: FurryTrack
   appendTechnique:2

60:AppendToAllNotesBetween
tobeat:63
Njsoffset:Random(1,3)
AnimatePosition:[Random(-7,6),Random(-6,6),0,0],[0,0,0,0.35,"easeOutCubic"],[0,0,0,1]
AnimateDissolve:[0,0],[1,0.1],[1,1]
NoSpawnEffect:true

66:AppendToAllNotesBetween
tobeat:99
NJS:10
NoSpawnEffect:true
AnimateDissolveArrow: [0,0],[0,1]
track:CameraMoveNotes
 ```

 ## AppendTechnique
tells the append function how to add on your custom data properties to other map object custom data.
 - 0 = Will not overwrite any old custom data property but can still append to nulled properties. Usefull for NJS and Offset fixing.
 - 1 = Overwrites the old custom data property for the new one. Usefull for most applications.
 - 2 = Nulls all old customdata properties and appends on new ones. Usefull for some internal stuff but not much else.

**default is always 0**

 
# Import
adds in map objects from other map.dat files

 - path: string
 - fullpath string
 - type:int,int,int (defaults to 0,1,2,3) what to import where 0 = walls, 1 = notes, 2 = lights, 3 = customevents & point definitions
 - fromBeat: float
 - toBeat: float
 
  Example
  ```
 5:Import
   fullpath:E:\New folder\steamapps\common\Beat Saber\Beat Saber_Data\CustomWIPLevels\scuffed walls test\EasyStandard.dat
   type:2
   fromBeat:15
   toBeat:180
 ```



# Wall
makes a wall

- duration: float
- repeat: int, amount of times to repeat
- repeatAddTime: float
- generic custom data

 Example
```
	#blue fire
5:Wall
  repeat:160
  repeataddtime:0.2
  NJSOffset:-10
  animatedefiniteposition:[Random(0,2),Random(8,12),Random(28,31),0],[Random(-7,-4),Random(10,14),Random(28,31),1,"easeInSine"]
  animatescale:[1,1,1,0],[0.01,0.01,0.01,1,"easeInSine"]
  scale:[0.8,0.8,0.8]
  color:[0,Random(1.6,1.7),Random(1.9,2),2]
  localrotation:[Random(0,360),Random(0,360),Random(0,360)]
  rotation:[0,0,-5]
  track:flowerfloat
  animatedissolve:[0,0],[1,0],[1,0.9],[0,1]

	#shooting star
164:Wall
  repeat:50
  repeataddtime:0.4
  scale:[0.2,0.2,18]
  Njs:10
  NjsOffset:20
  position:[Random(0,80),Random(-100,100)]
  color:[Random(0,20),Random(0,20),Random(0,20),20]
  rotation:[Random(0,360),90,0]

```

# Note
makes a note

- repeat: int, amount of times to repeat
- repeatAddTime: float
- generic custom data

 Example
```
	#Note fire
100:Note
  repeat:66
  repeatAddTime:0.3
  localRotation:[Random(0,360),Random(0,360),Random(0,360)]
  Rotation:[90,0,0]
  Position:[Random(-12,-6),Random(8,18)]
  AnimatePosition:[0,0,-10,0],[0,0,-10,1]
  AnimateDissolve:[0,0],[1,0.1],[1,1]
  AnimateScale:[2,2,2,0],[2,2,2,1]
  AnimateColor:[1, 0, 0, 1,0], [0, 1, 0, 0.5, 0.0832], [0, 0, 1, 1, 0.15], [1, 0, 0, 1, 0.23], [0, 1, 0, 1, 0.30], [0, 0, 1, 1, 0.38], [1, 0, 0, 1, 0.45], [0, 1, 0, 1, 0.52],     [0, 0, 1, 1, 0.60], [1, 0, 0, 1, 0.68], [0, 1, 0, 1, 0.75], [0, 0, 1, 1, 0.84],[1,1,1,1,1]
  NJS:10
  NJSOffset:4
  fake:true
  isInteractable: false
  track: RandomShit
```

# AnimateTrack
makes a custom event

 - customevent data
 - repeat: int, amount of times to repeat
 - repeatAddTime: float
 
  Example
 ```
 70:AnimateTrack
  track:RandomShit
  duration:1
  animatedissolve:[0,0],[0,1]
  animatedissolvearrow:[0,0],[0,1]
  
  100:AnimateTrack
  track:RandomShit
  duration:1
  animatedissolve:[0,0],[1,1]
  animatedissolvearrow:[0,0],[1,1]
```

# AssignPathAnimation
makes a custom event

 - customevent data
 - repeat: int, amount of times to repeat
 - repeatAddTime: float
 
  Example
 ```
42:AssignPathAnimation
  track:BeginningStretch
  duration:8
  AnimateRotation:[0,0,0,0],[0,0,0,0.5,"easeOutQuad"],[0,0,0,1]
  easing:easeInOutSine
```

# AssignPlayerToTrack
makes a custom event
 - customevent data
 
  Example
 ```
 3:AssignPlayerToTrack
    track:BigMovement
 ```

# ParentTrack
makes a custom event
 - customevent data
 
  Example
  ```
 3:ParentTrack
    ParentTrack:BigMovement
    ChildTracks:["rightnotes","leftnotes"]
 ```

# PointDefinition
makes a point definition
  - name:string
  - points:points

  Example
```
  5:PointDefinition
    name:UpDownPoints
    points:[0,0,0,0],[0,15,0,0.5,"easeInOutSine"],[0,0,0,1,"easeInOutSine"]

  15:Wall
    DefineAnimateDefinitePosition:UpDownPoints
```

# Uwu
:)

