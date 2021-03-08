# D2FilterPicker
Little tool built to try out different loot filters on Project Diablo 2.
![screenshot of D2FilterPicker](https://i.imgur.com/XqwGI8m.png)

## Installation and How to Use
You can download the latest release here on github.
[D2FilterPicker-1.0.2](https://github.com/mik1893/D2FilterPicker/releases/download/V1.0.2/D2FilterPicker-1.0.2.zip) is the last avaialble version to download.

* Once downloaded, extract the zip file in a location of your choice and run the file **D2FilterPicker.exe**
* Browse to the default.filter in your PD2 installation folder (it might find it automatically if you have installed everything with default paths)
* The program will show your current filter on the right hand side, and you can select a filter from the dropdown list to view it
* Clicking Apply Filter will overwrite your current default.filter file and apply the selected filter. **MAKE A BACKUP OF YOUR DEFAULT FILTER IF NEEDED!**
* Clicking Update Filters List will download the latest list of filters available. I am looking for volunteers to help me update the list, please contact me on reddit in case!

## Advanced Options and Customization
You can customize the tool with your own selection of customized filters and share it with your friends. In order to do so you can:

* use a tool like [gist](https://gist.github.com) or github itself to upload your own filters to a public downloadable raw text file
* create another file which will contain the list of links to the filters you created in the step before - using the format of the [filters.json](https://raw.githubusercontent.com/mik1893/D2FilterPicker/main/D2FilterPicker/filters.json) file you can find in this repository
* Edit the **D2FilterPicker.exe.config** an change the **RemoteFilterList** setting from the default value to the link to the list of links (the filters.json file)
* Once done you can open the tool and click on the update button - it will download your list of filters.
