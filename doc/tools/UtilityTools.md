# Utility Tools ⚙️
- [Dictionary](#Dictionary)

## Dictionary
![image](https://user-images.githubusercontent.com/57708659/145928584-7dcad912-642b-4eb3-95a0-dd32fe0ebc78.png)

**Why should Dynamo be the only one to have Dictionaries?**<br/>
This series of components allows you to structure your data using [dictionaries](https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/).<br/>
Dictionary is a generic collection that stores key-value pairs in no particular order. <br/>
This will help to transport more data easily.

## Nodes 
* CreateDict
  1. is able to create e dictionary inputting a list of keys and values.
  2. is able to create a value list of the dictionary and place it into the canvas.
* ReadDict
  1. is able to read the dictionary and retrieve the keys and values.
* MergeDict
  1. merges dictionaries together.
* FilterDict
  1. is able to filter the dictionary keys using a wildcard pattern.
* DeleteKeyDict
  1. is able to search for keys and delete the key-value pair from the dictionary.
* ValueTypeDict
  1. returns the type of all the values in a dictionary. 
* JSONFromDict
  1. returns a json serialised object from the dictionary.
* CSVFromDict
  1. returns a csv string as well as saves a file based on the provided dictionary, with one line per value in the dictionary. 

### Tags 
Grasshopper, Dictionary
