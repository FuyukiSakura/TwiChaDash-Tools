# TwiChaDash OBS comment generator

> The tool parse TwiChaDash HTML5 comment to text file of your preference for OBS Text source.

There's lots of Browser source layouts in the wild for this purpose. But I make this one so you **don't** have to deal with the painful CSS and HTML.

If you don't know what is TwiChaDash, claim down. That's very normal. It is one of the fancy Japanese streaming tools that you may never be able to find on Google because you will never know what's the keyword.

But that is definitely a cool tool for either viewer or streamer. It offers the ability to translate stream chats (multi-platform) in real time for free. That will save you lots of time using translators when viewing a channel in a foreign language.

You can even type in your own language and send the text translated. The app provides ability for you to check the translation before you send. So it will be a great tool if you can read but not write in a language like me. Also to make sure you don't send an embarrassed translation.

> TwiChaDash can be [downloaded here](https://www.machanbazaar.com/download_twichadash/).

## How to use

### Setup TwiChaDash for comment output

Enable HTML 5 Comment generator in TwiChaDash

![Enable HTML5 Comment Generator](/images/twi-cha-dash-html-5.png)

Well... Claim down if you don't know what's the Japanese there. Most Japanese software translations are like this. Just get used to it 🙃

So the options actually means

```txt
0 - Original text only
1 - Translated text only
2 - Translated and Original text
```

### Start TwiChaDash OBS Comment Generator

1. Download the latest release from the [Release page](../../releases/latest).
2. Extract the program to anywhere you like.
3. Use a command prompt and execute the command as follow

    ```cmd
    ObsCommentGenerator.exe <where your HTML 5 comments are saved> <where you want to put the OBS text file>
    ```

### Setup OBS for the subtitle

1. Add a `Text (GDI+)` source in OBS

   ![Enable HTML5 Comment Generator](/images/obs-text-source.png)

2. Select read from file and select the file you put as `argument 2` when starting the program

   ![Enable HTML5 Comment Generator](/images/obs-text-from-file.png)

3. Don't forget to change the text style so your stream looks absolutely appealing ;p  
   * Here are some recommendations

   ```txt
   Alignment:               Centre
   Vertical Alignment:      Centre
   Outline:                 On
   Use Custom Text Extents: On
   Wrap:                    On

   So your text will not keep moving for different sentence lengths
   ```
