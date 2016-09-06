# Keycap
Keycap is a c# .Net library, for super easy key board input. Keycap can be used in all different c# .Net projects cause it uses a low level keyboard hook.

# How to implement Keycab?
Keycab is very easy to use and implement.
First you have to download and insert the Keycab library into your project.
Then make sure you have a reference to the Keycab library.
Once that is done you just have to write:
```c#
	using Keycap;
```
And in the very beginning of you application somewhere you have to write:
```c#
	Input.initInput();
```
This function will initialize the library and register the keyboard hook.

Now Keycab is setup and ready to use.

#How to use?
Keycab knows 8 different methods you can use.
The methods are divided under 3 different categories:

- OnUpdateInput
- RegisteredEvents
- Remove

###### On Update Input
The OnUpdateInput methods are meant to check if a key is pressed down/up in a certain frame. You can use it like this:
```c#
	// On pressed down
	if(Input.onKeyDown(KeyCode.Space)){
		// if the space key is pressed down
	}
	
	// On key released
	if(Input.onKeyUp(KeyCode.Space)){
		// if the space key is released
	}
	
	// On constantly pressed down
	if(Input.onKeyPressed(KeyCode.Space)){
		// if the space key is constantly pressed down
	}
```

###### Registered Events
The registered events are meant to work outside a updating environment.
These functions register a key event and gives a callback everytime the events occurs.
Use the registered events like this:
```c#
	// Register on key down event.
	// First parm: The key you want to target.
	// Second parm: The void you want to use as callback
	// Third (Optional) parm: Do you want this event to occur once or until stopped.
	Input.registerOnKeyDown(KeyCode.Space,CallbackVoid,true);
	
	// Register on key constantly pressed.
	// First parm: The key you want to target.
	// Second parm: The void you want to use as callback
	Input.registerOnKeyPressed(KeyCode.Space,CallbackVoid);
	
	// Register on key released.
	// First parm: The key you want to target.
	// Second parm: The void you want to use as callback
	// Thirth (Optional) parm: Do you want this event to occur once or until stopped.
	Input.registerOnKeyUp(KeyCode.Space,CallbackVoid,true);
```

###### Remove
If you want to remove a registered event you can just use this function:
```c#
	// remove all the registered events from a key
	Input.removeKeyRegister(KeyCode.Space);
```

#What KeyCodes can I use?
In Keycap you can use a bunch of different KeyCodes.

###### Special buttons
- Space
- Tab
- Enter
- Shift
- Backspace
- Ctrl
- Alt
- Pause
- CapsLock
- Esc
- PageUp
- PageDown
- End
- Home
- Left
- Right
- Up
- Down
- Ins
- Del
- Help
- PrintScreen
- Select
- NumLock
 
###### Letters
- A
- B
- C
- D
- E
- F
- G
- H
- I
- J
- K
- L
- M
- N
- O
- P
- Q
- R
- S
- T
- U
- V
- W
- X
- Y
- Z

###### Numbers
- Zero
- One
- Two
- Three
- Four
- Five
- Six
- Seven
- Eight
- Nine

# End
Thanks for using Keycap.
If you have any questions or tips make sure to contact us at: vkieft@dreamincode.nl

	**© Copyright 2016 DreamInCode B.V.**

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.