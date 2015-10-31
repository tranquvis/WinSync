# WinSync
This project provides a beautiful interface to synchronise multiple folder links with one click.

For example you often have to copy your music folder on an external device, because you downloaded some new music.
It is the same process every time. So create a Link in your WinSync application that synchronise your local music folder with the external device. Then the only thing you have to do next time is clicking on the sync button.

##Functions
- easily add, edit and synchronisation links
- pause and cancel a synchronisation process
- one-way and two-way synchronisation
- possibility to remove old files and folders
- watch the synchronisation progress with several statistics
- log display
- conflict and error documentation when the synchronisation finished
- open folders in explorer directly from the application interface

![main window](https://raw.github.com/andi1501/WinSync/master/screenshots/mainWindow.png)

##Usage
There is no installation needed.
The data is saved in a text file named links.dat.
You only have to run the WinSync.exe from the repository home, or compile it on your own.

You can also edit the **data file** if you don't want to use the interface.
The file looks like this:

```
<links>
"Downloads":"D:/Users/Andi/Downloads","D:/Users/Andi/SyncTestData/Downloads","Two Way","True"
"conflict test":"C:/Windows/System32/networklist","D:/Users/Andi/SyncTestData/conflictTest","To Folder 2","False"
"big file":"D:/Users/Andi/Downloads/bigfile","D:/Users/Andi/SyncTestData/bigfile","To Folder 2","True"
"Musik":"D:/Users/Andi/GoogleDrive/Musik","D:/Users/Andi/SyncTestData/Musik","To Folder 2","True"
"deltasport bilder":"D:/Users/Andi/Pictures/Delta Sportpark","D:/Users/Andi/SyncTestData/DeltaSportBilder","To Folder 2","True"
</links>
```
(whitespaces are allowed)

