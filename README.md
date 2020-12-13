# ImAText

A library for creating images to text. Don't know why I made it, but here we go.

It takes any image, colored or not and creates the textified version of it using characters like
#,8,o,@ etc.

For colored images, it resizes them if their size is big and converts them to grayscale.
For the already grayscaled images, it just resizes them.

## How does it convert the image to characters?

Simple. To the already grayscaled image, it gets the R value of every pixel and analyzes it's value.  
The Dictionary _redValues consists of the different levels of R values and which character corresponds to what level.  
To be more specific, those values are:

<b>Level 0  :</b> "@"  
<b>Level 50 :</b> "#"  
<b>Level 70 :</b> "8"  
<b>Level 100:</b> "&"  
<b>Level 130:</b> "o"  
<b>Level 160:</b> ":"  
<b>Level 180:</b> "*"  
<b>Level 200:</b> " "  

## How to use it?

Basically the way to use it is this:

```csharp
var bitmap = new Bitmap("path/to/file");
var converter = new ImATextConveter(bitmap);

var result = converter.GetTextifiedImage();
```

## Can I extend the _redValues?

Yeah, you can. Just do this

```csharp
converter.AddRedValue(yourLevel, yourCharacter);
```

You might be asking why you can't edit the dictionary directly.  
The answer's simple. I don't trust you.

# Yes, but why?
Honestly, I'm asking myself the same thing.  
I guess I just wanted to get into image processing and this was an easy task.
