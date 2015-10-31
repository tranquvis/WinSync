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

**add link**

![add link window](https://raw.github.com/andi1501/WinSync/master/screenshots/addLink.png)

**edit, remove link or open its folders by right clicking on a line**

![line popup](https://raw.github.com/andi1501/WinSync/master/screenshots/linePopup.png)

**view synchronisation statistics**

Select a running or finished synchronisation and click on the details button.

![link statistics window](https://raw.github.com/andi1501/WinSync/master/screenshots/linkStatistics.png)

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

##Development
I tried to seperate the project in data management and the synchronisation service.
####Data Management
DataManager.cs contains all important functions to store, load and change links.
Link.cs is the data model on teh one side and provides functions for synchronisation.

####Synchronisation Service
I have created a synchronisation service that is in abackground task using async and await.
The synchronisations processes like detecting file changes and copying files are running parallel too.
This isn't really necessary, due to limited read and write speeds of hard drives. But it was a good training for me.

All synchronisation statistics are published in the SyncInfo object. The SyncInfo object provides the whole information for the synchronisation, which sould be displayed at the interface.

##More
I'm happy about all people that want to improve or comment this project.
But please note that this is my first big c# application and I have written it particularly for training purposes.
