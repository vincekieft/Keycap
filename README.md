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
