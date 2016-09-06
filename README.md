# Keycap
Keycap is a c# .net library, for super easy key board input. Keycap can be used in all different c# .net projects cause it uses a low level keyboard hook.

# How to implement Keycab?
Keycab is very easy to use and implement.
First you have to download and insert the Keycab library into your project.
Then make sure you have a reference to the Keycab library.
Once that is done you just have to write:
```c#
	using Keycap;
```
And in the very beginnin of you application somewhere you have to write:
```c#
	Input.initInput();
```
This function will initialize the library and register the keyboard hook.

Now Keycab is setup and ready to use.

#How to use?
Keycab knows 8 different methodes you can use.
The methodes are devided under 3 different categories:

- OnUpdateInput
- RegisteredEvents
- Remove

###### On Update Input
The OnUpdateInput methodes are meant to check if a key is pressed down/up in a certain frame. You can use it like this:
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
The registered events are ment to work outside a updating enviroment.
These functions register a key event and gives a callback everytime the events occurs.
Use the registered events like this:
```c#
	// Register on key down event.
	// First parm: The key you want to target.
	// Second parm: The void you want to use as callback
	// Thirth (Optional) parm: Do you want this event to occur once or until stopped.
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

#Wich KeyCodes can i use?
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
If you have any question or tips make sure to contact me at: info@dreamincode.nl

© DreamInCode B.V. 2016
